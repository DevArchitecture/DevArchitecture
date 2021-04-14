namespace Tests.Helpers.Adapter
{
	using global::Business.Adapters.PersonService;

	using System.Net;
	using Entities.Dtos;

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
