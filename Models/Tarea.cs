using System.ComponentModel.DataAnnotations;

namespace gestor_tareas_api.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio")]
        [StringLength(200, ErrorMessage = "El titulo no debe superar los 200 cartacteres")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La descripcion no puede superar 1000 caracteres")]
        public string? Descripcion { get; set; } 
        
        public bool Completada { get; set; } = false;
        
        public DateTime FechaCreacion { get; set; }
        
        public DateTime? FechaCompletado { get; set; }
        
        [Required(ErrorMessage ="La prioridad es obligatoria")]
        public string Prioridad { get; set; } ="Media";

    
    }
}