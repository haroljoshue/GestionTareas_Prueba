using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaRegistro { get; set; }
        [Required]
        public required string UserId { get; set; }
        public List<Tarea> Tareas { get; set; } = new List<Tarea>();
        public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        public List<MiembrosProyecto> Miembros{ get; set; } = new List<MiembrosProyecto>();

    }
}
