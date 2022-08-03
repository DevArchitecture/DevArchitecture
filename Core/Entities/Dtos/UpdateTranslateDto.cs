using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class UpdateTranslateDto : IDto
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}
