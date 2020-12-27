
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface ILanguageRepository : IEntityRepository<Language>
    {
        Task<List<SelectionItem>> GetLanguagesLookUp();
    }
}