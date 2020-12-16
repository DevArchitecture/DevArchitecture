using Business.BusinessAspects;
using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
using Business.Handlers.UserClaims.Commands;
using Core.Utilities.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class OperationClaimCreatorMiddleware
    {
        public async static Task UseDbOperationClaimCreator(this IApplicationBuilder app)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();
            foreach (var operationName in GetOperationNames())
            {
                await mediator.Send(new CreateOperationClaimCommand
                {
                    ClaimName = operationName
                });
            }
            var operationClaims = (await mediator.Send(new GetOperationClaimsQuery())).Data;
            await mediator.Send(new CreateUserClaimsInternalCommand
            {
                UserId = 1,
                OperationClaims = operationClaims
            });
        }

        private static IEnumerable<string> GetOperationNames()
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                // runtime generated anonmous type'larin assemblysi olmadigi icin null cek yap
                x.Namespace != null && x.Namespace.StartsWith("Business.Handlers") && (x.Name.EndsWith("Command") || x.Name.EndsWith("Query"))
              && x.CustomAttributes.Any(a => a.AttributeType == typeof(SecuredOperation)))
            .Select(x => x.Name);
            return assemblyNames;
        }
    }
}
