namespace DataAccess.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.DataAccess;
    using Core.Entities.Concrete;
    using Core.Entities.Dtos;

    public interface ILanguageRepository : IEntityRepository<Language>
    {
        Task<List<SelectionItem>> GetLanguagesLookUp();
        Task<List<SelectionItem>> GetLanguagesLookUpWithCode();

    }
}