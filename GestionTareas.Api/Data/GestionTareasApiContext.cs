using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionTareas.Modelos;

public class GestionTareasApiContext : DbContext
{
    public GestionTareasApiContext(DbContextOptions<GestionTareasApiContext> options)
        : base(options)
    {
    }

    public DbSet<GestionTareas.Modelos.LogEvento> LogEventos { get; set; } = default!;


    public DbSet<GestionTareas.Modelos.MiembrosProyecto> MiembrosProyectos { get; set; } = default!;

    public DbSet<GestionTareas.Modelos.Proyecto> Proyectos { get; set; } = default!;

    public DbSet<GestionTareas.Modelos.Tarea> Tareas { get; set; } = default!;

    public DbSet<GestionTareas.Modelos.Usuario> Usuarios { get; set; } = default!;
}
