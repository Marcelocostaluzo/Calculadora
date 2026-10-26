using Calculadora.Operacoes;
using Calculadora.Services;
using Calculadora.Models;

namespace Calculadora.Tests;

public class RegistroCalculoTests
{
    public static IEnumerable<object[]> Cenarios => new List<object[]>
    {
        new object[] {new Soma(), 1, 2, 3, "Adição", "+"},
        new object[] {new Subtracao(), 1, 2, -1, "Subtração", "-"},
        new object[] {new Multiplicacao(), 1, 2, 2, "Multiplicação", "x"}, 
        new object[] {new Divisao(), 1, 2, 0.5, "Divisão", "÷"},  
    };

    [Theory]
    [MemberData(nameof(Cenarios))]
    public void RegistroCalculo_TestaVariosCenarios_DevolveOResultadoEsperado(IOperacao op, decimal a, decimal b, decimal esperado, string nomeEsperado, string simboloEsperado)
    {
        RegistroCalculo registro = Calcular.Executar(op, a, b);

        DateTime dataEsperada = DateTime.Now;

        Assert.Equal(a, registro.A);
        Assert.Equal(simboloEsperado, registro.Simbolo);
        Assert.Equal(b, registro.B);
        Assert.Equal(esperado, registro.Resultado);
        Assert.Null(registro.Erro);
        Assert.Equal($"{dataEsperada:dd/MM/yyyy}", $"{registro.Data:dd/MM/yyyy}");
        Assert.Equal(nomeEsperado, registro.Nome);
    }

    [Fact]
    public void RegistroCalculo_CenarioComErro_DevolverResultadoEsperado()
    {
        var divisao = new Divisao();

        var antes = DateTime.Now;
        RegistroCalculo registro = Calcular.Executar(divisao, 2, 0);
        var depois = DateTime.Now;


        decimal valorAEsperado = 2;
        decimal valorBEsperado = 0;
        string nomeEsperado = "Divisão";
        string simboloEsperado = "÷";
        string txtErroEsperado = "Não é possível dividir por zero";

        Assert.InRange(registro.Data, antes, depois);
        Assert.Equal(valorAEsperado, registro.A);
        Assert.Equal(simboloEsperado, registro.Simbolo);
        Assert.Equal(valorBEsperado, registro.B);
        Assert.Null(registro.Resultado);
        Assert.Equal(txtErroEsperado, registro.Erro);
        Assert.Equal(nomeEsperado, registro.Nome);
    }
}
