using MicroServiceDiscipline.Application.Common;
using MicroServiceDiscipline.Application.Interfaces;
using MicroServiceDiscipline.Domain.Entities;
using MicroServiceDiscipline.Domain.Ports;
using MicroServiceDiscipline.Domain.Rules;

namespace MicroServiceDiscipline.Application.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineRepository _disciplineRepository;

        public DisciplineService(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        public async Task<Result<int>> CreateAsync(Discipline discipline)
        {
            var nameValidationError = DisciplineValidationRules.ValidateName(discipline.Name);
            if (nameValidationError != null) return Result<int>.Failure(nameValidationError);

            var timeValidationError = DisciplineValidationRules.ValidateTimes(discipline.StartTime, discipline.EndTime);
            if (timeValidationError != null) return Result<int>.Failure(timeValidationError);

            discipline.CreatedAt = DateTime.UtcNow;
            var newId = await _disciplineRepository.AddAsync(discipline);
            return Result<int>.Success(newId);
        }

        public async Task<Result> UpdateAsync(Discipline discipline)
        {
            var nameValidationError = DisciplineValidationRules.ValidateName(discipline.Name);
            if (nameValidationError != null) return Result.Failure(nameValidationError);

            var existingDiscipline = await _disciplineRepository.GetByIdAsync(discipline.Id);
            if (existingDiscipline is null) return Result.Failure($"No se encontró la disciplina con ID {discipline.Id}.");

            discipline.LastModification = DateTime.UtcNow;
            var success = await _disciplineRepository.UpdateAsync(discipline);
            return success ? Result.Success() : Result.Failure("Error al actualizar la disciplina.");
        }

        public async Task<Result<Discipline>> GetByIdAsync(int id)
        {
            var discipline = await _disciplineRepository.GetByIdAsync(id);
            return discipline != null ? Result<Discipline>.Success(discipline) : Result<Discipline>.Failure($"No se encontró la disciplina con ID {id}.");
        }

        public async Task<Result<IEnumerable<Discipline>>> GetAllAsync()
        {
            var disciplines = await _disciplineRepository.GetAllAsync();
            return Result<IEnumerable<Discipline>>.Success(disciplines);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var success = await _disciplineRepository.DeleteAsync(id);
            return success ? Result.Success() : Result.Failure("Error al eliminar la disciplina.");
        }
    }
}