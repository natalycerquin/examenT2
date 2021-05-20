using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using N00019639.DB;
using N00019639.Models;

namespace N00019639.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppPokemonContext context;

        public AuthController(AppPokemonContext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            var user = context.Usuarios
               .FirstOrDefault(o => o.Username == usuario.Username && o.Password == CreateHash(usuario.Password));

            if (user == null)
            {
                TempData["AuthMessage"] = "Usuario o password incorrectos";
                return RedirectToAction("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index", "Pokemon");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public string Create(string password)
        {
            return CreateHash(password);
        }

        private string CreateHash(string input)
        {
            input += "as34232r98as50";
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
