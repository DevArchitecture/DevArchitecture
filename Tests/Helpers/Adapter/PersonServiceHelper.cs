using System.Net;
using Business.Adapters.PersonService;
using Entities.Dtos;

namespace Tests.Helpers.Adapter
{
    public class PersonServiceHelper
    {
        private readonly IPersonService _personService;

        public PersonServiceHelper(IPersonService personService)
        {
            _personService = personService;
        }

        public bool VerifyId(Citizen citizen)
        {
            try
            {
                return _personService.VerifyCid(citizen).Result;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}