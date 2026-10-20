namespace Calculadora.Operacoes;

public class Soma : IOperacao
{
    public string Nome => "Adição";
    public string Simbolo => "+";
    public decimal Executar(decimal a, decimal b) => a + b;
}
