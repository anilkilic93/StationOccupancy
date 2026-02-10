using MediatR;

using Microsoft.EntityFrameworkCore;
using StationOccupancy.Web.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// CQRS / MediatR
builder.Services.AddMediatR(typeof(StationOccupancy.Web.Features.Stations.Commands.CreateStation.CreateStationCommand));

// Persistence (EF Core Sqlite)
builder.Services.AddDbContext<StationOccupancyDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EfStationRepository>();

builder.Services.AddScoped<IStationReadRepository>(sp =>sp.GetRequiredService<EfStationRepository>());

builder.Services.AddScoped<IStationWriteRepository>(sp =>sp.GetRequiredService<EfStationRepository>());

var app = builder.Build();

// Ensure DB exists (bootstrap)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StationOccupancyDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Attribute-routed controllers (e.g., ApiController)
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
