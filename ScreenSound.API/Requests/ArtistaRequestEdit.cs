namespace ScreenSound.API.Requests;

public record ArtistaRequestEdit (string nome, int ArtistaId, string bio) : ArtistaRequest(nome,bio);
