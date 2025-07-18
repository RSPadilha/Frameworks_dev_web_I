using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Controllers
{
   // Classe para receber o status na requisição PUT
   public class StatusUpdateRequest
   {
      public string Status { get; set; } = null!;
   }

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
      public async Task<IActionResult> Put(long id, [FromBody] StatusUpdateRequest request)
      {
         try
         {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            // Verificar se o status foi fornecido
            if (string.IsNullOrEmpty(request.Status))
            {
               return BadRequest("Status é obrigatório");
            }

            // Verificar se o status realmente mudou
            string statusAnterior = pedido.Status ?? "";
            if (statusAnterior.Equals(request.Status, StringComparison.OrdinalIgnoreCase))
            {
               return Ok("Status não foi alterado");
            }

            // Atualizar apenas o status
            pedido.Status = request.Status;

            // Se o status for "Concluído", definir a data de conclusão
            if (request.Status.ToLower() == "concluído" || request.Status.ToLower() == "concluido")
            {
               pedido.DataConclusao = DateTime.UtcNow;
            }

            // Criar registro de histórico
            var historico = new Historico
            {
               IdPedido = id,
               DataAlteracao = DateTime.UtcNow,
               StatusNovo = request.Status,
               ModificadoPor = null // Pode ser configurado para receber o ID do usuário que fez a alteração
            };

            _context.Historicos.Add(historico);
            await _context.SaveChangesAsync();

            return NoContent();
         }
         catch (Exception ex)
         {
            return BadRequest($"Erro ao atualizar o status do pedido: {ex.Message}");
         }
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
