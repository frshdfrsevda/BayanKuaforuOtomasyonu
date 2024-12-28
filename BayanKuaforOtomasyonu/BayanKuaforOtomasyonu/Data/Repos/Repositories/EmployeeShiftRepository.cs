using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayanKuaforOtomasyonu.Data.Repos.Repositories
{
    public class EmployeeShiftRepository : GenericRepository<EmployeeShift>, IEmployeeShiftRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<EmployeeShift> _employeeShiftTable;
        public EmployeeShiftRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _employeeShiftTable = _appDbContext.Set<EmployeeShift>();
        }
    }
}
