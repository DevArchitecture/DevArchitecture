using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateUserClaimDto : IDto
    {
        public int UserId { get; set; }
        public int[] ClaimIds { get; set; }
    }
}
