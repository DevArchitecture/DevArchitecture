
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.DataAccess.Concrete;
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
        public LanguageRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetLanguagesLookUp()
        {
            var lookUp = await (from entity in context.Languages
                         select new SelectionItem()
                         {
                             Id = entity.Id.ToString(),
                             Label = entity.Name
                         }).ToListAsync();
            return lookUp;
        }
    }
}
