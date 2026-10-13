using System.Text.Json;
using DesafioDev.App.Models;
using DesafioDev.App.Services;
using System.Globalization;

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

estoqueService.RegistrarEntrada(101, 20);
estoqueService.RegistrarSaida(101, 30);
estoqueService.RegistrarSaida(102, 10);

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

// =====================================
// EXERCÍCIO 3 - CÁLCULO DE JUROS
// =====================================

Console.WriteLine();
Console.WriteLine("===== CÁLCULO DE JUROS =====");

CultureInfo cultura = CultureInfo.GetCultureInfo("pt-BR");

Console.Write("Informe o valor da dívida (ex.: 200,00): ");
string? valorDigitado = Console.ReadLine();

if (!decimal.TryParse(
        valorDigitado,
        NumberStyles.Number,
        cultura,
        out decimal valorOriginal)
    || valorOriginal < 0)
{
    Console.WriteLine("Valor inválido. Informe um valor não negativo.");
    return;
}

Console.Write("Informe a data de vencimento (dd/MM/yyyy): ");
string? dataDigitada = Console.ReadLine();

if (!DateOnly.TryParseExact(
        dataDigitada,
        "dd/MM/yyyy",
        cultura,
        DateTimeStyles.None,
        out DateOnly dataVencimento))
{
    Console.WriteLine("Data inválida. Utilize o formato dd/MM/yyyy.");
    return;
}

DateOnly dataCalculo = DateOnly.FromDateTime(DateTime.Today);

var jurosService = new JurosService();

ResultadoJuros resultado = jurosService.Calcular(
    valorOriginal,
    dataVencimento,
    dataCalculo
);

Console.WriteLine();
Console.WriteLine("===== RESULTADO =====");
Console.WriteLine($"Valor original: {resultado.ValorOriginal.ToString("C", cultura)}");
Console.WriteLine($"Data de vencimento: {resultado.DataVencimento:dd/MM/yyyy}");
Console.WriteLine($"Data do cálculo: {resultado.DataCalculo:dd/MM/yyyy}");
Console.WriteLine($"Dias de atraso: {resultado.DiasAtraso}");
Console.WriteLine($"Juros: {resultado.ValorJuros.ToString("C", cultura)}");
Console.WriteLine($"Total atualizado: {resultado.ValorTotal.ToString("C", cultura)}");
