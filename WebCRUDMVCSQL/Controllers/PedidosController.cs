using Microsoft.AspNetCore.Mvc;
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
            return View(new Pedido());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("NomeCliente,NomeProduto,Quantidade,Preco")] Pedido pedido,
            string acao)
        { 
            var cliente = await _context.Client
                .FirstOrDefaultAsync(c => c.Nome == pedido.NomeCliente);

            var produto = await _context.Produto
                .FirstOrDefaultAsync(p => p.Nome == pedido.NomeProduto);

            if (acao == "buscarPreco")
            {
                if (produto != null && pedido.Quantidade > 0)
                {
                    pedido.Preco = (decimal)produto.Preco * pedido.Quantidade;
                }
                else
                {
                    ModelState.AddModelError("NomeProduto", "Produto não encontrado.");
                }

                ModelState.Clear();
                return View(pedido);
            }

            if (cliente == null)
                ModelState.AddModelError("NomeCliente", "Cliente não encontrado.");

            if (produto == null)
                ModelState.AddModelError("NomeProduto", "Produto não encontrado.");

            if (ModelState.IsValid)
            {
                pedido.IdCliente = cliente!.Id;
                pedido.IdProduto = produto!.Id;

                pedido.Cliente = null;
                pedido.Produto = null;

                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(pedido);
        }

        public IActionResult Edit(int id)
        {
            var pedido = _context.Pedido.Find(id);
            if (pedido == null)
                return NotFound();
            return View(pedido);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmado(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}