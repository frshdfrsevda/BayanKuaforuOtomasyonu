using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayanKuaforOtomasyonu.Data.Repos.Repositories
{
    public class UserEmploymentRepository : GenericRepository<AppUserEmployement>, IUserEmploymentRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<AppUserEmployement> _userEmploymentTable;
        public UserEmploymentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _userEmploymentTable = _appDbContext.Set<AppUserEmployement>();
        }
    }
}
