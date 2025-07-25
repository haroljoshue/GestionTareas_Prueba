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
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public UsuariosController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Usuarios
        [HttpGet]
        public IEnumerable<Usuario> GetUsuario()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var usuarios = connection.Query<Usuario>("SELECT * FROM Usuarios");
            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var usuario = connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutUsuario(int id, Usuario usuario)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Contrasena= @Contrasena, FechaRegistro = @FechaRegistro, UserId = @UserId WHERE Id = @Id",
                new { Id = id, Nombre = usuario.Nombre, Apellido = usuario.Apellido, Email = usuario.Email, Contrasena = usuario.Contrasena, FechaRegistro = usuario.FechaRegistro, UserId = usuario.UserId });
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Usuario PostUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            var id = connection.QuerySingle<int>("INSERT INTO Usuarios (Nombre, Apellido, Email, Contrasena, FechaRegistro, UserId) OUTPUT INSERTED.Id VALUES (@Nombre, @Apellido, @Email, @Contrasena, @FechaRegistro, @UserId)",
                new { Nombre = usuario.Nombre, Apellido = usuario.Apellido, Email = usuario.Email, Contrasena = usuario.Contrasena, FechaRegistro = usuario.FechaRegistro, UserId = usuario.UserId });
            return usuario;
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public void DeleteUsuario(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("GestionTareasApiContext"));
            connection.Open();
            connection.Execute("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }

        /*private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }*/
    }
}
