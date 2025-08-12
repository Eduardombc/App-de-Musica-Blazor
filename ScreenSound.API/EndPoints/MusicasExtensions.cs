using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints;

public static class MusicasExtensions
{
    public static void AddEndPointsMusicas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] DAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });


        app.MapGet("/Musicas/{Nome}", ([FromServices] DAL<Artista> dal, string Nome) =>
        {
            var musica = dal.RecuperarPor(a => a.Generos.Any(m => m.Nome.ToLower() == Nome.ToLower()));
            if (musica == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(musica);
            }
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
        {
            var musica = new Musica(musicaRequest.Nome)
            {
                Id = musicaRequest.Id,
                AnoLancamento = musicaRequest.AnoLancamento,
                Generos = musicaRequest.Generos is not null ? GeneroRequestConverter(musicaRequest.Generos) : new List<Genero>()
            };

        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.RecuperarPor(a => a.ArtistaId == id);
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

        app.MapPut("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id, [FromBody] MusicaRequestEdit musicaRequestEdit) =>
        {
            var musicaExistente = dal.RecuperarPor(m => m.Id == musicaRequestEdit.Id);
            if (musicaExistente == null)
            {
                return Results.NotFound();
            }
            else
            {
                musicaExistente.Nome = musicaRequestEdit.Nome;
                musicaExistente.AnoLancamento = musicaRequestEdit.AnoLancamento;
                dal.Atualizar(musicaExistente);
                return Results.Ok();
            }
        });
    }

    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos)
    {
        return generos.Select(a => RequestToEntity(a)).ToList();
    }

    private static Genero RequestToEntity(GeneroRequest genero)
    {
        return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
    }
    private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
    {
        return musicaList.Select(a => EntityToResponse(a)).ToList();
    }

    private static MusicaResponse EntityToResponse(Musica musica)
    {
        return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.ArtistaId, musica.Artista.Nome);
    }
}

