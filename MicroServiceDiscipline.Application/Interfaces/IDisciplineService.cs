using MicroServiceDiscipline.Application.Common;
using MicroServiceDiscipline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceDiscipline.Application.Interfaces
{
    public interface IDisciplineService
    {
        Task<Result<Discipline>> GetByIdAsync(int id);
        Task<Result<IEnumerable<Discipline>>> GetAllAsync();
        Task<Result<int>> CreateAsync(Discipline discipline);
        Task<Result> UpdateAsync(Discipline discipline);
        Task<Result> DeleteAsync(int id);
    }
}