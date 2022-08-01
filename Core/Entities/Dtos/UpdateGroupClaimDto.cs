using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateGroupClaimDto : IDto
    {
        public int GroupId { get; set; }
        public int[] ClaimIds { get; set; }
    }
}
