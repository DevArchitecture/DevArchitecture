using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateGroupDto : IDto
    {
        public string GroupName { get; set; }
    }
}
