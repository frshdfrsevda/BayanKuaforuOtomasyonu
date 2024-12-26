using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayanKuaforOtomasyonu.Data.Repos.Repositories
{
    public class ReservationDetailRepository : GenericRepository<ReservationDetail>, IReservationDetailRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<ReservationDetail> _reservationDetailTable;
        public ReservationDetailRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _reservationDetailTable = _appDbContext.Set<ReservationDetail>();
        }
    }
}
