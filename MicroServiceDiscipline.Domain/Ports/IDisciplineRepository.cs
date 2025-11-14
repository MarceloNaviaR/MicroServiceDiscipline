// *** ESTA LÍNEA ES LA SOLUCIÓN ***
// Le dice a este archivo dónde encontrar la definición de la clase 'Discipline'.
using MicroServiceDiscipline.Domain.Entities;

namespace MicroServiceDiscipline.Domain.Ports
{
    public interface IDisciplineRepository
    {
        Task<Discipline?> GetByIdAsync(int id);
        Task<IEnumerable<Discipline>> GetAllAsync();
        Task<int> AddAsync(Discipline discipline);
        Task<bool> UpdateAsync(Discipline discipline);
        Task<bool> DeleteAsync(int id);
    }
}