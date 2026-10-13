using DesafioDev.App.Models;

namespace DesafioDev.App.Services;

public class JurosService
{
    private const decimal TaxaDiaria = 0.025m;

    public ResultadoJuros Calcular(
        decimal valorOriginal,
        DateOnly dataVencimento,
        DateOnly dataCalculo)
    {
        if (valorOriginal < 0)
        {
            throw new ArgumentException(
                "O valor original não pode ser negativo."
            );
        }

        int diasAtraso = Math.Max(
            0,
            dataCalculo.DayNumber - dataVencimento.DayNumber
        );

        decimal valorJuros =
            valorOriginal * TaxaDiaria * diasAtraso;

        return new ResultadoJuros
        {
            ValorOriginal = valorOriginal,
            DataVencimento = dataVencimento,
            DataCalculo = dataCalculo,
            DiasAtraso = diasAtraso,
            ValorJuros = valorJuros,
            ValorTotal = valorOriginal + valorJuros
        };
    }
}
