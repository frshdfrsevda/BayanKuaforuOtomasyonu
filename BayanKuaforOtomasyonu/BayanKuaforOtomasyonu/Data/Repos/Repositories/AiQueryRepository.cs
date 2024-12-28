using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayanKuaforOtomasyonu.Data.Repos.Repositories
{
    public class AiQueryRepository : GenericRepository<AiQuery>, IAiQueryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<AiQuery> _aiQueryTable;
        public AiQueryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _aiQueryTable = _appDbContext.Set<AiQuery>();
        }
    }
}
