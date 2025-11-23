using CalendarBackend.Data;
using Microsoft.EntityFrameworkCore;
using UNVICal.Data; // for EventsDbContext

var builder = WebApplication.CreateBuilder(args);

// ✅ Register existing AppDbContext (for users)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 43))
    ));

// ✅ Register new EventsDbContext (for events)
builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 43))
    ));

// ✅ Add controllers
builder.Services.AddControllers();

// ✅ CORS so frontend (React local + Vercel) can communicate
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins(
                "http://localhost:3000",                                   // local dev
                "https://unvical-7e34dgx82-aldinas-projects.vercel.app"    // deployed frontend URL
            )
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// ✅ Middleware order
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();

// ✅ Map controllers
app.MapControllers();

// ✅ Configure PORT for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
