using System;

namespace Core.Entities.Dtos
{
    public class ShowcaseRowDto : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
    }
}
