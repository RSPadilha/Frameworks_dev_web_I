using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using DotNetEnv;

Env.Load();


var builder = WebApplication.CreateBuilder(args);

// Configura CORS para liberar todos os endereços
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configura a porta do Render se existir a variável de ambiente PORT
// Testando
// var port = Environment.GetEnvironmentVariable("PORT");
// if (!string.IsNullOrEmpty(port))
//     builder.WebHost.UseUrls($"http://*:{port}");

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

// Usa a política CORS criada
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.UseAuthorization();

// Rota para o path raiz
app.MapGet("/", () => new
{
    message = "API está funcionando!",
    version = "1.0.0",
    timestamp = DateTime.UtcNow,
    rotas = new
    {
        usuarios = new
        {
            get_all = "GET /api/usuarios",
            get_by_id = "GET /api/usuarios/{id}",
            create = "POST /api/usuarios",
            update = "PUT /api/usuarios/{id}",
            delete = "DELETE /api/usuarios/{id}"
        },
        enderecos = new
        {
            get_all = "GET /api/enderecos",
            get_by_id = "GET /api/enderecos/{id}",
            get_by_usuario = "GET /api/enderecos/usuario/{usuarioId}",
            create = "POST /api/enderecos",
            update = "PUT /api/enderecos/{id}",
            delete = "DELETE /api/enderecos/{id}"
        },
        servicos = new
        {
            get_all = "GET /api/servicos",
            get_by_id = "GET /api/servicos/{id}",
            create = "POST /api/servicos",
            update = "PUT /api/servicos/{id}",
            delete = "DELETE /api/servicos/{id}"
        },
        pedidos = new
        {
            get_all = "GET /api/pedidos",
            get_by_id = "GET /api/pedidos/{id}",
            get_by_cliente = "GET /api/pedidos/cliente/{clienteId}",
            get_by_atendente = "GET /api/pedidos/atendente/{atendenteId}",
            create = "POST /api/pedidos",
            update = "PUT /api/pedidos/{id}",
            delete = "DELETE /api/pedidos/{id}"
        },
        historicos = new
        {
            get_all = "GET /api/historicos",
            get_by_id = "GET /api/historicos/{id}",
            get_by_pedido = "GET /api/historicos/pedido/{pedidoId}",
            create = "POST /api/historicos",
            update = "PUT /api/historicos/{id}",
            delete = "DELETE /api/historicos/{id}"
        },
        chats = new
        {
            get_all = "GET /api/chats",
            get_by_id = "GET /api/chats/{id}",
            get_by_pedido = "GET /api/chats/pedido/{pedidoId}",
            create = "POST /api/chats",
            update = "PUT /api/chats/{id}",
            delete = "DELETE /api/chats/{id}"
        }
    }
});

app.MapControllers();

app.Run();
