using System;
using System.Collections.Generic;

namespace Frameworks_dev_web_I.Models
{
   public class Pedido
   {
      public long Id { get; set; }
      public long IdCliente { get; set; }
      public long? IdAtendente { get; set; }
      public long? IdServico { get; set; }
      public string? Descricao { get; set; }
      public DateTime DataCriacao { get; set; }
      public string? Status { get; set; }
      public DateTime? DataConclusao { get; set; }

      public Usuario? Cliente { get; set; }
      public Usuario? Atendente { get; set; }
      public Servico? Servico { get; set; }
      public ICollection<Historico>? Historicos { get; set; }
      public ICollection<Chat>? Chats { get; set; }
   }
}
