using System.Text.Json.Serialization;
using ScreenSound.API.EndPoints;
using ScreenSound.Banco;
using ScreenSound.Modelos;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ScreenSoundContext>();
        builder.Services.AddTransient<DAL<Artista>>();
        builder.Services.AddTransient<DAL<Musica>>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });


        var app = builder.Build();
        app.UseCors();


        app.AddEndPointsArtistas();
        app.AddEndPointsMusicas();
        app.AddEndPointGeneros();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.Run();
    }
}