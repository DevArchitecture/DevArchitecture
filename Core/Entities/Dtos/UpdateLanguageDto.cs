using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateLanguageDto : IDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
