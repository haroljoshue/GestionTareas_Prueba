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
    public class LogEventosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LogEventosController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/LogEventos
        [HttpGet]
        public IEnumerable<LogEvento> GetLogEvento()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var logEventos = connection.Query<LogEvento>("SELECT * FROM LogEventos");
            return logEventos;

        }

        // GET: api/LogEventos/5
        [HttpGet("{id}")]
        public ActionResult<LogEvento> GetLogEvento(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var logEvento = connection.QueryFirstOrDefault<LogEvento>("SELECT * FROM LogEventos WHERE Id = @Id", new { Id = id });

            if (logEvento == null)
            {
                return NotFound();
            }

            return logEvento;
        }

        // PUT: api/LogEventos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutLogEvento(int id, LogEvento logEvento)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();

            connection.Execute("UPDATE LogEventos SET Fecha = @Fecha, Descripcion = @Descripcion WHERE Id = @Id",
                new { Fecha = logEvento.Fecha, Descripcion = logEvento.Descripcion, Id = id });

        }

        // POST: api/LogEventos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public LogEvento PostLogEvento(LogEvento logEvento)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("INSERT INTO LogEventos (Fecha, Descripcion) VALUES (@Fecha, @Descripcion)",
                new { Fecha = logEvento.Fecha, Descripcion = logEvento.Descripcion });

            return  logEvento;
        }

        // DELETE: api/LogEventos/5
        [HttpDelete("{id}")]
        public void DeleteLogEvento(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("DELETE FROM LogEventos WHERE Id = @Id", new { Id = id });

        }

       /* private bool LogEventoExists(int id)
        {
            return _context.LogEventos.Any(e => e.Id == id);
        }*/
    }
}
