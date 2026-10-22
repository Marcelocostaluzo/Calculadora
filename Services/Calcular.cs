using Calculadora.Models;
using Calculadora.Operacoes;

namespace Calculadora.Services;

public class Calcular
{
    public static RegistroCalculo Executar(IOperacao op, decimal a, decimal b)
    {
        try
        {
            return new (a, op.Simbolo, b, op.Executar(a, b), null, DateTime.Now, op.Nome);

        } catch (DivideByZeroException ex)
        {
            return new (a, op.Simbolo, b, null, ex.Message, DateTime.Now, op.Nome);
        }
    }
}
