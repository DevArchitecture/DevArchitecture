using Business.Handlers.Users.Queries;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Helpers
{
    public class GetTenantQuery: IRequest<IDataResult<TenantDto>>
    {
        public class GetTenantQueryHandler : IRequestHandler<GetTenantQuery, IDataResult<TenantDto>>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IMediator _mediator;
            public GetTenantQueryHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator)
            {
                _httpContextAccessor = httpContextAccessor;
                _mediator = mediator;
            }
            public async Task<IDataResult<TenantDto>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
            {
                var tenantId = _httpContextAccessor.HttpContext?.User.Claims
               .FirstOrDefault(x => x.Type.EndsWith("tenantId"))?.Value;

                var userId = _httpContextAccessor.HttpContext?.User.Claims
               .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var organizationId = await _mediator.Send(new GetUserQuery { UserId = Convert.ToInt32(userId) }, cancellationToken);
                var tenant = new TenantDto
                {
                    TenantId = Convert.ToInt32(tenantId),
                    UserId = Convert.ToInt32(userId),
                    OrganizationId =organizationId.Data.OrganizationId
                };
                return new SuccessDataResult<TenantDto>(tenant);
            }

            
        }
    }
}
