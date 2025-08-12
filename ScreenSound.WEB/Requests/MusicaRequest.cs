using System.ComponentModel.DataAnnotations;
namespace ScreenSound.WEB.Requests;

public record MusicaRequest ([Required] string Nome, [Required] int Id, int AnoLancamento,ICollection<GeneroRequest> Generos = null);
