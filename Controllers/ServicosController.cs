using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ServicosController : ControllerBase
   {
      private readonly AppDbContext _context;
      public ServicosController(AppDbContext context) => _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Servico>>> Get() => await _context.Servicos.ToListAsync();

      [HttpGet("{id}")]
      public async Task<ActionResult<Servico>> Get(long id)
      {
         var servico = await _context.Servicos.FindAsync(id);
         if (servico == null) return NotFound();
         return servico;
      }

      [HttpPost]
      public async Task<ActionResult<Servico>> Post(Servico servico)
      {
         _context.Servicos.Add(servico);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Get), new { id = servico.Id }, servico);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(long id, Servico servico)
      {
         if (id != servico.Id) return BadRequest();
         _context.Entry(servico).State = EntityState.Modified;
         try { await _context.SaveChangesAsync(); }
         catch (DbUpdateConcurrencyException) { if (!_context.Servicos.Any(e => e.Id == id)) return NotFound(); else throw; }
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(long id)
      {
         var servico = await _context.Servicos.FindAsync(id);
         if (servico == null) return NotFound();
         _context.Servicos.Remove(servico);
         await _context.SaveChangesAsync();
         return NoContent();
      }
   }
}
