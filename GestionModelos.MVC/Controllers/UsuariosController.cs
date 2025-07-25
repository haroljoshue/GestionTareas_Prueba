using Consumer.API;
using GestionTareas.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionModelos.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: UsuariosController
        //[Authorize(Roles = "administrador")]
        public async Task<ActionResult> IndexAsync()
        {
            var usuarios = await Consumer.API.Crud<Usuario>.GetAllAsync();
            return View(usuarios);
        }

        // GET: UsuariosController/Details/5
        //[Authorize]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var usuario =  await Consumer.API.Crud<Usuario>.GetByIdAsync(id);
            return View(usuario);
        }

        // GET: UsuariosController/Create
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Usuario usuario)
        {
            try
            {
                await Crud<Usuario>.CreateAsync(usuario);

                return RedirectToAction(nameof(IndexAsync));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while creating the user: " + ex.Message);
                return View(usuario);

            }
        }

        // GET: UsuariosController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var usuario = await Consumer.API.Crud<Usuario>.GetByIdAsync(id);
            return View(usuario);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Usuario usuario)
        {
            try
            {
                await Crud<Usuario>.UpdateAsync(id, usuario);
                return RedirectToAction(nameof(IndexAsync));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the user: " + ex.Message);
                return View(usuario);
            }
        }

        // GET: UsuariosController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var usuario =await Consumer.API.Crud<Usuario>.GetByIdAsync(id);
            return View(usuario);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Usuario usuario)
        {
            try
            {
                await Crud<Usuario>.DeleteAsync(id);
                return RedirectToAction(nameof(IndexAsync));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the user: " + ex.Message);  
                return View(usuario);
            }
        }
    }
}
