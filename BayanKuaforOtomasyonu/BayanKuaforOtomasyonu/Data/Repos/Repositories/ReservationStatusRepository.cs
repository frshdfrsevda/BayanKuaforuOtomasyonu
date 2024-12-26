using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayanKuaforOtomasyonu.Data.Repos.Repositories
{
    public class ReservationStatusRepository : GenericRepository<ReservationStatus>, IReservationStatusRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<ReservationStatus> _reservationStatusTable;
        public ReservationStatusRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _reservationStatusTable = _appDbContext.Set<ReservationStatus>();
        }
    }
}
