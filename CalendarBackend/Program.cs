using CalendarBackend.Data;
using Microsoft.EntityFrameworkCore;
using UNVICal.Data; // za EventsDbContext

var builder = WebApplication.CreateBuilder(args);

// ✅ Registracija postojećeg AppDbContext-a (za korisnike)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 43)) // verzija MySQL servera
    ));

// ✅ Registracija novog EventsDbContext-a (za evente)
builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 43))
    ));

// ✅ Dodaj kontrolere
builder.Services.AddControllers();

// ✅ CORS da frontend (React na portu 3000) može komunicirati
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000") // URL frontenda
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// ✅ Omogući CORS
app.UseCors("AllowFrontend");

// ✅ Mapiraj kontrolere
app.MapControllers();

app.Run();
