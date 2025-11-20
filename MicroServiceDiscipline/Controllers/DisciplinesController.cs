using MicroServiceDiscipline.Application.Interfaces;
using MicroServiceDiscipline.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceDiscipline.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok((await _disciplineService.GetAllAsync()).Value);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _disciplineService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Discipline discipline)
        {
            // <-- PUNTO DE INTERRUPCIÓN 2 AQUÍ
            discipline.CreatedBy = "api.user";
            var result = await _disciplineService.CreateAsync(discipline);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            var newDiscipline = (await _disciplineService.GetByIdAsync(result.Value)).Value;
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, newDiscipline);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Discipline discipline)
        {
            if (id != discipline.Id) return BadRequest("ID de la ruta no coincide con el ID del cuerpo.");
            discipline.LastModifiedBy = "api.user";
            var result = await _disciplineService.UpdateAsync(discipline);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _disciplineService.DeleteAsync(id);
            return result.IsSuccess ? NoContent() : NotFound(result.Error);
        }
    }
}