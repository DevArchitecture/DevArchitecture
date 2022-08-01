using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateUserDto : IDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string MobilePhones { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }
}
