using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class HistoricosController : ControllerBase
   {
      private readonly AppDbContext _context;
      public HistoricosController(AppDbContext context) => _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Historico>>> Get() => await _context.Historicos.ToListAsync();

      [HttpGet("{id}")]
      public async Task<ActionResult<Historico>> Get(long id)
      {
         var historico = await _context.Historicos.FindAsync(id);
         if (historico == null) return NotFound();
         return historico;
      }

      [HttpPost]
      public async Task<ActionResult<Historico>> Post(Historico historico)
      {
         _context.Historicos.Add(historico);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Get), new { id = historico.Id }, historico);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(long id, Historico historico)
      {
         if (id != historico.Id) return BadRequest();
         _context.Entry(historico).State = EntityState.Modified;
         try { await _context.SaveChangesAsync(); }
         catch (DbUpdateConcurrencyException) { if (!_context.Historicos.Any(e => e.Id == id)) return NotFound(); else throw; }
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(long id)
      {
         var historico = await _context.Historicos.FindAsync(id);
         if (historico == null) return NotFound();
         _context.Historicos.Remove(historico);
         await _context.SaveChangesAsync();
         return NoContent();
      }
   }
}
