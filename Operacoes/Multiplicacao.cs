namespace Calculadora.Operacoes;

public class Multiplicacao : IOperacao
{
    public string Nome => "Multiplicação";
    public string Simbolo => "x";
    public decimal Executar(decimal a, decimal b) => a * b;
}
