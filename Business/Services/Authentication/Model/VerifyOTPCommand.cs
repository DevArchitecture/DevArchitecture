using Core.Utilities.Results;
using Core.Entities;
using MediatR;

namespace Business.Services.Authentication.Model
{
    /// <summary>
    /// Kullanıcıya gönderilen One Time Password'ü kontrol etmek gönderilen komuttur.
    /// </summary>
    public class VerifyOtpCommand : IRequest<IDataResult<SFwToken>>
    {
        public AuthenticationProviderType Provider { get; set; }
        /// <summary>
        /// Aynı provider kullanıcısın farklı sistemlerden girebilmesini
        /// sağlamak için alt türü belirtir.
        /// </summary>
        public string ProviderSubType { get; set; }
        public string ExternalUserId { get; set; }
        public int Code { get; set; }
    }
}
