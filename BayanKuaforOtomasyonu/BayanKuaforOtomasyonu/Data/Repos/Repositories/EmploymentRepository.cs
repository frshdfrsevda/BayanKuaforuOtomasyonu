using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayanKuaforOtomasyonu.Data.Repos.Repositories
{
    public class EmploymentRepository : GenericRepository<Employment>, IEmploymentRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Employment> _employmentTable;
        public EmploymentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _employmentTable = _appDbContext.Set<Employment>();
        }
    }
}
