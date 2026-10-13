namespace DesafioDev.App.Models;

public class ResultadoJuros
{
    public decimal ValorOriginal { get; set; }

    public DateOnly DataVencimento { get; set; }

    public DateOnly DataCalculo { get; set; }

    public int DiasAtraso { get; set; }

    public decimal ValorJuros { get; set; }

    public decimal ValorTotal { get; set; }
}
