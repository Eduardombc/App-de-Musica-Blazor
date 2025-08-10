using ScreenSound.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests;

public record MusicaRequest (string Nome, int Id, int AnoLancamento);
