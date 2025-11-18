using MicroServiceDiscipline.Domain.Entities;
using System.Linq;
using System.Text.RegularExpressions;
namespace MicroServiceDiscipline.Domain.Rules
{
    public static class DisciplineValidationRules
    {
        private static readonly Regex AllowedCharsRegex = new Regex("^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚüÜ]+$");
        private static TimeSpan HoraApertura = new TimeSpan(8, 0, 0); // 08:00 AM
        private static TimeSpan HoraCierre = new TimeSpan(19, 0, 0);  // 07:00 PM

        public static string? ValidateName(string name)
        {
            int letterCount = name.Count(char.IsLetter);
            int digitCount = name.Count(char.IsDigit);
            if (string.IsNullOrWhiteSpace(name))
                return "El nombre de la disciplina no puede estar vacío.";
            if (!AllowedCharsRegex.IsMatch(name))
                return "El nombre de la disciplina contiene caracteres no permitidos.";
            if (digitCount > 2)
                return "El nombre no puede contener más de 2 números.";
            if (digitCount > 0 && letterCount < 5)
                return "Si el nombre contiene números, debe estar acompañado por al menos 5 letras.";
            if (letterCount < 3)
                return "El nombre debe contener al menos 3 letras.";
            if (name.Length > 15)
                return "El nombre de la disciplina no puede exceder los 15 caracteres.";
            return null;
        }

        public static string? ValidateTimes(TimeSpan startTime, TimeSpan endTime)
        {
            var duration = endTime - startTime;
            if (endTime < startTime)
                return "La hora de finalización no puede ser anterior a la hora de inicio.";
            if (endTime == startTime)
                return "La hora de inicio y finalización no pueden ser la misma.";
            if (duration < TimeSpan.FromHours(1))
                return "La duración de la disciplina debe ser de al menos 1 hora.";
            if (duration > TimeSpan.FromHours(2))
                return "La duración de la disciplina no puede exceder las 2 horas.";
            if (startTime < HoraApertura || endTime > HoraCierre)
                return "El horario de la disciplina debe estar entre las 08:00 AM y las 07:00 PM.";
            return null;
        }
        public static string? ValidateDecription(string description)
        {
            int letterCount = description.Count(char.IsLetter);
            int digitCount = description.Count(char.IsDigit);
            if (string.IsNullOrWhiteSpace(description))
                return "La descripción de la disciplina no puede estar vacía.";
            if (!AllowedCharsRegex.IsMatch(description))
                return "La descripción de la disciplina contiene caracteres no permitidos.";
            if (digitCount > 3)
                return "La descripción no puede contener más de 3 números.";
            if (digitCount > 0 && letterCount < 5)
                return "Si la descripción contiene números, debe estar acompañado por al menos 5 letras.";
            if (letterCount < 3)
                return "La descripción debe contener al menos 3 letras.";
            if (description.Length > 50)
                return "La descripción de la disciplina no puede exceder los 50 caracteres.";
            return null;
        }
    }
}