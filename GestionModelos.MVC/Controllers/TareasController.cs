using Consumer.API;
using GestionTareas.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionModelos.MVC.Controllers
{
    public class TareasController : Controller
    {
        // GET: TareasController
        public async Task<IActionResult> Index()
        {
            var tareas = await Crud<Tarea>.GetAllAsync();
            return View(tareas);
        }

        // GET: TareasController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tarea = await Crud<Tarea>.GetByIdAsync(id);
            return View(tarea);
        }

        private async Task<List<SelectListItem>> GetUsuariosAsync()
        {
            var usuarios = await Crud<Usuario>.GetAllAsync();
            return usuarios.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nombre
            }).ToList();
        }

        private async Task<List<SelectListItem>> GetProyectosAsync()
        {
            var proyectos = await Crud<Proyecto>.GetAllAsync();
            return proyectos.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nombre
            }).ToList();
        }

        // GET: TareasController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await GetUsuariosAsync();
            ViewBag.Proyectos = await GetProyectosAsync();
            return View();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarea tarea)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Crud<Tarea>.CreateAsync(tarea);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Usuarios = await GetUsuariosAsync();
                ViewBag.Proyectos = await GetProyectosAsync();
                return View(tarea);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear la tarea: " + ex.Message);
                ViewBag.Usuarios = await GetUsuariosAsync();
                ViewBag.Proyectos = await GetProyectosAsync();
                return View(tarea);
            }
        }

        // GET: TareasController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tarea = await Crud<Tarea>.GetByIdAsync(id);
            ViewBag.Usuarios = await GetUsuariosAsync();
            ViewBag.Proyectos = await GetProyectosAsync();
            return View(tarea);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarea tarea)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Crud<Tarea>.UpdateAsync(id, tarea);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Usuarios = await GetUsuariosAsync();
                ViewBag.Proyectos = await GetProyectosAsync();
                return View(tarea);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar la tarea: " + ex.Message);
                ViewBag.Usuarios = await GetUsuariosAsync();
                ViewBag.Proyectos = await GetProyectosAsync();
                return View(tarea);
            }
        }

        // GET: TareasController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tarea = await Crud<Tarea>.GetByIdAsync(id);
            return View(tarea);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Tarea tarea)
        {
            try
            {
                await Crud<Tarea>.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar la tarea: " + ex.Message);
                return View(tarea);
            }
        }
    }
}
