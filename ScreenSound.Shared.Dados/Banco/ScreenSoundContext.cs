using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;
public class ScreenSoundContext : DbContext
{
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Generos { get; set; }

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSoundV1;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public ScreenSoundContext()
    {

    }
    public ScreenSoundContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Musica>()
            .HasMany(c => c.Generos)
            .WithMany(c => c.Musicas);

        modelBuilder.Entity<Artista>().HasData(
            new Artista { Id = 1, Nome = "Djavan", Bio = "Um dos maiores cantores e compositores da MPB...", FotoPerfil = "url-da-foto-djavan" },
            new Artista { Id = 2, Nome = "U2", Bio = "Banda de rock irlandesa formada em 1976...", FotoPerfil = "url-da-foto-u2" },
            new Artista { Id = 3, Nome = "Queen", Bio = "Banda de rock britânica formada em Londres em 1970...", FotoPerfil = "url-da-foto-queen" }
        );

        modelBuilder.Entity<Genero>().HasData(
            new Genero { Id = 1, Nome = "Rock", Descricao = "Gênero de música popular que se desenvolveu durante e após a década de 1950." },
            new Genero { Id = 2, Nome = "MPB", Descricao = "Música popular brasileira, um estilo musical que surgiu no Brasil." },
            new Genero { Id = 3, Nome = "Pop", Descricao = "Música popular, geralmente orientada para o mercado comercial." }
        );

        modelBuilder.Entity<Musica>().HasData(
            new Musica { Id = 1, Nome = "Sina", AnoLancamento = 1982, ArtistaId = 1 },
            new Musica { Id = 2, Nome = "Oceano", AnoLancamento = 1989, ArtistaId = 1 },
            new Musica { Id = 3, Nome = "With or Without You", AnoLancamento = 1987, ArtistaId = 2 },
            new Musica { Id = 4, Nome = "One", AnoLancamento = 1991, ArtistaId = 2 },
            new Musica { Id = 5, Nome = "Bohemian Rhapsody", AnoLancamento = 1975, ArtistaId = 3 }
        );

        modelBuilder.Entity("GeneroMusica").HasData(
            new { MusicasId = 1, GenerosId = 2 },
            new { MusicasId = 2, GenerosId = 2 },
            new { MusicasId = 3, GenerosId = 1 },
            new { MusicasId = 4, GenerosId = 1 },
            new { MusicasId = 5, GenerosId = 1 },
            new { MusicasId = 5, GenerosId = 3 }
        );
    }


}
