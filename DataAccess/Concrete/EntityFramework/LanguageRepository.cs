using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

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