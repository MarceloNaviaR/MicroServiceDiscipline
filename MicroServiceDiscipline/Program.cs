// using para clases de Application
using MicroServiceDiscipline.Application.Interfaces;
using MicroServiceDiscipline.Application.Services;

// using para clases de Domain
using MicroServiceDiscipline.Domain.Ports;

// using para clases de Infrastructure
using MicroServiceDiscipline.Infrastructure.Persistence; // O .Repository, según tu carpeta

var builder = WebApplication.CreateBuilder(args);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
// 1. REGISTRO DE SERVICIOS
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Inyección de Dependencias Manual (Estilo MicroservicioCliente) ---

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Capa de Infraestructura:
// Ahora 'DisciplineRepository' será encontrado gracias al 'using' de arriba.
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();

// Capa de Aplicación:
// Ahora 'DisciplineService' será encontrado gracias al 'using' de arriba.
builder.Services.AddScoped<IDisciplineService, DisciplineService>();

// --------------------------------------------------------------------

var app = builder.Build();

// 2. PIPELINE HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 3. EJECUCIÓN
app.Run();