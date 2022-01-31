using System;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Palestrante> Palestrantes { get; set; }

        //Relacao m x n
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }

        public DbSet<Lote> Lotes { get; set; }

        public DbSet<RedeSocial> RedeSociais { get; set; }
        private readonly ModelBuilder modelBuilder;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //associação entre as classses Evento x Palestrante
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new {PE.EventoId,PE.PalestranteId});

            // trata a relacao de Evento x RedeSociais 
            modelBuilder.Entity<Evento>()
            .HasMany(e=>e.RedesSociais)   
            .WithOne(rs=> rs.Evento)
            .OnDelete(DeleteBehavior.Cascade);

            // trata a relacao de Palestrante x RedeSociais 
            modelBuilder.Entity<Palestrante>()
            .HasMany(e=>e.RedesSociais)   
            .WithOne(rs=> rs.Palestrante)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}