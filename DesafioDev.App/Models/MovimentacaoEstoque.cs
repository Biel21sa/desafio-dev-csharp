namespace DesafioDev.App.Models;

public class MovimentacaoEstoque
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int CodigoProduto { get; set; }

    public string DescricaoProduto { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public DateTime DataHora { get; set; } = DateTime.Now;

    public int EstoqueFinal { get; set; }

    public string Descricao { get; set; } = string.Empty;
}
