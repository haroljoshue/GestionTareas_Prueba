using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Modelos
{
    public class MiembrosProyecto
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }
        public Usuario? Usuario { get; set; }
        public Proyecto? Proyecto { get; set; }

    }
}
