namespace MicroServiceDiscipline.Domain.Rules
{
    public static class DisciplineValidationRules
    {
        public static string? ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "El nombre de la disciplina no puede estar vacío.";
            if (name.Length > 50)
                return "El nombre de la disciplina no puede exceder los 50 caracteres.";
            return null;
        }

        public static string? ValidateTimes(TimeSpan start, TimeSpan end)
        {
            if (end <= start)
                return "La hora de finalización debe ser posterior a la hora de inicio.";
            return null;
        }
    }
}