using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class CompanyRepository : EfEntityRepositoryBase<Company, ProjectDbContext>, ICompanyRepository
    {
        public CompanyRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
