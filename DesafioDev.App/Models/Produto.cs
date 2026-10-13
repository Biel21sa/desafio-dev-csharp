namespace DesafioDev.App.Models;

public class Produto
{
    public int CodigoProduto { get; set; }

    public string DescricaoProduto { get; set; } = string.Empty;

    public int Estoque { get; set; }
}
