using System;
using System.Collections.Generic;

namespace Frameworks_dev_web_I.Models
{
   public class Chat
   {
      public long Id { get; set; }
      public long IdPedido { get; set; }
      public long IdRemetente { get; set; }
      public string Mensagem { get; set; } = null!;
      public DateTime CreatedAt { get; set; }

      public Pedido? Pedido { get; set; }
      public Usuario? Remetente { get; set; }
   }
}
