namespace ScreenSound.WEB.Requests;

public record MusicaRequestEdit (string Nome, int Id, int AnoLancamento) : MusicaRequest(Nome,Id,AnoLancamento);
