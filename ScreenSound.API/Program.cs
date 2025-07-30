using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{Nome}", ([FromServices] DAL<Artista> dal,string nome) =>
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


app.Run();
