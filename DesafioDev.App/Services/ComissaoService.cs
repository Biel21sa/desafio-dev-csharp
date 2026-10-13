using DesafioDev.App.Models;

namespace DesafioDev.App.Services;

public class ComissaoService
{
    public decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 0)
        {
            throw new ArgumentException(
                "O valor da venda não pode ser negativo."
            );
        }

        if (valorVenda < 100m)
            return 0m;

        if (valorVenda < 500m)
            return valorVenda * 0.01m;

        return valorVenda * 0.05m;
    }

    public List<ResumoComissao> GerarResumo(List<Venda> vendas)
    {
        return vendas
            .GroupBy(venda => venda.Vendedor)
            .Select(grupo => new ResumoComissao
            {
                Vendedor = grupo.Key,
                TotalVendido = grupo.Sum(venda => venda.Valor),
                TotalComissao = grupo.Sum(
                    venda => CalcularComissao(venda.Valor)
                )
            })
            .ToList();
    }
}
