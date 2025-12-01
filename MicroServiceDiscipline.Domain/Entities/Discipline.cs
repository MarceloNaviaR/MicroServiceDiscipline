namespace MicroServiceDiscipline.Domain.Entities
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // --- CAMBIOS CLAVE ---
        public int UserId { get; set; } // Reemplaza a InstructorId, ahora es NOT NULL
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } // Nuevo campo

        // --- CAMPOS DE AUDITORÍA ACTUALIZADOS ---
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        // LastModification y LastModifiedBy se han eliminado
    }
}