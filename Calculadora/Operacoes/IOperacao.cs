namespace Calculadora.Operacoes;

public interface IOperacao
{
    string Nome { get; }
    string Simbolo { get; }
    decimal Executar(decimal a, decimal b);
}
