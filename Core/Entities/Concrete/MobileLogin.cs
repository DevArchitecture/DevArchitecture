using System;

namespace Core.Entities.Concrete
{
    public class MobileLogin : IEntity
    {
        public int Id { get; set; }
        public AuthenticationProviderType Provider { get; set; }
        public string ExternalUserId { get; set; }
        public int Code { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSend { get; set; }
        public bool IsUsed { get; set; }
    }
}