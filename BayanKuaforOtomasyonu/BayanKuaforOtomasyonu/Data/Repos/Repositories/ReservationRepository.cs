using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayanKuaforOtomasyonu.Data.Repos.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Reservation> _reservationTable;
        public ReservationRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _reservationTable = _appDbContext.Set<Reservation>();
        }
    }
}
