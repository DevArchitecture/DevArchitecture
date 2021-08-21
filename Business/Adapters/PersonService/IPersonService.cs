using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Adapters.PersonService
{
    public interface IPersonService
    {
        Task<bool> VerifyCid(Citizen citizen);
    }
}