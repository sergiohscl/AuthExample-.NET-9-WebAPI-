using AuthExample.Data;
using Microsoft.EntityFrameworkCore;
using AuthExample.Configurations;
using AuthExample.Middlewares;
using AuthExample.Services.Interfaces;
using AuthExample.Services;

var builder = WebApplication.CreateBuilder(args);
var key = "this_is_a_very_secure_jwt_key_1234567890"; // ≥ 32 caracteres

// Adiciona o contexto com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=users.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddJwtAuthentication(key);
builder.Services.AddScoped<IUserService, UserService>();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();
app.MapControllers();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();
app.Run();