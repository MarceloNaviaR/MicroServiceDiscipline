using MicroServiceDiscipline.Application.Interfaces;
using MicroServiceDiscipline.Application.Services;
using MicroServiceDiscipline.Domain.Ports;
using MicroServiceDiscipline.Infrastructure.Persistence;
using MicroServiceDiscipline.Infrastructure.Provider; // 👈 Soluciona error CS0246

var builder = WebApplication.CreateBuilder(args);

// Configuración opcional para Dapper (ayuda con snake_case)
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ============================================================
// INYECCIÓN DE DEPENDENCIAS
// ============================================================

// 1. Connection Provider (Singleton es correcto para configuración)
builder.Services.AddSingleton<IDisciplineConnectionProvider, DisciplineConnectionProvider>();

// 2. Repositorio (Debe ser Scoped para conexiones a DB)
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();

// 3. Servicio de Aplicación
builder.Services.AddScoped<IDisciplineService, DisciplineService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Puedes descomentarlo si usas HTTPS local
app.UseAuthorization();
app.MapControllers();

app.Run();