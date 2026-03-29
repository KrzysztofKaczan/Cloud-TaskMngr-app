using Microsoft.EntityFrameworkCore;
using CloudBackend.Data; 

var builder = WebApplication.CreateBuilder(args);

// Bardzo ważne: Dodajemy obsługę CORS, żeby React nie miał problemów z połączeniem!
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CloudTaskDbContext>(options =>
    options.UseSqlServer("Server=db;Database=OstateczneDB;User Id=sa;Password=TwojeHaslo123!;TrustServerCertificate=True;"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CloudTaskDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors(); // <-- Uruchamiamy CORS
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();