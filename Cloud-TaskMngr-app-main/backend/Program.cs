using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Dodajemy bazę danych do "budowniczego"
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=tcp:mojserwer.database.windows.net,1433;Initial Catalog=CloudDB;User ID=admin;Password=MojeHaslo123!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

// 2. Dodajemy zasady CORS do "budowniczego"
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy => 
        policy.SetIsOriginAllowed(origin => true) // Zezwala KAŻDEMU!
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

// 3. TUTAJ POWSTAJE ZMIENNA 'app'
var app = builder.Build();

// 4. Od tego momentu możemy używać zmiennej 'app'
app.UseSwagger();
app.UseSwaggerUI();

// Aplikujemy CORS i podpinamy kontrolery
app.UseCors("AllowFrontend");
app.MapControllers();

// Uruchamiamy na porcie 8080
app.Run("http://0.0.0.0:8080");

// Definicja klasy kontekstu
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}