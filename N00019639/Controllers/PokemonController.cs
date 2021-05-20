using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using N00019639.DB;
using N00019639.Models;

namespace N00019639.Controllers
{
    public class PokemonController : Controller
    {
        private readonly AppPokemonContext context;
        public IHostEnvironment _hostEnv;

        public PokemonController(IHostEnvironment hostEnv, AppPokemonContext context)
        {
            this.context = context;
            _hostEnv = hostEnv;
        }

        public IActionResult Index()
        {
            var pokemons = context.Pokemons.Include(o => o.Tipo).ToList();
            return View(pokemons);
        }

        [HttpGet]
        public IActionResult Registrar() {
            var tipos = context.Tipos.ToList();
            return View(tipos);
        }

        [HttpPost]
        public IActionResult Registrar(Pokemon pokemon, IFormFile image)
        {
            var tipos = context.Tipos.ToList();


            if (string.IsNullOrEmpty(pokemon.Nombre))
            {
                TempData["ErrorValidacion"] = "el nombre es obligatorio";
                return View(tipos);
            }

            var pokemonBd = context.Pokemons.Where(o => o.Nombre == pokemon.Nombre).FirstOrDefault();
            if (pokemonBd != null)
            {
                TempData["ErrorValidacion"] = "el nombre ya esta registrado";
                return View(tipos);
            }

            if (pokemon.TipoId == null)
            {
                TempData["ErrorValidacion"] = "el tipo es obligatorio";
                return View(tipos);
            }

            if (image == null)
            {
                TempData["ErrorValidacion"] = "la imagen es obligatoria";
                return View(tipos);
            }

            pokemon.RutaImagen = SaveImage(image);
            context.Pokemons.Add(pokemon);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Capturar(UsuarioPokemon usuarioPokemon)
        {
            var usuario = GetUsuario();
            usuarioPokemon.Fecha = DateTime.Now;
            usuarioPokemon.UsuarioId = usuario.Id;
            context.UsuarioPokemons.Add(usuarioPokemon);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Liberar(UsuarioPokemon usuarioPokemon)
        {
            var usuario = GetUsuario();
            var usuarioPokemonBd = context.UsuarioPokemons.Where(o => o.PokemonId == usuarioPokemon.PokemonId && o.UsuarioId == usuario.Id).FirstOrDefault();
            context.UsuarioPokemons.Remove(usuarioPokemonBd);
            context.SaveChanges();
            return RedirectToAction("Capturados");
        }

        [Authorize]
        public IActionResult Capturados()
        {
            var usuario = GetUsuario();
            var pokemonesCapturados = context.UsuarioPokemons.Where(o => o.UsuarioId == usuario.Id).Include(o => o.Pokemon).ThenInclude(o => o.Tipo).ToList();
            return View(pokemonesCapturados);
        }

        private string SaveImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                var ruta = @"\pokemones\" + image.FileName;

                using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                {
                    image.CopyTo(strem);
                    return ruta;
                }
            }
            return null;
        }

        private Usuario GetUsuario()
        {
            var claim = HttpContext.User.Claims.Where(o => o.Type == ClaimTypes.Name).FirstOrDefault();
            if (claim != null)
            {
                return context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
