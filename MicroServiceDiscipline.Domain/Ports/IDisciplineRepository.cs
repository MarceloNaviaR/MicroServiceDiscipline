using MicroServiceDiscipline.Domain.Entities;

namespace MicroServiceDiscipline.Domain.Ports
{
    public interface IDisciplineRepository
    {
        Task<IEnumerable<Discipline>> GetAllAsync();
        Task<Discipline?> GetByIdAsync(int id);
        Task<int> AddAsync(Discipline discipline);
        Task<bool> UpdateAsync(Discipline discipline);
        Task<bool> DeleteAsync(int id);
    }
}