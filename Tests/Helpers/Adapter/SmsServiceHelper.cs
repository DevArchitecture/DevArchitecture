using Business.Adapters.SmsService;

namespace Tests.Helpers.Adapter
{
    public class SmsServiceHelper
    {
        private readonly ISmsService _smsService;

        public SmsServiceHelper(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public bool Send(string password, string text, string cellPhone)
        {
            return _smsService.Send(password, text, cellPhone).Result;
        }

        public bool SendAssist(string text, string cellPhone)
        {
            return _smsService.SendAssist(text, cellPhone).Result;
        }
    }
}