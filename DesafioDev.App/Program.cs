using System.Text.Json;
using DesafioDev.App.Models;
using DesafioDev.App.Services;

var opcoes = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

// =====================================
// EXERCÍCIO 1 - COMISSÕES
// =====================================

string caminhoVendas = Path.Combine(
    AppContext.BaseDirectory,
    "Data",
    "vendas.json"
);

string jsonVendas = File.ReadAllText(caminhoVendas);

VendasData? dadosVendas =
    JsonSerializer.Deserialize<VendasData>(
        jsonVendas,
        opcoes
    );

if (dadosVendas is null)
{
    Console.WriteLine("Não foi possível carregar as vendas.");
    return;
}

var comissaoService = new ComissaoService();

List<ResumoComissao> resumos =
    comissaoService.GerarResumo(dadosVendas.Vendas);

Console.WriteLine("===== RESUMO DE COMISSÕES =====");
Console.WriteLine();

foreach (ResumoComissao resumo in resumos)
{
    Console.WriteLine($"Vendedor: {resumo.Vendedor}");
    Console.WriteLine($"Total vendido: R$ {resumo.TotalVendido:F2}");
    Console.WriteLine($"Total de comissão: R$ {resumo.TotalComissao:F2}");
    Console.WriteLine("--------------------------------");
}

// =====================================
// EXERCÍCIO 2 - CONTROLE DE ESTOQUE
// =====================================

string caminhoEstoque = Path.Combine(
    AppContext.BaseDirectory,
    "Data",
    "estoque.json"
);

string jsonEstoque = File.ReadAllText(caminhoEstoque);

EstoqueData? dadosEstoque =
    JsonSerializer.Deserialize<EstoqueData>(
        jsonEstoque,
        opcoes
    );

if (dadosEstoque is null)
{
    Console.WriteLine("Não foi possível carregar o estoque.");
    return;
}

var estoqueService = new EstoqueService(dadosEstoque.Estoque);

// Exemplos de movimentações para testar o sistema.
estoqueService.RegistrarEntrada(101, 20);
estoqueService.RegistrarSaida(101, 30);
estoqueService.RegistrarSaida(102, 10);

// Exibir histórico de movimentações.
Console.WriteLine();
Console.WriteLine("===== HISTÓRICO DE MOVIMENTAÇÕES =====");
Console.WriteLine();

foreach (MovimentacaoEstoque movimentacao
         in estoqueService.Movimentacoes)
{
    Console.WriteLine($"ID: {movimentacao.Id}");
    Console.WriteLine($"Produto: {movimentacao.DescricaoProduto}");
    Console.WriteLine($"Descrição: {movimentacao.Descricao}");
    Console.WriteLine($"Tipo: {movimentacao.Tipo}");
    Console.WriteLine($"Quantidade: {movimentacao.Quantidade}");
    Console.WriteLine($"Estoque após movimentação: {movimentacao.EstoqueFinal}");
    Console.WriteLine($"Data: {movimentacao.DataHora:dd/MM/yyyy HH:mm:ss}");
    Console.WriteLine("--------------------------------");
}

// Exibir estoque final de todos os produtos.
Console.WriteLine();
Console.WriteLine("===== ESTOQUE FINAL =====");
Console.WriteLine();

foreach (Produto produto in estoqueService.Produtos)
{
    Console.WriteLine(
        $"Código: {produto.CodigoProduto} | " +
        $"Produto: {produto.DescricaoProduto} | " +
        $"Estoque: {produto.Estoque}"
    );
}
