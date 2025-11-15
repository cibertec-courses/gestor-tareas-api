using Microsoft.AspNetCore.Mvc;
using gestor_tareas_api.Models;
using gestor_tareas_api.Data;

namespace gestor_tareas_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly TareaRepository _repository;

        public TareasController()
        {
            _repository = new TareaRepository();
        }

        [HttpGet]
        public ActionResult<List<Tarea>> ObtenerTodas([FromQuery] string? prioridad, [FromQuery] bool? completada) /// /api/tareas?prioridad="Alta"
        {
            if(!string.IsNullOrEmpty(prioridad))
            {
                return Ok(_repository.FiltrarPorPrioridad(prioridad));
            }
            if (completada.HasValue)
            {
                return Ok(_repository.FiltrarPorEstado(completada.Value));
            }
            return Ok(_repository.ObtenerTodas());
        }

        [HttpGet("{id}")]
        public ActionResult<Tarea> ObtenerPorId(int id)
        {
            var tarea = _repository.ObtenerPorId(id);
            return tarea == null? NotFound(new {message =  $"Tarea {id} no encontrada"}): Ok(tarea);
        }

        // POST 
        [HttpPost]
        public ActionResult<Tarea> Crear([FromBody] Tarea tarea)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var tareaCreada = _repository.Crear(tarea);
            return CreatedAtAction( 
                nameof(ObtenerPorId),
                new {Id = tareaCreada.Id},
                tareaCreada
            );
        }

        //PUt
        [HttpPut("{id}")]
        public ActionResult Actualizar(int id, [FromBody] Tarea  tarea)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = _repository.Actualizar(id, tarea);

            if(!actualizado)
                return  NotFound(new {message = $"Tarea con ID {id} no encontrada"});
            return NoContent();
        }

        // PATCH
        [HttpPatch("{id}/completar")]
        public ActionResult MarcarCompletada(int id)
        {
            var resultado  = _repository.MarcarCompletada(id);
            if(!resultado)
                return NotFound(new {message = $"Tarea con ID {id} no encontrada"});
            
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            var eliminado = _repository.Eliminar(id);
            if(!eliminado)
                return NotFound(new {message = $"Tarea con ID {id} no encontrada"});
            
            return NoContent();
        }

    }
}