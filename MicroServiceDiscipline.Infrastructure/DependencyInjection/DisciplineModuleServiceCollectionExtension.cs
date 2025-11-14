using MicroServiceDiscipline.Application.Interfaces;
using MicroServiceDiscipline.Application.Services;
using MicroServiceDiscipline.Domain.Ports;
using MicroServiceDiscipline.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServiceDiscipline.Infrastructure.DependencyInjection
{
    public static class DisciplineModuleServiceCollectionExtensions
    {
        public static IServiceCollection AddDisciplineModule(this IServiceCollection services)
        {
            services.AddScoped<IDisciplineRepository, DisciplineRepository>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            return services;
        }
    }
}