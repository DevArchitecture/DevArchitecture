using Business.Adapters.SmsService;

namespace Tests.Business.Adapters
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
			return _smsService.SendAsist(text, cellPhone).Result;
		}
	}
}
