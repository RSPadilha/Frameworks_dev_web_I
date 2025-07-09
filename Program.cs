using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Configura a porta do Render se existir a variável de ambiente PORT
// Testando
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
   builder.WebHost.UseUrls($"http://*:{port}");

// Add services to the container.
builder.Services.AddControllers();

// Configura o DbContext com a string de conexão do appsettings.json
var baseConnStr = builder.Configuration.GetConnectionString("DefaultConnection");
if (baseConnStr == null)
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connStr = baseConnStr.Replace("Password", $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
