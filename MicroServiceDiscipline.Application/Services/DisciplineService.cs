using MicroServiceDiscipline.Application.Common;
using MicroServiceDiscipline.Application.Interfaces;
using MicroServiceDiscipline.Domain.Entities;
using MicroServiceDiscipline.Domain.Ports;
using MicroServiceDiscipline.Domain.Rules;

namespace MicroServiceDiscipline.Application.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineRepository _repo;
        public DisciplineService(IDisciplineRepository repo) { _repo = repo; }

        public async Task<Result<int>> CreateAsync(Discipline discipline)
        {
            var nameError = DisciplineValidationRules.ValidateName(discipline.Name);
            if (nameError != null) return Result.Failure<int>(nameError);

            var timeError = DisciplineValidationRules.ValidateTimes(discipline.StartTime, discipline.EndTime);
            if (timeError != null) return Result.Failure<int>(timeError);

            var userError = DisciplineValidationRules.ValidateUserId(discipline.UserId);
            if (userError != null) return Result.Failure<int>(userError);

            discipline.IsActive = true;
            discipline.CreatedAt = DateTime.UtcNow;

            var newId = await _repo.AddAsync(discipline);
            return Result.Success(newId);
        }

        public async Task<Result> UpdateAsync(Discipline discipline)
        {
            var nameError = DisciplineValidationRules.ValidateName(discipline.Name);
            if (nameError != null) return Result.Failure(nameError);

            var userError = DisciplineValidationRules.ValidateUserId(discipline.UserId);
            if (userError != null) return Result.Failure(userError);

            var existing = await _repo.GetByIdAsync(discipline.Id);
            if (existing == null) return Result.Failure($"No se encontró la disciplina con ID {discipline.Id}.");

            discipline.CreatedAt = existing.CreatedAt;
            discipline.CreatedBy = existing.CreatedBy;

            var success = await _repo.UpdateAsync(discipline);
            return success ? Result.Success() : Result.Failure("Error al actualizar la disciplina.");
        }

        public async Task<Result<Discipline>> GetByIdAsync(int id)
        {
            var discipline = await _repo.GetByIdAsync(id);
            return discipline != null ? Result.Success(discipline) : Result.Failure<Discipline>($"No se encontró la disciplina con ID {id}.");
        }

        public async Task<Result<IEnumerable<Discipline>>> GetAllAsync()
        {
            var disciplines = await _repo.GetAllAsync();
            return Result.Success(disciplines);
        }

        // --- MÉTODO AÑADIDO QUE SOLUCIONA EL ERROR ---
        public async Task<Result> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                return Result.Failure($"No se encontró la disciplina con ID {id} para eliminar.");
            }

            var success = await _repo.DeleteAsync(id);
            return success ? Result.Success() : Result.Failure("Ocurrió un error al eliminar la disciplina.");
        }
    }
}