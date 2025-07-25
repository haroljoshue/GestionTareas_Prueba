using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Modelos
{
    public class AspNetRoles
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }

        public string? ConcurrencyStamp { get; set; }
    }
}
