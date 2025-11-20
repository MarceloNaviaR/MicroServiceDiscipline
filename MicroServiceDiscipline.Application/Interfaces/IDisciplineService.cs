using MicroServiceDiscipline.Application.Common;
using MicroServiceDiscipline.Domain.Entities;

namespace MicroServiceDiscipline.Application.Interfaces
{
    public interface IDisciplineService
    {
        Task<Result<IEnumerable<Discipline>>> GetAllAsync();
        Task<Result<Discipline>> GetByIdAsync(int id);
        Task<Result<int>> CreateAsync(Discipline discipline);
        Task<Result> UpdateAsync(Discipline discipline);
        Task<Result> DeleteAsync(int id);
    }
}