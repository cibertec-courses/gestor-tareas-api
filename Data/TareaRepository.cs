using gestor_tareas_api.Models;
using Microsoft.EntityFrameworkCore;

namespace gestor_tareas_api.Data
{
    public class TareaRepository
    {
        private readonly ApplicationDbContext _context;

        public TareaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Logica Repository
        public List<Tarea> ObtenerTodas()
        {
            return _context.Tareas.ToList();
        }

        public Tarea? ObtenerPorId(int id)
        {
            return _context.Tareas.Find(id);
        }

        public Tarea Crear(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            _context.SaveChanges();
            return tarea;
        }

        public bool Actualizar(int id, Tarea tareaActualizada)
        {
            var tarea = ObtenerPorId(id);
            if (tarea == null)
                return false;

            tarea.Titulo = tareaActualizada.Titulo;
            tarea.Descripcion = tareaActualizada.Descripcion;
            tarea.Prioridad = tareaActualizada.Prioridad;

            _context.SaveChanges();

            return true;
        }

        public bool MarcarCompletada(int id)
        {
            var tarea = ObtenerPorId(id);
            if (tarea == null)
                return false;

            tarea.Completada = true;
            tarea.FechaCompletado = DateTime.Now;

            _context.SaveChanges();

            return true;
        }

        public bool Eliminar(int id)
        {
            var tarea = ObtenerPorId(id);
            if (tarea == null)
                return false;

            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            return true;
        }

        // Operaciones adicionales
        public List<Tarea> FiltrarPorEstado(bool completada)
        {
            return _context.Tareas
                            .Where(t => t.Completada == completada)
                            .ToList();
        }

        public List<Tarea> FiltrarPorPrioridad(string prioridad)
        {
            return _context.Tareas
                        .Where(t =>
                                    t.Prioridad.Equals(prioridad, StringComparison.OrdinalIgnoreCase))
                        .ToList();
        }


    }
}