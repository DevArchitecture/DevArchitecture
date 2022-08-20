﻿using Business.BusinessAspects;
using Business.Fakes.Handlers.Authorizations;
using Business.Fakes.Handlers.Companies;
using Business.Fakes.Handlers.Groups;
using Business.Fakes.Handlers.OperationClaims;
using Business.Fakes.Handlers.UserClaims;
using Core.Utilities.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Business.Helpers;

public static class OperationClaimCreatorMiddleware
{
    public static async Task UseDbOperationClaimCreator(this IApplicationBuilder app)
    {
        var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();
        await mediator.Send(new CreateCompanyInternalCommand
        {
            FirmName = "DevArchitecture Inc.",
            Email = "info@adminmail.com",
            Address = "Orenda is Everywhere",
            Name = "Orenda",
            Phone = "03122123456",
            Phone2 = "03122125678",
            TaxNo = "12345678912",
            WebSite = "www.devarchitecture.net"
        });
        foreach (var operationName in GetOperationNames())
        {
            await mediator.Send(new CreateOperationClaimInternalCommand
            {
                ClaimName = operationName
            });
        }

        var operationClaims = (await mediator.Send(new GetOperationClaimsInternalQuery())).Data;
        await mediator.Send(new CreateGroupInternalCommand
        {
            TenantId = 1,
            GroupName = "Users"
        });
        await mediator.Send(new RegisterUserInternalCommand
        {
            FullName = "System Admin",
            Password = "Q1w212*_*",
            Email = "admin@adminmail.com",
        });
        await mediator.Send(new CreateUserClaimsInternalCommand
        {
            UserId = 1,
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
