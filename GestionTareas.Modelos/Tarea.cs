using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Modelos
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Prioridad { get; set; } // 1: Alta, 2: Media, 3: Baja
        [Required]
        public required string Estado { get; set; } // "Pendiente", "En Progreso", "Completada", "Cancelada"
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}
