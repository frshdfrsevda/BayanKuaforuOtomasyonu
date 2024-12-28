using BayanKuaforOtomasyonu.Data;
using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Data.Repos.Repositories;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Services.Abstracts;
using BayanKuaforOtomasyonu.Services.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // HttpClient'� ekle
//Database baglant�s�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Session
builder.Services.AddSession();


//Identity Servisi
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.SignIn.RequireConfirmedAccount = false;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    opt.Lockout.MaxFailedAccessAttempts = 5;
})
    .AddRoleManager<RoleManager<AppRole>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//Cookie ��lemleri
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "BayanKuaforuOtomasyonu";
    opt.ExpireTimeSpan = TimeSpan.FromDays(30); // ge�erlilik s�resi
    opt.SlidingExpiration = true; // s�resinin yenilenmesi
    opt.LoginPath = "/Access/Login"; // Giri�
    opt.LogoutPath = "/Access/LogOut"; // ��k�� 
    opt.AccessDeniedPath = "/Access/Denied"; // Eri�im engellendi
});

// Scopes
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IEmploymentService,EmploymentService>();
builder.Services.AddScoped<IEmploymentRepository,EmploymentRepository>();
builder.Services.AddScoped<IUserEmploymentRepository,UserEmploymentRepository>();
builder.Services.AddScoped<IUserEmploymentService, UserEmploymentService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationDetailRepository, ReservationDetailRepository>();
builder.Services.AddScoped<IReservationStatusRepository, ReservationStatusRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITrackingService, TrackingService>();
builder.Services.AddScoped<IAiQueryRepository, AiQueryRepository>();
builder.Services.AddScoped<IAiQueryService, AiQueryService>();
builder.Services.AddScoped<IEmployeeShiftRepository, EmployeeShiftRepository>();
builder.Services.AddScoped<IEmployeeShiftService, EmployeeShiftService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
