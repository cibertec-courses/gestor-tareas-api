using gestor_tareas_api.Models;

namespace gestor_tareas_api.Data
{
    public class TareaRepository
    {
        // Data en memoria
        private static List<Tarea> _tareas = new List<Tarea>
        {
            new Tarea { Id = 1, Titulo="Implementar API REST", Descripcion="Crear servicio web con ASP.net core", Prioridad="Alta", Completada=false},
            new Tarea { Id = 2, Titulo="Documentar Endpoints", Descripcion="Agregar documentacion Swagger", Prioridad="Media", Completada=true, FechaCompletado=DateTime.Now.AddDays(-1)},
            new Tarea { Id = 3, Titulo="Configurar base de datos", Descripcion="Integrar Entity Frameword", Prioridad="Baja", Completada=false}
        };
        private static int _siguienteId =4;

        // Logica Repository
        public List<Tarea> Tareas()
        {
            return _tareas;
        }

        public Tarea? ObtenerPorId (int id)
        {
            return _tareas.FirstOrDefault( t => t.Id == id);
        }

        public Tarea Crear(Tarea tarea)
        {
            tarea.Id = _siguienteId++;
            tarea.FechaCreacion = DateTime.Now;
            _tareas.Add(tarea);
            return tarea;
        }

        public bool Actualizar(int id, Tarea tareaActualizada)
        {
            var tarea = ObtenerPorId(id);
            if(tarea == null)
                return false;
            
            tarea.Titulo = tareaActualizada.Titulo;
            tarea.Descripcion = tareaActualizada.Descripcion;
            tarea.Prioridad = tareaActualizada.Prioridad;

            return true;
        }
        
        public bool MarcarCompletada (int id)
        {
            var tarea = ObtenerPorId(id);
            if (tarea == null)
                return false;
            
            tarea.Completada = true;
            tarea.FechaCompletado = DateTime.Now;
            return true;
        }

        public bool Eliminar(int id)
        {
            var tarea = ObtenerPorId(id);
            if(tarea == null)
                return false;
            
            _tareas.Remove(tarea);
            return true;
        }

        // Operaciones adicionales
        public List<Tarea> FiltrarPorEstado(bool completada)
        {
            return _tareas.Where( t => t.Completada ==  completada).ToList();
        }

        public List<Tarea> FiltrarPorPrioridad(string prioridad)
        {
            return _tareas.Where( t => t.Prioridad.Equals(prioridad, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        
    }
}