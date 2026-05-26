using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCRUDMVCSQL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCRUDMVCSQL.Models;
using System.Text.Json;

namespace WebCRUDMVCSQL.Controllers
{
    public class PedidosController : Controller
    {
        private readonly Contexto _context;

        public PedidosController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Produto)
                .ToListAsync();
            return View(pedidos);
        }

        public async Task<IActionResult> Create()
        {
            var clientes = await _context.Client.ToListAsync();
            var produtos = await _context.Produto.ToListAsync();

            ViewBag.Clientes = new SelectList(clientes, "Id", "Nome");
            ViewBag.Produtos = new SelectList(produtos, "Id", "Nome");
            ViewBag.ProdutosJson = JsonSerializer.Serialize(produtos.Select(p => new
            {
                id = p.Id,
                nome = p.Nome,
                preco = p.Preco
            }));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,IdProduto,Quantidade,Preco")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var clientes = await _context.Client.ToListAsync();
            var produtos = await _context.Produto.ToListAsync();

            ViewBag.Clientes = new SelectList(clientes, "Id", "Nome");
            ViewBag.Produtos = new SelectList(produtos, "Id", "Nome");
            ViewBag.ProdutosJson = JsonSerializer.Serialize(produtos.Select(p => new
            {
                id = p.Id,
                nome = p.Nome,
                preco = p.Preco
            }));

            return View(pedido);
        }

        public IActionResult Edit(int id)
        {
            var pedido = _context.Pedido.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }
    }
}