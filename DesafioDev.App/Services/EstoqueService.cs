using DesafioDev.App.Models;

namespace DesafioDev.App.Services;

public class EstoqueService
{
    private readonly List<Produto> _produtos;
    private readonly List<MovimentacaoEstoque> _movimentacoes = [];

    public EstoqueService(List<Produto> produtos)
    {
        _produtos = produtos;
    }

    public IReadOnlyList<MovimentacaoEstoque> Movimentacoes
        => _movimentacoes;

    public IReadOnlyList<Produto> Produtos
        => _produtos;

    public MovimentacaoEstoque RegistrarEntrada(
        int codigoProduto,
        int quantidade)
    {
        ValidarQuantidade(quantidade);

        Produto produto = BuscarProduto(codigoProduto);

        produto.Estoque += quantidade;

        return RegistrarMovimentacao(
            produto,
            "Entrada",
            quantidade
        );
    }

    public MovimentacaoEstoque RegistrarSaida(
        int codigoProduto,
        int quantidade)
    {
        ValidarQuantidade(quantidade);

        Produto produto = BuscarProduto(codigoProduto);

        if (quantidade > produto.Estoque)
        {
            throw new InvalidOperationException(
                $"Estoque insuficiente para o produto " +
                $"{produto.DescricaoProduto}. " +
                $"Disponível: {produto.Estoque}."
            );
        }

        produto.Estoque -= quantidade;

        return RegistrarMovimentacao(
            produto,
            "Saída",
            quantidade
        );
    }

    private Produto BuscarProduto(int codigoProduto)
    {
        Produto? produto = _produtos.FirstOrDefault(
            p => p.CodigoProduto == codigoProduto
        );

        if (produto is null)
        {
            throw new ArgumentException(
                $"Produto com código {codigoProduto} não encontrado."
            );
        }

        return produto;
    }

    private static void ValidarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException(
                "A quantidade deve ser maior que zero."
            );
        }
    }

    private MovimentacaoEstoque RegistrarMovimentacao(
        Produto produto,
        string tipo,
        int quantidade)
    {
        var movimentacao = new MovimentacaoEstoque
        {
            CodigoProduto = produto.CodigoProduto,
            DescricaoProduto = produto.DescricaoProduto,
            Tipo = tipo,
            Quantidade = quantidade,
            EstoqueFinal = produto.Estoque,
            Descricao = $"{tipo} de {quantidade} unidade(s) do produto " +
            $"{produto.DescricaoProduto}",
        };

        _movimentacoes.Add(movimentacao);

        return movimentacao;
    }
}
