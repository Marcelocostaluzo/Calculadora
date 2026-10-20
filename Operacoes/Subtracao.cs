namespace Calculadora.Operacoes;

public class Subtracao : IOperacao
{
    public string Nome => "Subtração";
    public string Simbolo => "-";
    public decimal Executar(decimal a, decimal b) => a - b;
}
