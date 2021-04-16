namespace Tests.Helpers.Adapter
{
    using global::Business.Adapters.SmsService;

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
