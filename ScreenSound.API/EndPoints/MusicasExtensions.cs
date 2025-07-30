using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints;

public static class MusicasExtensions
{
    public static void AddExtensionsMusicas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] DAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });


        app.MapGet("/Musicas/{Nome}", ([FromServices] DAL<Artista> dal, string Nome) =>
        {
            var musica = dal.RecuperarPor(a => a.Musicas.Any(m => m.Nome.ToLower() == Nome.ToLower()));
            if (musica == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(musica);
            }
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
        {
            dal.Adicionar(musica);
            return Results.Ok();
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.RecuperarPor(a => a.Id == id);
            if (artista == null)
            {
                return Results.NotFound();
            }
            else
            {
                dal.Deletar(artista);
                return Results.NoContent();
            }
        });

        app.MapPut("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id, [FromBody] Musica musica) =>
        {
            var musicaExistente = dal.RecuperarPor(m => m.Id == id);
            if (musicaExistente == null)
            {
                return Results.NotFound();
            }
            else
            {
                musica.Nome = musicaExistente.Nome;
                musica.AnoLancamento = musicaExistente.AnoLancamento;
                musica.Artista = musicaExistente.Artista;
                dal.Atualizar(musica);
                return Results.Ok();
            }
        });
    }
}
