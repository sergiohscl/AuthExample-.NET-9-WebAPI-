using AuthExample.Data;
using Microsoft.EntityFrameworkCore;
using AuthExample.Configurations;
using AuthExample.Middlewares;
using AuthExample.Services.Interfaces;
using AuthExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Chave JWT (mínimo 32 caracteres)
var key = "this_is_a_very_secure_jwt_key_1234567890";

// Configura o banco SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=users.db"));

// Adiciona serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddJwtAuthentication(key);
builder.Services.AddScoped<IUserService, UserService>();

// Configura CORS para acesso do Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Sempre ativa o Swagger (inclusive em produção)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = ""; // <- Swagger em "/"
});
//app.MapGet("/", () => Results.Redirect("/swagger"));

// Middleware de exceções
app.UseCustomExceptionHandler();

// CORS vem antes de MapControllers
app.UseCors("AllowAngularApp");

// Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapear os controllers
app.MapControllers();

// Inicia o app
app.Run();
