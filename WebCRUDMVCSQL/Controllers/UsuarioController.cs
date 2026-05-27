using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCRUDMVCSQL.Models;

namespace WebCRUDMVCSQL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Contexto _context;

        public UsuarioController(Contexto context)
        {
            _context = context;
        }

        // GET: /Usuario/TelaDeLogin
        public IActionResult TelaDeLogin()
        {
            return View();
        }

        // POST: /Usuario/TelaDeLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TelaDeLogin(string email, string senha)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                ViewBag.Erro = "Email ou senha inválidos!";
                return View();
            }

            // Login OK — redireciona para a tela principal
            return RedirectToAction("Index", "Home");
        }

        // GET: /Usuario/TelaDeCadastro
        public IActionResult TelaDeCadastro()
        {
            return View();
        }

        // POST: /Usuario/TelaDeCadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TelaDeCadastro([Bind("Nome,Email,Senha")] Usuario usuario)
        {
            // Verifica se o email já está cadastrado
            var existe = await _context.Usuario
                .AnyAsync(u => u.Email == usuario.Email);

            if (existe)
            {
                ViewBag.Erro = "Este email já está cadastrado!";
                return View(usuario);
            }

            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TelaDeLogin));
            }

            return View(usuario);
        }
    }
}