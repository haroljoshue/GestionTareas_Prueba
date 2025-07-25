using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Modelos
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public List<Tarea>? Tareas { get; set; } = new List<Tarea>();
        public List<MiembrosProyecto>? Miembros { get; set; } = new List<MiembrosProyecto>();

    }
}
