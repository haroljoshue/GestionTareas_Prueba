using Consumer.API;
using GestionTareas.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionModelos.MVC.Controllers
{
    //[Authorize]
    public class ProyectosController : Controller
    {
        // GET: ProyectosController
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Index()
        {
            var proyectos = await Crud<Proyecto>.GetAllAsync();
            return View(proyectos);
        }

        // GET: ProyectosController/Details/5
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Details(int id)
        {
            var proyecto = await Crud<Proyecto>.GetByIdAsync(id);
            return View(proyecto);
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

        // GET: ProyectosController/Create
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await GetUsuariosAsync();
            return View();
        }

        // POST: ProyectosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Create(Proyecto proyecto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Crud<Proyecto>.CreateAsync(proyecto);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Usuarios = await GetUsuariosAsync();
                return View(proyecto);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al crear el proyecto.");
                ViewBag.Usuarios = await GetUsuariosAsync();
                return View(proyecto);
            }
        }

        // GET: ProyectosController/Edit/5
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Edit(int id)
        {
            var proyecto = await Crud<Proyecto>.GetByIdAsync(id);
            ViewBag.Usuarios = await GetUsuariosAsync();
            return View(proyecto);
        }

        // POST: ProyectosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Edit(int id, Proyecto proyecto)
        {
            try
            {
                await Crud<Proyecto>.UpdateAsync(id, proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al editar el proyecto.");
                ViewBag.Usuarios = await GetUsuariosAsync();
                return View(proyecto);
            }
        }

        // GET: ProyectosController/Delete/5
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Delete(int id)
        {
            var proyecto = await Crud<Proyecto>.GetByIdAsync(id);
            return View(proyecto);
        }

        // POST: ProyectosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "lider")]
        public async Task<IActionResult> Delete(int id, Proyecto proyecto)
        {
            try
            {
                await Crud<Proyecto>.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar el proyecto.");
                return View(proyecto);
            }
        }
    }
}
