using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class UsuariosController : ControllerBase
   {
      private readonly AppDbContext _context;
      public UsuariosController(AppDbContext context) => _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Usuario>>> Get() => await _context.Usuarios.ToListAsync();

      [HttpGet("{id}")]
      public async Task<ActionResult<Usuario>> Get(long id)
      {
         var usuario = await _context.Usuarios.FindAsync(id);
         if (usuario == null) return NotFound();
         return usuario;
      }

      [HttpPost]
      public async Task<ActionResult<Usuario>> Post(Usuario usuario)
      {
         _context.Usuarios.Add(usuario);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(long id, Usuario usuario)
      {
         if (id != usuario.Id) return BadRequest();
         _context.Entry(usuario).State = EntityState.Modified;
         try { await _context.SaveChangesAsync(); }
         catch (DbUpdateConcurrencyException) { if (!_context.Usuarios.Any(e => e.Id == id)) return NotFound(); else throw; }
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(long id)
      {
         var usuario = await _context.Usuarios.FindAsync(id);
         if (usuario == null) return NotFound();
         _context.Usuarios.Remove(usuario);
         await _context.SaveChangesAsync();
         return NoContent();
      }
   }
}
