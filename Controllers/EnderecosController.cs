using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class EnderecosController : ControllerBase
   {
      private readonly AppDbContext _context;
      public EnderecosController(AppDbContext context) => _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<object>>> Get()
      {
         var enderecos = await _context.Enderecos
            .Include(e => e.Usuario)
            .Select(e => new
            {
               e.Id,
               e.CreatedAt,
               e.Cep,
               e.Estado,
               e.Cidade,
               e.Rua,
               e.Bairro,
               e.Numero,
               e.Complemento,
               e.IdUsuario,
               Usuario = e.Usuario != null ? new
               {
                  e.Usuario.Id,
                  e.Usuario.Nome,
                  e.Usuario.Email,
                  e.Usuario.Tipo,
                  e.Usuario.Telefone
               } : null
            })
            .ToListAsync();

         return Ok(enderecos);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<object>> Get(long id)
      {
         var endereco = await _context.Enderecos
            .Include(e => e.Usuario)
            .Where(e => e.Id == id)
            .Select(e => new
            {
               e.Id,
               e.CreatedAt,
               e.Cep,
               e.Estado,
               e.Cidade,
               e.Rua,
               e.Bairro,
               e.Numero,
               e.Complemento,
               e.IdUsuario,
               Usuario = e.Usuario != null ? new
               {
                  e.Usuario.Id,
                  e.Usuario.Nome,
                  e.Usuario.Email,
                  e.Usuario.Tipo,
                  e.Usuario.Telefone
               } : null
            })
            .FirstOrDefaultAsync();

         if (endereco == null) return NotFound();
         return endereco;
      }

      [HttpGet("usuario/{usuarioId}")]
      public async Task<ActionResult<IEnumerable<object>>> GetByUsuario(long usuarioId)
      {
         var enderecos = await _context.Enderecos
            .Where(e => e.IdUsuario == usuarioId)
            .Include(e => e.Usuario)
            .Select(e => new
            {
               e.Id,
               e.CreatedAt,
               e.Cep,
               e.Estado,
               e.Cidade,
               e.Rua,
               e.Bairro,
               e.Numero,
               e.Complemento,
               e.IdUsuario,
               Usuario = e.Usuario != null ? new
               {
                  e.Usuario.Id,
                  e.Usuario.Nome,
                  e.Usuario.Email,
                  e.Usuario.Tipo,
                  e.Usuario.Telefone
               } : null
            })
            .ToListAsync();

         return Ok(enderecos);
      }

      [HttpPost]
      public async Task<ActionResult<Endereco>> Post(Endereco endereco)
      {
         endereco.CreatedAt = DateTime.UtcNow;
         _context.Enderecos.Add(endereco);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Get), new { id = endereco.Id }, endereco);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(long id, Endereco endereco)
      {
         if (id != endereco.Id) return BadRequest();
         _context.Entry(endereco).State = EntityState.Modified;
         try { await _context.SaveChangesAsync(); }
         catch (DbUpdateConcurrencyException) { if (!_context.Enderecos.Any(e => e.Id == id)) return NotFound(); else throw; }
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(long id)
      {
         var endereco = await _context.Enderecos.FindAsync(id);
         if (endereco == null) return NotFound();
         _context.Enderecos.Remove(endereco);
         await _context.SaveChangesAsync();
         return NoContent();
      }
   }
}
