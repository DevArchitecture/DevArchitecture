using AutoMapper;
using Business.Fakes.Handlers.Authorizations;
using Business.Fakes.Handlers.Companies;
using Business.Fakes.Handlers.Groups;
using Business.Fakes.Handlers.Languages;
using Business.Fakes.Handlers.OperationClaims;
using Business.Fakes.Handlers.Translates;
using Business.Fakes.Handlers.UserGroups;
using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Companies.Commands;
using Business.Handlers.Groups.Queries;
using Business.Handlers.Languages.Commands;
using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
using Business.Handlers.Translates.Commands;
using Business.Handlers.UserGroups.Commands;
using Core.Entities.Concrete;
using Core.Entities.Dtos;

namespace Business.Helpers;

public class AutoMapperHelper : Profile
{
    public AutoMapperHelper()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<RegisterUserInternalCommand, RegisterUserCommand>();
        CreateMap<CreateCompanyInternalCommand, CreateCompanyCommand>();
        CreateMap<CreateLanguageInternalCommand, CreateLanguageCommand>();
        CreateMap<CreateOperationClaimInternalCommand, CreateOperationClaimCommand>();
        CreateMap<GetOperationClaimsInternalQuery, GetOperationClaimsQuery>();
        CreateMap<CreateTranslateInternalCommand, CreateTranslateCommand>();
        CreateMap<CreateUserGroupInternalCommand, CreateUserGroupCommand>();
        CreateMap<GetGroupByNameInternalQuery, GetGroupByNameQuery>();
    }
}
