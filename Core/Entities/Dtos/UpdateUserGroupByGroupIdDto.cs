using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateUserGroupByGroupIdDto : IDto
    {
        public int GroupId { get; set; }
        public int[] UserIds { get; set; }
    }
}
