using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionTareas.Modelos;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GestionTareas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProyectosController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Proyectos
        [HttpGet]
        public IEnumerable<Proyecto> GetProyecto()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var proyectos = connection.Query<Proyecto>("SELECT * FROM Proyectos");
            return proyectos;
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public ActionResult<Proyecto> GetProyecto(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var proyecto = connection.QueryFirstOrDefault<Proyecto>("SELECT * FROM Proyectos WHERE Id = @Id", new { Id = id });

            if (proyecto == null)
            {
                return NotFound();
            }

            return proyecto;
        }

        // PUT: api/Proyectos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutProyecto(int id, Proyecto proyecto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("UPDATE Proyectos SET Nombre = @Nombre, Descripcion = @Descripcion, FechaCreacion = @FechaCreacion, FechaFinalizacion = @FechaFinalizacion, UsuarioId = @UsuarioId WHERE Id = @Id",
                new { Id = id, Nombre = proyecto.Nombre, Descripcion = proyecto.Descripcion, FechaCreacion = proyecto.FechaCreacion, FechaFinalizacion = proyecto.FechaFinalizacion, UsuarioId = proyecto.UsuarioId });
        }

        // POST: api/Proyectos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Proyecto PostProyecto(Proyecto proyecto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var id = connection.QuerySingle<int>("INSERT INTO Proyectos (Nombre, Descripcion, FechaCreacion, FechaFinalizacion, UsuarioId) OUTPUT INSERTED.Id VALUES (@Nombre, @Descripcion, @FechaCreacion, @FechaFinalizacion, @UsuarioId)",
                new { Nombre = proyecto.Nombre, Descripcion = proyecto.Descripcion, FechaCreacion = proyecto.FechaCreacion, FechaFinalizacion = proyecto.FechaFinalizacion, UsuarioId = proyecto.UsuarioId });
            return proyecto;
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public void DeleteProyecto(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("DELETE FROM Proyectos WHERE Id = @Id", new { Id = id });
        }

        /*private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }*/
    }
}
