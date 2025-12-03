using CalendarBackend.Data;
using Microsoft.EntityFrameworkCore;
using UNVICal.Data; // for EventsDbContext

var builder = WebApplication.CreateBuilder(args);

// 🔹 DbContext konfiguracija
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Controllers
builder.Services.AddControllers();

// 🔹 CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins(
                "http://localhost:3000",          // dev React
                "http://localhost:59782",         // serve build
                "https://unvical.vercel.app",     // glavni produkcijski domen
                "https://unvical-7spr4i7sn-aldinas-projects.vercel.app" // trenutni Vercel deploy
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
        );
});

var app = builder.Build();

// 🔹 Middleware redoslijed
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();   // ako koristiš JWT/Identity
app.UseAuthorization();

app.MapControllers();

// 🔹 Port konfiguracija (Render koristi PORT env var)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
