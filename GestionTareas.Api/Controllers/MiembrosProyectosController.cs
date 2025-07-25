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
    public class MiembrosProyectosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public MiembrosProyectosController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/MiembrosProyectos
        [HttpGet]
        public IEnumerable<MiembrosProyecto> GetMiembrosProyecto()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var miembrosProyectos = connection.Query<MiembrosProyecto>("SELECT * FROM MiembrosProyectos");
            return miembrosProyectos;

        }

        // GET: api/MiembrosProyectos/5
        [HttpGet("{id}")]
        public ActionResult<MiembrosProyecto> GetMiembrosProyecto(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var miembrosProyecto = connection.QueryFirstOrDefault<MiembrosProyecto>("SELECT * FROM MiembrosProyectos WHERE Id = @Id", new { Id = id });

            if (miembrosProyecto == null)
            {
                return NotFound();
            }

            return miembrosProyecto;
        }

        // PUT: api/MiembrosProyectos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutMiembrosProyecto(int id, MiembrosProyecto miembrosProyecto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("UPDATE MiembrosProyectos SET UsuarioId = @UsuarioId, ProyectoId = @ProyectoId WHERE Id = @Id",
                new { Id = id, UsuarioId = miembrosProyecto.UsuarioId, ProyectoId = miembrosProyecto.ProyectoId });
        }

        // POST: api/MiembrosProyectos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public MiembrosProyecto PostMiembrosProyecto(MiembrosProyecto miembrosProyecto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("INSERT INTO MiembrosProyectos (UsuarioId, ProyectoId) VALUES (@UsuarioId, @ProyectoId)",
                new { UsuarioId = miembrosProyecto.UsuarioId, ProyectoId = miembrosProyecto.ProyectoId });
            return miembrosProyecto;
        }

        // DELETE: api/MiembrosProyectos/5
        [HttpDelete("{id}")]
        public void DeleteMiembrosProyecto(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("DELETE FROM MiembrosProyectos WHERE Id = @Id", new { Id = id });
        }

        /*private bool MiembrosProyectoExists(int id)
        {
            return _context.MiembrosProyectos.Any(e => e.Id == id);
        }*/
    }
}
