using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TenantDto : IDto
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public int? OrganizationId { get; set; }
    }
}
