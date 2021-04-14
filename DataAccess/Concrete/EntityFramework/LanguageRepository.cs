using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System.Collections.Generic;
using Core.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class LanguageRepository : EfEntityRepositoryBase<Language, ProjectDbContext>, ILanguageRepository
    {
        public LanguageRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetLanguagesLookUp()
        {
            var lookUp = await (from entity in Context.Languages
                         select new SelectionItem()
                         {
                             Id = entity.Id,
                             Label = entity.Name
                         }).ToListAsync();
            return lookUp;
        }

        public async Task<List<SelectionItem>> GetLanguagesLookUpWithCode()
        {
            var lookUp = await (from entity in Context.Languages
                                select new SelectionItem()
                                {
                                    Id = entity.Code.ToString(),
                                    Label = entity.Name
                                }).ToListAsync();
            return lookUp;
        }
    }
}
