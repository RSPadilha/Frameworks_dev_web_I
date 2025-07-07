using System.Collections.Generic;

namespace Frameworks_dev_web_I.Models
{
   public class Servico
   {
      public long Id { get; set; }
      public string Nome { get; set; } = null!;
      public string? Descricao { get; set; }
      public decimal? PrecoBase { get; set; }

      public ICollection<Pedido>? Pedidos { get; set; }
   }
}
