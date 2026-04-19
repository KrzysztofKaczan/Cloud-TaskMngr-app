using Microsoft.EntityFrameworkCore;
using CloudBackend.Data; 
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// --- INTEGRACJA Z KEY VAULT ---
var keyVaultUri = builder.Configuration["KeyVaultName"];

if (!string.IsNullOrEmpty(keyVaultUri))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri(keyVaultUri),
        new DefaultAzureCredential());
}

// Pobieramy Connection String (nazwa musi być identyczna jak w Key Vault!)
var connectionString = builder.Configuration["DbConnectionString"];

// Rejestracja bazy danych z mechanizmem ponawiania prób (ważne dla bazy Serverless!)
builder.Services.AddDbContext<CloudTaskDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => 
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, 
            maxRetryDelay: TimeSpan.FromSeconds(10), 
            errorNumbersToAdd: null)));

// --- KONFIGURACJA CORS ---
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- AUTOMATYCZNE MIGRACJE ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CloudTaskDbContext>();
    // Dzięki EnableRetryOnFailure, ta linijka poczeka, aż baza się obudzi
    dbContext.Database.Migrate();
}

// Kolejność Middleware jest ważna
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(); // Musi być przed MapControllers i Authorization

app.UseAuthorization();
app.MapControllers();

app.Run();