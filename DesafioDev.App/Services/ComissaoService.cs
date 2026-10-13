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
        {
            return 0m;
        }

        if (valorVenda < 500m)
        {
            return valorVenda * 0.01m;
        }

        return valorVenda * 0.05m;
    }
}
