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


bool menuEstoqueAtivo = true;

while (menuEstoqueAtivo)
{
    Console.WriteLine();
    Console.WriteLine("===== CONTROLE DE ESTOQUE =====");
    Console.WriteLine("1 - Registrar entrada");
    Console.WriteLine("2 - Registrar saída");
    Console.WriteLine("3 - Consultar estoque");
    Console.WriteLine("4 - Consultar movimentações");
    Console.WriteLine("0 - Continuar para o próximo exercício");
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
        case "2":
        {
            Console.Write("Código do produto: ");

            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido.");
                continue;
            }

            Console.Write("Quantidade: ");

            if (!int.TryParse(Console.ReadLine(), out int quantidade))
            {
                Console.WriteLine("Quantidade inválida.");
                continue;
            }

            try
            {
                MovimentacaoEstoque movimentacao;

                if (opcao == "1")
                {
                    movimentacao =
                        estoqueService.RegistrarEntrada(codigo, quantidade);
                }
                else
                {
                    movimentacao =
                        estoqueService.RegistrarSaida(codigo, quantidade);
                }

                Console.WriteLine();
                Console.WriteLine("Movimentação registrada com sucesso!");
                Console.WriteLine($"ID: {movimentacao.Id}");
                Console.WriteLine($"Descrição: {movimentacao.Descricao}");
                Console.WriteLine($"Estoque atual: {movimentacao.EstoqueFinal}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            break;
        }

        case "3":
            Console.WriteLine();
            Console.WriteLine("===== ESTOQUE ATUAL =====");

            foreach (Produto produto in estoqueService.Produtos)
            {
                Console.WriteLine(
                    $"Código: {produto.CodigoProduto} | " +
                    $"Produto: {produto.DescricaoProduto} | " +
                    $"Estoque: {produto.Estoque}"
                );
            }

            break;

        case "4":
            Console.WriteLine();
            Console.WriteLine("===== HISTÓRICO DE MOVIMENTAÇÕES =====");

            if (estoqueService.Movimentacoes.Count == 0)
            {
                Console.WriteLine("Nenhuma movimentação registrada.");
                break;
            }

            foreach (
                MovimentacaoEstoque movimentacao
                in estoqueService.Movimentacoes
            )
            {
                Console.WriteLine($"ID: {movimentacao.Id}");
                Console.WriteLine($"Produto: {movimentacao.DescricaoProduto}");
                Console.WriteLine($"Descrição: {movimentacao.Descricao}");
                Console.WriteLine($"Tipo: {movimentacao.Tipo}");
                Console.WriteLine($"Quantidade: {movimentacao.Quantidade}");
                Console.WriteLine($"Estoque final: {movimentacao.EstoqueFinal}");
                Console.WriteLine("--------------------------------");
            }

            break;

        case "0":
            menuEstoqueAtivo = false;
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}

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
