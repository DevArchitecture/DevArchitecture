using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface ITranslateRepository : IEntityRepository<Translate>
    {
        Task<List<TranslateDto>> GetTranslateDto();
        Task<Dictionary<string, string>> GetTranslateWordList(string lang);
        Task<string> GetTranslatesByLang(string langCode);
    }
}