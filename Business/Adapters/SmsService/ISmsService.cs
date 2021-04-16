namespace Business.Adapters.SmsService
{
    using System.Threading.Tasks;

    public interface ISmsService
    {
        Task<bool> Send(string password, string text, string cellPhone);
        Task<bool> SendAssist(string text, string cellPhone);
    }
}
