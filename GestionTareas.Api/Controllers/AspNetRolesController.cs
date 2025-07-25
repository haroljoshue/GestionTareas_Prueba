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
    public class AspNetRolesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AspNetRolesController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/AspNetRoles
        [HttpGet]
        public IEnumerable<AspNetRoles> GetAspNetRoles()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var aspNetRoles = connection.Query<AspNetRoles>("SELECT * FROM AspNetRoles");
            return aspNetRoles;
        }

        // GET: api/AspNetRoles/5
        [HttpGet("{id}")]
        public ActionResult<AspNetRoles> GetAspNetRoles(string id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var aspNetRoles = connection.QueryFirstOrDefault<AspNetRoles>("SELECT * FROM AspNetRoles WHERE Id = @Id", new { Id = id });

            if (aspNetRoles == null)
            {
                return NotFound();
            }

            return aspNetRoles;
        }

        // PUT: api/AspNetRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutAspNetRoles(string id, AspNetRoles aspNetRoles)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("UPDATE AspNetRoles SET Name = @Name, NormalizedName = @NormalizedName, ConcurrencyStamp = @ConcurrencyStamp WHERE Id = @Id",
                new { aspNetRoles.Name, aspNetRoles.NormalizedName, aspNetRoles.ConcurrencyStamp, Id = id });
        }

        // POST: api/AspNetRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public AspNetRoles PostAspNetRoles(AspNetRoles aspNetRoles)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("INSERT INTO AspNetRoles (Name, NormalizedName, ConcurrencyStamp) VALUES (@Name, @NormalizedName, @ConcurrencyStamp)",
                new { aspNetRoles.Name, aspNetRoles.NormalizedName, aspNetRoles.ConcurrencyStamp });
            return aspNetRoles;
        }

        // DELETE: api/AspNetRoles/5
        [HttpDelete("{id}")]
        public void DeleteAspNetRoles(string id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("DELETE FROM AspNetRoles WHERE Id = @Id", new { Id = id });
        }

        /*private bool AspNetRolesExists(int id)
        {
            return _context.AspNetRoles.Any(e => e.Id == id);
        }*/
    }
}
