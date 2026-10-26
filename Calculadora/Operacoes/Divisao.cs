namespace Calculadora.Operacoes;

public class Divisao : IOperacao
{
    public string Nome => "Divisão";
    public string Simbolo => "÷";
    public decimal Executar(decimal a, decimal b)
    {
        if (b == 0) throw new DivideByZeroException("Não é possível dividir por zero");
        return a / b;
    }
}
