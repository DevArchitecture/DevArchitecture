namespace Business.Adapters.SmsService
{
    using System.Threading;
    using System.Threading.Tasks;

    public class SmsServiceManager : ISmsService
    {
        public async Task<bool> Send(string password, string text, string cellPhone)
        {
            Thread.Sleep(1000);
            return await Task.FromResult(true);
        }
        public async Task<bool> SendAssist(string text, string cellPhone)
        {
            Thread.Sleep(1000);
            return await Task.FromResult(true);
        }
    }
}
