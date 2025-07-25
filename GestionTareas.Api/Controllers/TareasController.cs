using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionTareas.Modelos;
using Microsoft.Data.SqlClient;

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
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public ActionResult<Tarea> GetTarea(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();

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
        }

        // POST: api/Tareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Tarea PostTarea(Tarea tarea)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public void DeleteTarea(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
        }

        /*private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }*/
    }
}
