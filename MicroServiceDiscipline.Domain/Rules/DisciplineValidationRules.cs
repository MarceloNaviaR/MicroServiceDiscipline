using MicroServiceDiscipline.Domain.Entities;
using System.Linq;
using System.Text.RegularExpressions;
namespace MicroServiceDiscipline.Domain.Rules
{
    public static class DisciplineValidationRules
    {
        public static string? ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "El nombre no puede estar vacío.";
            if (name.Length > 50) // <-- CAMBIO CLAVE: Ajustado a VARCHAR(50)
                return "El nombre no puede tener más de 50 caracteres.";
            return null;
        }

        public static string? ValidateTimes(TimeSpan start, TimeSpan end)
        {
            if (end <= start)
                return "La hora de fin debe ser posterior a la hora de inicio.";
            return null;
        }

        // --- NUEVA REGLA ---
        public static string? ValidateUserId(int userId)
        {
            if (userId <= 0)
                return "Se debe especificar un instructor válido.";
            return null;
        }
    }
}