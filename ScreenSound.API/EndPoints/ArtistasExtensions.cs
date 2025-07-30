using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints;

public static class ArtistasExtensions
{
    public static void AddEndPointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Artistas/{Nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            var artista = dal.RecuperarPor(a => a.Nome.ToLower().Equals(nome.ToLower()));
            if (artista == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(artista);
            }
        });

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) =>
        {
            dal.Adicionar(artista);
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
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

        app.MapPut("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id, [FromBody] Artista artista) =>
        {
            var artistaExistente = dal.RecuperarPor(a => a.Id == id);
            if (artistaExistente == null)
            {
                return Results.NotFound();
            }
            else
            {
                artista.Nome = artistaExistente.Nome;
                artista.Bio = artistaExistente.Bio;
                artista.FotoPerfil = artistaExistente.FotoPerfil;
                dal.Atualizar(artista);
                return Results.Ok();
            }
        });
    }
}
