
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

foreach (Venda venda in dados.Vendas)
{
    decimal comissao = comissaoService.CalcularComissao(venda.Valor);

    Console.WriteLine(
        $"Vendedor: {venda.Vendedor} | " +
        $"Venda: R$ {venda.Valor:F2} | " +
        $"Comissão: R$ {comissao:F2}"
    );
}
