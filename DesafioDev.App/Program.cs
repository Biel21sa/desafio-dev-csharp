using System.Text.Json;
using DesafioDev.App.Models;
using DesafioDev.App.Services;

string caminhoArquivo = Path.Combine(
    AppContext.BaseDirectory,
    "Data",
    "vendas.json"
);

string json = File.ReadAllText(caminhoArquivo);

var opcoes = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

VendasData? dados = JsonSerializer.Deserialize<VendasData>(
    json,
    opcoes
);

if (dados is null)
{
    Console.WriteLine("Não foi possível carregar as vendas.");
    return;
}

var comissaoService = new ComissaoService();

List<ResumoComissao> resumos =
    comissaoService.GerarResumo(dados.Vendas);

Console.WriteLine("===== RESUMO DE COMISSÕES =====");
Console.WriteLine();

foreach (ResumoComissao resumo in resumos)
{
    Console.WriteLine($"Vendedor: {resumo.Vendedor}");
    Console.WriteLine($"Total vendido: R$ {resumo.TotalVendido:F2}");
    Console.WriteLine($"Total de comissão: R$ {resumo.TotalComissao:F2}");
    Console.WriteLine("--------------------------------");
}
