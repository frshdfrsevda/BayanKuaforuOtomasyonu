using BayanKuaforOtomasyonu.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BayanKuaforOtomasyonu.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // AppUserEmployement ile Employment arasındaki ilişki
            modelBuilder.Entity<AppUserEmployement>()
                .HasOne(ae => ae.Employment)  
                .WithMany(e => e.AppUserEmployements)
                .HasForeignKey(ae => ae.EmploymentId) 
                .OnDelete(DeleteBehavior.Restrict);

            // AppUserEmployement ile AppUser arasındaki ilişki
            modelBuilder.Entity<AppUserEmployement>()
                .HasOne(ae => ae.AppUser)
                .WithMany(e => e.AppUserEmployements)
                .HasForeignKey(ae => ae.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AppUser ile Reservation arasındaki ilişki
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.AppUser)
                .WithMany(au => au.Reservations)
                .HasForeignKey(r => r.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ReservationDetail ile Reservation arasındaki ilişki
            modelBuilder.Entity<ReservationDetail>()
                .HasOne(rd => rd.Reservation)
                .WithMany(r => r.ReservationDetails)
                .HasForeignKey(rd => rd.ResId)
                .OnDelete(DeleteBehavior.Restrict);

            // ReservationDetail ile AppUserEmployement arasındaki ilişki
            modelBuilder.Entity<ReservationDetail>()
               .HasOne(rd => rd.AppUserEmployement)
               .WithMany(aue => aue.ReservationDetails)
               .HasForeignKey(rd => rd.AppUserEmploymentId)
               .OnDelete(DeleteBehavior.Restrict);


        }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<AppUserEmployement> AppUserEmployement { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationDetail> ReservationDetails { get; set; }
    }
}
