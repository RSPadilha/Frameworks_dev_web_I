using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      public DbSet<Usuario> Usuarios { get; set; }
      public DbSet<Servico> Servicos { get; set; }
      public DbSet<Pedido> Pedidos { get; set; }
      public DbSet<Historico> Historicos { get; set; }
      public DbSet<Chat> Chats { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         // Mapear nomes das tabelas para minúsculo
         modelBuilder.Entity<Usuario>().ToTable("usuarios");
         modelBuilder.Entity<Usuario>(entity =>
         {
            entity.Property(u => u.Id).HasColumnName("id");
            entity.Property(u => u.Nome).HasColumnName("nome");
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.Senha).HasColumnName("senha");
            entity.Property(u => u.Tipo).HasColumnName("tipo");
            entity.Property(u => u.DataCadastro).HasColumnName("dataCadastro");
         });
         modelBuilder.Entity<Servico>().ToTable("servicos");
         modelBuilder.Entity<Servico>(entity =>
         {
            entity.Property(s => s.Id).HasColumnName("id");
            entity.Property(s => s.Nome).HasColumnName("nome");
            entity.Property(s => s.Descricao).HasColumnName("descricao");
            entity.Property(s => s.PrecoBase).HasColumnName("precobase");
         });
         modelBuilder.Entity<Pedido>().ToTable("pedidos");
         modelBuilder.Entity<Pedido>(entity =>
         {
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.IdCliente).HasColumnName("idCliente");
            entity.Property(p => p.IdAtendente).HasColumnName("idAtendente");
            entity.Property(p => p.IdServico).HasColumnName("idServico");
            entity.Property(p => p.Descricao).HasColumnName("descricao");
            entity.Property(p => p.DataCriacao).HasColumnName("dataCriacao");
            entity.Property(p => p.Status).HasColumnName("status");
            entity.Property(p => p.DataConclusao).HasColumnName("dataConclusao");
         });
         modelBuilder.Entity<Historico>().ToTable("historico");
         modelBuilder.Entity<Historico>(entity =>
         {
            entity.Property(h => h.Id).HasColumnName("id");
            entity.Property(h => h.IdPedido).HasColumnName("idPedido");
            entity.Property(h => h.DataAlteracao).HasColumnName("dataAlteração");
            entity.Property(h => h.StatusNovo).HasColumnName("statusNovo");
            entity.Property(h => h.ModificadoPor).HasColumnName("modificadoPor");
         });
         modelBuilder.Entity<Chat>().ToTable("chat");
         modelBuilder.Entity<Chat>(entity =>
         {
            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.IdPedido).HasColumnName("idPedido");
            entity.Property(c => c.IdRemetente).HasColumnName("idRemetente");
            entity.Property(c => c.Mensagem).HasColumnName("mensagem");
            entity.Property(c => c.CreatedAt).HasColumnName("created_at");
         });

         // Relacionamento Pedido - Cliente (Usuario)
         modelBuilder.Entity<Pedido>()
             .HasOne(p => p.Cliente)
             .WithMany(u => u.PedidosCliente)
             .HasForeignKey(p => p.IdCliente)
             .OnDelete(DeleteBehavior.Restrict);

         // Relacionamento Pedido - Atendente (Usuario)
         modelBuilder.Entity<Pedido>()
             .HasOne(p => p.Atendente)
             .WithMany(u => u.PedidosAtendente)
             .HasForeignKey(p => p.IdAtendente)
             .OnDelete(DeleteBehavior.Restrict);

         // Relacionamento Pedido - Servico
         modelBuilder.Entity<Pedido>()
             .HasOne(p => p.Servico)
             .WithMany(s => s.Pedidos)
             .HasForeignKey(p => p.IdServico)
             .OnDelete(DeleteBehavior.Restrict);

         // Relacionamento Historico - Pedido
         modelBuilder.Entity<Historico>()
             .HasOne(h => h.Pedido)
             .WithMany(p => p.Historicos)
             .HasForeignKey(h => h.IdPedido)
             .OnDelete(DeleteBehavior.Cascade);

         // Relacionamento Historico - Usuario (modificadoPor)
         modelBuilder.Entity<Historico>()
             .HasOne(h => h.UsuarioModificador)
             .WithMany(u => u.HistoricosModificados)
             .HasForeignKey(h => h.ModificadoPor)
             .OnDelete(DeleteBehavior.Restrict);

         // Relacionamento Chat - Pedido
         modelBuilder.Entity<Chat>()
             .HasOne(c => c.Pedido)
             .WithMany(p => p.Chats)
             .HasForeignKey(c => c.IdPedido)
             .OnDelete(DeleteBehavior.Cascade);

         // Relacionamento Chat - Usuario (Remetente)
         modelBuilder.Entity<Chat>()
             .HasOne(c => c.Remetente)
             .WithMany(u => u.ChatsRemetente)
             .HasForeignKey(c => c.IdRemetente)
             .OnDelete(DeleteBehavior.Restrict);
      }
   }
}
