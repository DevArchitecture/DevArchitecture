namespace Business.Adapters.PersonService
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Entities.Dtos;
    using wsKPSPublic;

    public class PersonServiceManager : IPersonService
    {
        public async Task<bool> VerifyCid(Citizen citizen)
        {
            return await Verify(citizen);
        }

        private static async Task<bool> Verify(Citizen citizen)
        {
            var locale = new CultureInfo("tr-TR", false);
            var svc = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            {
                var cmd = await svc.TCKimlikNoDogrulaAsync(
                  citizen.CitizenId,
                  citizen.Name.ToUpper(locale),
                  citizen.Surname.ToUpper(locale),
                  citizen.BirthYear);
                return cmd.Body.TCKimlikNoDogrulaResult;
            }
        }
    }
}
