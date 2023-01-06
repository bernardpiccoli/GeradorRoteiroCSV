using GeradorRoteiroExecucao;
using System.Configuration;

Console.WriteLine("O CSV será montado de acordo com a ordem alfabetica dos arquivos na pasta. Coloque-os na pasta script");
Console.WriteLine("O CSV ficará no formato: (1, 001_SCRIPT.SQL, host, schema).");
Console.WriteLine("Pressione ENTER para confirmar!");
Console.ReadKey();

Console.WriteLine("Digite o nome do host");
var host = Console.ReadLine();
if (string.IsNullOrEmpty(host)) return;

Console.WriteLine("Digite o nome do schema");
var schema = Console.ReadLine();
if (string.IsNullOrEmpty(schema)) return;

var folder = ConfigurationManager.AppSettings["folder"];
if (string.IsNullOrEmpty(folder)) { Console.WriteLine("Variável folder não declarada na App.config."); return; };

var arquivoCSV = ConfigurationManager.AppSettings["arquivoCSV"];
if (string.IsNullOrEmpty(arquivoCSV)) { Console.WriteLine("Variável folder não declarada na App.config."); return; } ;

var arquivosEncontrados = RetornaNomeArquivos(AppDomain.CurrentDomain.BaseDirectory + "\\" + folder);

if (arquivosEncontrados.Count <= 0)
    Console.WriteLine("A pasta não possui arquivos.");
else
    EscreverLinhasCSV(MontarLinhasObjeto(arquivosEncontrados, host, schema), folder, arquivoCSV);
//arquivosEncontrados.ForEach(delegate(string nome) { Console.WriteLine(nome); });

Console.WriteLine("Concluído com sucesso!");
Console.ReadKey();

static List<string> RetornaNomeArquivos(string caminho)
{
    //Também pega informações de um arquivo passado o path:
    //FileInfo infoArquivo = new FileInfo("E:\\NFE.xml");
    //infoArquivo.Name

    if (Directory.Exists(caminho) && !string.IsNullOrEmpty(caminho))
        return Directory.GetFiles(caminho).ToList().Select(x => { x = Path.GetFileName(x); return x; }).ToList();
    //Directory.GetFiles(caminho).ToList().Select(x => Path.GetFileName(x)).ToList();
    else
        return new List<string>();
}

static List<Roteiro> MontarLinhasObjeto(List<string> nomeArquivos, string host, string schema)
{
    List<Roteiro> roteiros = new List<Roteiro>();

    foreach (var nomeArquivo in nomeArquivos)
    {
        var roteiro = new Roteiro
        {
            Id = nomeArquivos.IndexOf(nomeArquivo)+1,
            Arquivo = nomeArquivo,
            Schema = schema,
            Host = host
        };
        roteiros.Add(roteiro);
    }
    return roteiros;
}

static void EscreverLinhasCSV(List<Roteiro> roteiros, string folder, string arquivoCSV)
{

    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + folder + "\\" + arquivoCSV))
        Console.WriteLine("Já existe um arquivo de roteiro na pasta. Delete-o antes de executar.");

    else // temos duas formas de converter pra string ( veja na classe Roteiro )
        //File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\" + folder + "\\" + arquivoCSV, roteiros.Select(roteiro => roteiro.ToString()).ToList());
        File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\" + folder + "\\" + arquivoCSV, roteiros.Select(roteiro => (string)roteiro).ToList());
}


