using MicroServiceDiscipline.Domain.Entities;
using MicroServiceDiscipline.Domain.Ports;

namespace MicroServiceDiscipline.Infrastructure.Persistence
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private static readonly List<Discipline> _disciplines = new()
        {
            new Discipline { Id = 1, Name = "Yoga", Description = "Clases de relajación.", StartTime = new(8,0,0), EndTime = new(9,0,0), InstructorId = 101, CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new Discipline { Id = 2, Name = "Spinning", Description = "Clases de ciclismo.", StartTime = new(18,0,0), EndTime = new(19,0,0), InstructorId = 102, CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
        };
        private static int _nextId = 3;

        public Task<int> AddAsync(Discipline discipline)
        {
            // <-- PUNTO DE INTERRUPCIÓN 3 AQUÍ
            discipline.Id = _nextId++;
            _disciplines.Add(discipline);
            return Task.FromResult(discipline.Id);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = _disciplines.FirstOrDefault(d => d.Id == id);
            return item != null ? Task.FromResult(_disciplines.Remove(item)) : Task.FromResult(false);
        }

        public Task<IEnumerable<Discipline>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Discipline>>(_disciplines);
        }

        public Task<Discipline?> GetByIdAsync(int id)
        {
            return Task.FromResult(_disciplines.FirstOrDefault(d => d.Id == id));
        }

        public Task<bool> UpdateAsync(Discipline discipline)
        {
            var index = _disciplines.FindIndex(d => d.Id == discipline.Id);
            if (index == -1) return Task.FromResult(false);
            _disciplines[index] = discipline;
            return Task.FromResult(true);
        }
    }
}