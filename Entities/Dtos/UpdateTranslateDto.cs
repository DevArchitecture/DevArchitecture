using Core.Entities;

namespace Entities.Dtos
{
    public class UpdateTranslateDto : IDto
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}