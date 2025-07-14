using System;

namespace Frameworks_dev_web_I.Models
{
   public class Historico
   {
      public long Id { get; set; }
      public long IdPedido { get; set; }
      public DateTime DataAlteracao { get; set; }
      public string? StatusNovo { get; set; }
      public long? ModificadoPor { get; set; }

      public Pedido? Pedido { get; set; }
      public Usuario? UsuarioModificador { get; set; }
   }
}
