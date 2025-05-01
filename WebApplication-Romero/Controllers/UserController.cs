using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using WebApplication_Romero.Context;
using WebApplication_Romero.Models;
using WebApplication_Romero.Services;

namespace WebApplication_Romero.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly AppDbContext _context;
        public UserController(AppDbContext context, UserService userService)
        {
            _userService = userService;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Import()
        {
            var result = await _userService.ImportUsersAsync();
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            else
            {
                return BadRequest(new { message = result.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                List<User> lista = await _context.Users
                   .OrderByDescending(u => u.UserId)
                   .ToListAsync();
                if (lista.Count > 0)
                { return View(lista); }
                else
                { return View(lista); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return View(new List<User>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListaJson()
        {
            try
            {
                var lista = await _context.Users
                    .OrderByDescending(u => u.UserId)
                    .ToListAsync();

                if (lista.Count == 0)
                    return Json(new { success = false, message = "No hay usuarios disponibles." });

                return Json(new { success = true, data = lista });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al obtener usuarios.", error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new User{
                nombre = "",
                telefono = "",
                correo = "",
                nombre_company = "",
                calle = "",
                latitud = "",
                longitud = ""
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Lista");
                }
                return View(newUser);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar el usuario.");
                return View(newUser);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    ViewBag.UsuarioNoEncontrado = true;
                    return View(user);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return View(new List<User>());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(User updatedUser)
        {
            var user = await _context.Users.FindAsync(updatedUser.UserId);
            if (user == null) return NotFound();

            user.nombre = updatedUser.nombre;
            user.correo = updatedUser.correo;
            user.telefono = updatedUser.telefono;
            await _context.SaveChangesAsync();
            return RedirectToAction("Lista");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var user = await _context.Users.FindAsync(id);
            Console.WriteLine($" COUNT :: {user} ::: ID:: {id}");
            if (user == null) return NotFound(new { message = "Usuario no encontrado." });
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Usuario eliminado correctamente." });
        }
    }
}
