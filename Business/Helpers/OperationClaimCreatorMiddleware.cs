using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Fakes.Handlers.OperationClaims;
using Business.Fakes.Handlers.UserClaims;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Helpers
{
    public static class OperationClaimCreatorMiddleware
    {
        public static async Task UseDbOperationClaimCreator(this IApplicationBuilder app)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();
            var userRepository = ServiceTool.ServiceProvider.GetService<IUserRepository>();
            foreach (var operationName in GetOperationNames())
            {
                await mediator.Send(new CreateOperationClaimInternalCommand
                {
                    ClaimName = operationName
                });
            }

            var operationClaims = (await mediator.Send(new GetOperationClaimsInternalQuery())).Data;
            var adminUser = await userRepository.GetAsync(x => x.Email == "admin@adminmail.com");
            HashingHelper.CreatePasswordHash("Q1w212*_*", out var passwordSalt, out var passwordHash);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    FullName = "System Admin",
                    Email = "admin@adminmail.com",
                    Status = true,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash
                };
                userRepository.Add(adminUser);
            }
            else
            {
                adminUser.FullName = "System Admin";
                adminUser.Status = true;
                adminUser.PasswordSalt = passwordSalt;
                adminUser.PasswordHash = passwordHash;
                userRepository.Update(adminUser);
            }

            await userRepository.SaveChangesAsync();

            await mediator.Send(new CreateUserClaimsInternalCommand
            {
                UserId = adminUser.UserId,
                OperationClaims = operationClaims
            });
        }

        private static IEnumerable<string> GetOperationNames()
        {
            var assemblies = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    // runtime generated anonmous type'larin assemblysi olmadigi icin null cek yap
                    x.Namespace != null && x.Namespace.StartsWith("Business.Handlers") &&
                    (x.Name.EndsWith("Command") || x.Name.EndsWith("Query")));

            return (from assembly in assemblies
                    from nestedType in assembly.GetNestedTypes()
                    from method in nestedType.GetMethods()
                    where method.CustomAttributes.Any(u => u.AttributeType == typeof(SecuredOperation))
                    select assembly.Name).ToList();
        }
    }
}
