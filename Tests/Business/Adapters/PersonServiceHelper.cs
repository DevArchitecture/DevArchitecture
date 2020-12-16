using Business.Adapters.PersonService;
using System.Net;

namespace SennedjemTests.Business.Adapters
{
    public class PersonServiceHelper
    {
        private readonly IPersonService _personService;

        public PersonServiceHelper(IPersonService personService)
        {
            this._personService = personService;
        }

        public bool VerifyId(long TCKimlikNo, string Ad, string Soyad, int DogumYili)
        {
            try
            {
                return _personService.VerifyCid(TCKimlikNo, Ad, Soyad, DogumYili).Result;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
