using CalendarBackend.Data;
using Microsoft.EntityFrameworkCore;
using UNVICal.Data; // for EventsDbContext

var builder = WebApplication.CreateBuilder(args);

// ✅ Register existing AppDbContext (for users) with Postgres
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register new EventsDbContext (for events) with Postgres
builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add controllers
builder.Services.AddControllers();

// ✅ CORS so frontend (React local + Vercel) can communicate
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins(
                "http://localhost:3000",                                   // local dev
                "https://unvical-7e34dgx82-aldinas-projects.vercel.app",   // deployed frontend URL (deploy 1)
                "https://unvical-g0d8cwpor-aldinas-projects.vercel.app"    // deployed frontend URL (deploy 2)
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

// ✅ Middleware order
app.UseRouting();
app.UseCors("AllowFrontend");   // mora biti odmah nakon UseRouting
app.UseAuthorization();

// ✅ Map controllers
app.MapControllers();

// ✅ Configure PORT for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
