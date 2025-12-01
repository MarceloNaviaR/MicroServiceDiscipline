using MicroServiceDiscipline.Domain.Entities;
using MicroServiceDiscipline.Domain.Ports;

namespace MicroServiceDiscipline.Infrastructure.Persistence
{
    public class DisciplineRepository : IDisciplineRepository
    {
        // Datos de ejemplo actualizados según el nuevo esquema
        private static readonly List<Discipline> _disciplines = new()
        {
            new Discipline { Id = 1, Name = "Yoga Matutino", UserId = 101, StartTime = new(8,0,0), EndTime = new(9,0,0), IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new Discipline { Id = 2, Name = "Spinning Tarde", UserId = 102, StartTime = new(18,0,0), EndTime = new(19,0,0), IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new Discipline { Id = 3, Name = "Boxeo (Nocturno)", UserId = 101, StartTime = new(20,0,0), EndTime = new(21,0,0), IsActive = false, CreatedAt = DateTime.UtcNow, CreatedBy = "system" }
        };
        private static int _nextId = 4;

        // --- MÉTODO CORREGIDO ---
        public Task<int> AddAsync(Discipline discipline)
        {
            discipline.Id = _nextId++;
            _disciplines.Add(discipline);
            return Task.FromResult(discipline.Id);
        }

        // --- MÉTODO CORREGIDO ---
        public Task<bool> DeleteAsync(int id)
        {
            var item = _disciplines.FirstOrDefault(d => d.Id == id);
            if (item != null)
            {
                // El método Remove de una lista devuelve un booleano (true si lo encontró y eliminó)
                return Task.FromResult(_disciplines.Remove(item));
            }
            // Si no se encontró el ítem, la eliminación falla
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Discipline>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Discipline>>(_disciplines);
        }

        public Task<Discipline?> GetByIdAsync(int id)
        {
            return Task.FromResult(_disciplines.FirstOrDefault(d => d.Id == id));
        }

        // --- LÓGICA COMPLETADA ---
        public Task<bool> UpdateAsync(Discipline discipline)
        {
            var index = _disciplines.FindIndex(d => d.Id == discipline.Id);
            if (index != -1)
            {
                // Reemplaza el objeto antiguo en la lista con el nuevo
                _disciplines[index] = discipline;
                return Task.FromResult(true); // Actualización exitosa
            }
            return Task.FromResult(false); // No se encontró el objeto para actualizar
        }
    }
}