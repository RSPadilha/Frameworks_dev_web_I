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
      public async Task<ActionResult<IEnumerable<object>>> Get()
      {
         var pedidos = await _context.Pedidos
             .Include(p => p.Cliente)
             .Include(p => p.Atendente)
             .Include(p => p.Servico)
             .Include(p => p.Endereco)
             .Select(p => new
             {
                p.Id,
                p.IdCliente,
                p.IdAtendente,
                p.IdServico,
                p.IdEndereco,
                p.Descricao,
                p.DataCriacao,
                p.Status,
                p.DataConclusao,
                cliente = p.Cliente != null ? p.Cliente.Nome : null,
                atendente = p.Atendente != null ? p.Atendente.Nome : null,
                servico = p.Servico != null ? p.Servico.Nome : null,
                endereco = p.Endereco != null ? $"{p.Endereco.Rua}, {p.Endereco.Numero} - {p.Endereco.Bairro}, {p.Endereco.Cidade}/{p.Endereco.Estado}" : null
             })
             .ToListAsync();

         return Ok(pedidos);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<object>> Get(long id)
      {
         var pedido = await _context.Pedidos
             .Include(p => p.Cliente)
             .Include(p => p.Atendente)
             .Include(p => p.Servico)
             .Include(p => p.Endereco)
             .Where(p => p.Id == id)
             .Select(p => new
             {
                p.Id,
                p.IdCliente,
                p.IdAtendente,
                p.IdServico,
                p.IdEndereco,
                p.Descricao,
                p.DataCriacao,
                p.Status,
                p.DataConclusao,
                cliente = p.Cliente != null ? p.Cliente.Nome : null,
                atendente = p.Atendente != null ? p.Atendente.Nome : null,
                servico = p.Servico != null ? p.Servico.Nome : null,
                endereco = p.Endereco != null ? $"{p.Endereco.Rua}, {p.Endereco.Numero} - {p.Endereco.Bairro}, {p.Endereco.Cidade}/{p.Endereco.Estado}" : null,
                Cliente = p.Cliente != null ? new
                {
                   p.Cliente.Id,
                   p.Cliente.Nome,
                   p.Cliente.Email,
                   p.Cliente.Tipo,
                   p.Cliente.Telefone
                } : null,
                Atendente = p.Atendente != null ? new
                {
                   p.Atendente.Id,
                   p.Atendente.Nome,
                   p.Atendente.Email,
                   p.Atendente.Tipo,
                   p.Atendente.Telefone
                } : null,
                Servico = p.Servico != null ? new
                {
                   p.Servico.Id,
                   p.Servico.Nome,
                   p.Servico.Descricao,
                   p.Servico.PrecoBase
                } : null,
                Endereco = p.Endereco != null ? new
                {
                   p.Endereco.Id,
                   p.Endereco.Cep,
                   p.Endereco.Estado,
                   p.Endereco.Cidade,
                   p.Endereco.Rua,
                   p.Endereco.Bairro,
                   p.Endereco.Numero,
                   p.Endereco.Complemento
                } : null
             })
             .FirstOrDefaultAsync();

         if (pedido == null) return NotFound();
         return pedido;
      }

      [HttpGet("cliente/{idCliente}")]
      public async Task<ActionResult<IEnumerable<object>>> GetByCliente(long idCliente)
      {
         var pedidos = await _context.Pedidos
             .Where(p => p.IdCliente == idCliente)
             .Include(p => p.Cliente)
             .Include(p => p.Atendente)
             .Include(p => p.Servico)
             .Include(p => p.Endereco)
             .Select(p => new
             {
                p.Id,
                p.IdCliente,
                p.IdAtendente,
                p.IdServico,
                p.IdEndereco,
                p.Descricao,
                p.DataCriacao,
                p.Status,
                p.DataConclusao,
                cliente = p.Cliente != null ? p.Cliente.Nome : null,
                atendente = p.Atendente != null ? p.Atendente.Nome : null,
                servico = p.Servico != null ? p.Servico.Nome : null,
                endereco = p.Endereco != null ? $"{p.Endereco.Rua}, {p.Endereco.Numero} - {p.Endereco.Bairro}, {p.Endereco.Cidade}/{p.Endereco.Estado}" : null,
                Cliente = p.Cliente != null ? new
                {
                   p.Cliente.Id,
                   p.Cliente.Nome,
                   p.Cliente.Email,
                   p.Cliente.Tipo,
                   p.Cliente.Telefone
                } : null,
                Atendente = p.Atendente != null ? new
                {
                   p.Atendente.Id,
                   p.Atendente.Nome,
                   p.Atendente.Email,
                   p.Atendente.Tipo,
                   p.Atendente.Telefone
                } : null,
                Servico = p.Servico != null ? new
                {
                   p.Servico.Id,
                   p.Servico.Nome,
                   p.Servico.Descricao,
                   p.Servico.PrecoBase
                } : null,
                Endereco = p.Endereco != null ? new
                {
                   p.Endereco.Id,
                   p.Endereco.Cep,
                   p.Endereco.Estado,
                   p.Endereco.Cidade,
                   p.Endereco.Rua,
                   p.Endereco.Bairro,
                   p.Endereco.Numero,
                   p.Endereco.Complemento
                } : null
             })
             .ToListAsync();

         return Ok(pedidos);
      }

      [HttpGet("atendente/{idAtendente}")]
      public async Task<ActionResult<IEnumerable<object>>> GetByAtendente(long idAtendente)
      {
         var pedidos = await _context.Pedidos
             .Where(p => p.IdAtendente == idAtendente)
             .Include(p => p.Cliente)
             .Include(p => p.Atendente)
             .Include(p => p.Servico)
             .Include(p => p.Endereco)
             .Select(p => new
             {
                p.Id,
                p.IdCliente,
                p.IdAtendente,
                p.IdServico,
                p.IdEndereco,
                p.Descricao,
                p.DataCriacao,
                p.Status,
                p.DataConclusao,
                cliente = p.Cliente != null ? p.Cliente.Nome : null,
                atendente = p.Atendente != null ? p.Atendente.Nome : null,
                servico = p.Servico != null ? p.Servico.Nome : null,
                endereco = p.Endereco != null ? $"{p.Endereco.Rua}, {p.Endereco.Numero} - {p.Endereco.Bairro}, {p.Endereco.Cidade}/{p.Endereco.Estado}" : null,
                Cliente = p.Cliente != null ? new
                {
                   p.Cliente.Id,
                   p.Cliente.Nome,
                   p.Cliente.Email,
                   p.Cliente.Tipo,
                   p.Cliente.Telefone
                } : null,
                Atendente = p.Atendente != null ? new
                {
                   p.Atendente.Id,
                   p.Atendente.Nome,
                   p.Atendente.Email,
                   p.Atendente.Tipo,
                   p.Atendente.Telefone
                } : null,
                Servico = p.Servico != null ? new
                {
                   p.Servico.Id,
                   p.Servico.Nome,
                   p.Servico.Descricao,
                   p.Servico.PrecoBase
                } : null,
                Endereco = p.Endereco != null ? new
                {
                   p.Endereco.Id,
                   p.Endereco.Cep,
                   p.Endereco.Estado,
                   p.Endereco.Cidade,
                   p.Endereco.Rua,
                   p.Endereco.Bairro,
                   p.Endereco.Numero,
                   p.Endereco.Complemento
                } : null
             })
             .ToListAsync();

         return Ok(pedidos);
      }

      [HttpPost]
      public async Task<ActionResult<Pedido>> Post(Pedido pedido)
      {
         // Definir a data de criação automaticamente
         pedido.DataCriacao = DateTime.UtcNow;
         pedido.Status = pedido.Status ?? "Pendente";

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
