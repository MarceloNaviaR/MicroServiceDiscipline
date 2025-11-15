using Microsoft.AspNetCore.Mvc;
using MicroServiceDiscipline.Domain.Entities;
using MicroServiceDiscipline.Domain.Ports;
namespace MicroServiceDiscipline.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class DisciplineController : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;
        public DisciplineController(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        // GET: api/disciplines
        // Obtiene todas las disciplinas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var disciplines = await _disciplineRepository.GetAllAsync();
            return Ok(disciplines);
        }

        // GET: api/disciplines/5
        // Obtiene una disciplina por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discipline = await _disciplineRepository.GetByIdAsync(id);
            if (discipline == null)
            {
                return NotFound(); // Devuelve 404 Not Found si no existe
            }
            return Ok(discipline);
        }

        // POST: api/disciplines
        // Crea una nueva disciplina
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Discipline discipline)
        {
            if (discipline == null)
            {
                return BadRequest(); // Devuelve 400 Bad Request si el cuerpo está vacío
            }

            var newId = await _disciplineRepository.AddAsync(discipline);
            // Devuelve 201 Created con la URL para obtener el nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = newId }, discipline);
        }

        // PUT: api/disciplines/5
        // Actualiza una disciplina existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Discipline discipline)
        {
            if (id != discipline.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo de la solicitud.");
            }

            var success = await _disciplineRepository.UpdateAsync(discipline);
            if (!success)
            {
                return NotFound(); // No se encontró el registro para actualizar
            }

            return NoContent(); // Devuelve 204 No Content, que es estándar para actualizaciones exitosas
        }

        // DELETE: api/disciplines/5
        // Elimina una disciplina por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _disciplineRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound(); // No se encontró el registro para eliminar
            }

            return NoContent(); // Devuelve 204 No Content, que es estándar para eliminaciones exitosas
        }
    }
}
