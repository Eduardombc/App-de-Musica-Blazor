using System.Text.Json.Serialization;
using ScreenSound.API.EndPoints;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.AddEndPointsArtistas();
app.AddExtensionsMusicas();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
