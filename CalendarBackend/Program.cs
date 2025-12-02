using CalendarBackend.Data;
using Microsoft.EntityFrameworkCore;
using UNVICal.Data; // for EventsDbContext

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins(
                "http://localhost:3000",
                "https://unvical-7e34dgx82-aldinas-projects.vercel.app",
                "https://unvical-g0d8cwpor-aldinas-projects.vercel.app",
                "https://unvical.vercel.app"   // bez kosog znaka
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

app.UseHttpsRedirection();      // ✅ dodaj ovo
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();        // ✅ ako koristiš JWT/Identity
app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
