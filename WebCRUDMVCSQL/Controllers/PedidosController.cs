using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCRUDMVCSQL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCRUDMVCSQL.Models;
using Microsoft.EntityFrameworkCore;

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
            await CarregarViewBags();
            return View(new Pedido());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdCliente,IdProduto,Quantidade,Preco")] Pedido pedido,
            string acao)
        {
            if (acao == "buscarPreco")
            {
                if (pedido.IdProduto > 0)
                {
                    var produto = await _context.Produto.FindAsync(pedido.IdProduto);
                    if (produto != null)
                        pedido.Preco = (decimal)produto.Preco;
                }

                ModelState.Clear();

                await CarregarViewBags(pedido.IdCliente, pedido.IdProduto);
                return View(pedido);
            }

            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await CarregarViewBags(pedido.IdCliente, pedido.IdProduto);
            return View(pedido);
        }

        public IActionResult Edit(int id)
        {
            var pedido = _context.Pedido.Find(id);
            if (pedido == null)
                return NotFound();
            return View(pedido);
        }

        // Método auxiliar para evitar repetição
        private async Task CarregarViewBags(int idCliente = 0, int idProduto = 0)
        {
            var clientes = await _context.Client.ToListAsync();
            var produtos = await _context.Produto.ToListAsync();
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nome", idCliente);
            ViewBag.Produtos = new SelectList(produtos, "Id", "Nome", idProduto);
        }

    }
}