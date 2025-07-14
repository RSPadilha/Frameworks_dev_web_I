using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frameworks_dev_web_I.Models
{
   public class Usuario
   {
      public long Id { get; set; }
      public string Nome { get; set; } = null!;
      [EmailAddress]
      [Required]
      public string Email { get; set; } = null!;
      public string? Tipo { get; set; }
      public DateTime DataCadastro { get; set; }
      public string? Telefone { get; set; }

      public ICollection<Pedido>? PedidosCliente { get; set; }
      public ICollection<Pedido>? PedidosAtendente { get; set; }
      public ICollection<Historico>? HistoricosModificados { get; set; }
      public ICollection<Chat>? ChatsRemetente { get; set; }
      public ICollection<Endereco>? Enderecos { get; set; }
   }
}
