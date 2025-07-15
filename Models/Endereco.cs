using System;

namespace Frameworks_dev_web_I.Models
{
   public class Endereco
   {
      public long Id { get; set; }
      public DateTime CreatedAt { get; set; }
      public string? Cep { get; set; }
      public string? Estado { get; set; }
      public string? Cidade { get; set; }
      public string? Rua { get; set; }
      public string? Bairro { get; set; }
      public string? Numero { get; set; }
      public string? Complemento { get; set; }
      public long? IdUsuario { get; set; }

      public Usuario? Usuario { get; set; }
      public ICollection<Pedido>? Pedidos { get; set; }
   }
}
