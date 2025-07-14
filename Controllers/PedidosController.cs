using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class PedidosController : ControllerBase
   {
      private readonly AppDbContext _context;
      public PedidosController(AppDbContext context) => _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Pedido>>> Get() => await _context.Pedidos
         //  .Include(p => p.Cliente)
         //  .Include(p => p.Atendente)
         //  .Include(p => p.Servico)
          .ToListAsync();

      [HttpGet("{id}")]
      public async Task<ActionResult<Pedido>> Get(long id)
      {
         var pedido = await _context.Pedidos
             .Include(p => p.Cliente)
             .Include(p => p.Atendente)
             .Include(p => p.Servico)
             .FirstOrDefaultAsync(p => p.Id == id);
         if (pedido == null) return NotFound();
         return pedido;
      }

      [HttpPost]
      public async Task<ActionResult<Pedido>> Post(Pedido pedido)
      {
         _context.Pedidos.Add(pedido);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(long id, Pedido pedido)
      {
         if (id != pedido.Id) return BadRequest();
         _context.Entry(pedido).State = EntityState.Modified;
         try { await _context.SaveChangesAsync(); }
         catch (DbUpdateConcurrencyException) { if (!_context.Pedidos.Any(e => e.Id == id)) return NotFound(); else throw; }
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(long id)
      {
         var pedido = await _context.Pedidos.FindAsync(id);
         if (pedido == null) return NotFound();
         _context.Pedidos.Remove(pedido);
         await _context.SaveChangesAsync();
         return NoContent();
      }
   }
}
