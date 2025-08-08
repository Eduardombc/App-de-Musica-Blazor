using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.API.Requests;
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

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            dal.Adicionar(new Artista(artistaRequest.nome, artistaRequest.bio));
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
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

        app.MapPut("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
        {
            var artistaExistente = dal.RecuperarPor(a => a.ArtistaId == artistaRequestEdit.id);
            if (artistaExistente == null)
            {
                return Results.NotFound();
            }
            else
            {
                artistaExistente.Nome = artistaRequestEdit.nome;
                artistaExistente.Bio = artistaRequestEdit.bio;
                dal.Atualizar(artistaExistente);
                return Results.Ok();
            }
        });

        private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
        {
            return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
        }

        private static ArtistaResponse EntityToResponse(Artista artista)
        {
            return new ArtistaResponse(artista.ArtistaId, artista.Nome, artista.Bio, artista.FotoPerfil);
        }
}

