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
    public class TareasController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TareasController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Tareas
        [HttpGet]
        public IEnumerable<Tarea> GetTarea()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var tareas = connection.Query<Tarea>("SELECT * FROM Tareas");
            return tareas;
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public ActionResult<Tarea> GetTarea(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var tarea = connection.QueryFirstOrDefault<Tarea>("SELECT * FROM Tareas WHERE Id = @Id", new { Id = id });

            if (tarea == null)
            {
                return NotFound();
            }

            return tarea;
        }

        // PUT: api/Tareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutTarea(int id, Tarea tarea)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("UPDATE Tareas SET Nombre = @Nombre, Descripcion = @Descripcion, FechaInicio = @FechaInicio, FechaFin = @FechaFin, Prioridad = @Prioridad, Estado = @Estado, UsuarioId = @UsuarioId, ProyectoId = @ProyectoId WHERE Id = @Id",
                new { Id = id, Nombre = tarea.Nombre, Descripcion = tarea.Descripcion, FechaInicio = tarea.FechaInicio, FechaFin = tarea.FechaFin, Prioridad = tarea.Prioridad, Estado = tarea.Estado, UsuarioId = tarea.UsuarioId, ProyectoId = tarea.ProyectoId });
        }

        // POST: api/Tareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Tarea PostTarea(Tarea tarea)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var id = connection.QuerySingle<int>("INSERT INTO Tareas (Nombre, Descripcion, FechaInicio, FechaFin, Prioridad, Estado, UsuarioId, ProyectoId) OUTPUT INSERTED.Id VALUES (@Nombre, @Descripcion, @FechaInicio, @FechaFin, @Prioridad, @Estado, @UsuarioId, @ProyectoId)",
                new { Nombre = tarea.Nombre, Descripcion = tarea.Descripcion, FechaInicio = tarea.FechaInicio, FechaFin = tarea.FechaFin, Prioridad = tarea.Prioridad, Estado = tarea.Estado, UsuarioId = tarea.UsuarioId, ProyectoId = tarea.ProyectoId });
            return tarea;
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public void DeleteTarea(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("DELETE FROM Tareas WHERE Id = @Id", new { Id = id });
        }

        /*private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }*/
    }
}
