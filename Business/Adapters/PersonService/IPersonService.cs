namespace Business.Adapters.PersonService
{
    using System.Threading.Tasks;
    using Entities.Dtos;

    public interface IPersonService
    {
        Task<bool> VerifyCid(Citizen citizen);
    }
}
