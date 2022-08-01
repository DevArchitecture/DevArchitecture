using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateOperationClaimDto : IDto
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
    }
}
