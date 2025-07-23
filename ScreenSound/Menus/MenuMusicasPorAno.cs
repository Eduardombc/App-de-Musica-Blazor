using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMusicasPorAno : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        base.Executar(artistaDAL);
        ExibirTituloDaOpcao("Mostrar música por ano de lançamento");
        Console.Write("Digite o ano para consultar músicas: ");
        string anoDaMusica = Console.ReadLine()!;
        var musicaDAL = new DAL<Musica>(new ScreenSoundContext);
        var listaAnoLancamento = musicaDAL.ListarPor(a => a.AnoLancamento == Convert.ToInt32(anoDaMusica));
        if(listaAnoLancamento.Any())
        {
            Console.WriteLine($"\nMúsicas lançadas no ano de {anoDaMusica}:");
            foreach (var musica in listaAnoLancamento)
            {
                musica.ExibirFichaTecnica();
            }
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nNenhuma música encontrada para o ano de {anoDaMusica}.");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}