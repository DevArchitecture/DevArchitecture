
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CompanyRepository : EfEntityRepositoryBase<Company, ProjectDbContext>, ICompanyRepository
    {
        public CompanyRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
