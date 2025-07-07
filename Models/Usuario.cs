using System;
using System.Collections.Generic;

namespace Frameworks_dev_web_I.Models
{
   public class Usuario
   {
      public long Id { get; set; }
      public string? Nome { get; set; }
      [EmailAddress]
      [Required]
      public string Email { get; set; } = null!;
      [Required]
      [MinLength(6)]
      public string Senha { get; set; } = null!;
      public string? Tipo { get; set; }
      public DateTime DataCadastro { get; set; }

      public ICollection<Pedido>? PedidosCliente { get; set; }
      public ICollection<Pedido>? PedidosAtendente { get; set; }
      public ICollection<Historico>? HistoricosModificados { get; set; }
      public ICollection<Chat>? ChatsRemetente { get; set; }
   }
}
