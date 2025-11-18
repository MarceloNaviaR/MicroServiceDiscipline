using System.ComponentModel.DataAnnotations;
namespace MicroServiceDiscipline.Domain.Entities
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? InstructorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? LastModification { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}