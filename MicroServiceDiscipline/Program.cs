using MicroServiceDiscipline.Application.Interfaces;
using MicroServiceDiscipline.Application.Services;
using MicroServiceDiscipline.Domain.Ports;
using MicroServiceDiscipline.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de Dependencias
builder.Services.AddScoped<IDisciplineService, DisciplineService>();

// IMPORTANTE: Registrar el repositorio en memoria como Singleton para que los datos persistan entre llamadas.
// Esta es la causa más probable del "no guarda".
builder.Services.AddSingleton<IDisciplineRepository, DisciplineRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();