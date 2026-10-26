using Calculadora.Operacoes;

namespace Calculadora.Tests;

public class OperacoesTests
{
    // Soma 

    [Fact]
    public void Soma_DevolveORespectivoNomeESimbolo()
    {
        var soma = new Soma();

        string nome = "Adição";
        string simbolo = "+";

        Assert.Equal(nome, soma.Nome);
        Assert.Equal(simbolo, soma.Simbolo);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-2, 3, 1)]
    [InlineData(0, 0, 0)]
    public void Soma_SomaOsValores_DevolveOResultadoEsperado(decimal a, decimal b, decimal c)
    {
        Assert.Equal(c, new Soma().Executar(a, b));
    }

    // Subtracao

    [Fact]
    public void Subtracao_DevolveORespectivoNomeESimbolo()
    {
        var subtracao = new Subtracao();

        string nome = "Subtração";
        string simbolo = "-";

        Assert.Equal(nome, subtracao.Nome);
        Assert.Equal(simbolo, subtracao.Simbolo);
    }

    [Theory]
    [InlineData(3, 2, 1)]
    [InlineData(-1, 1, -2)]
    [InlineData(0, 0, 0)]
    public void Subtracao_SubtraiOsValores_DevolveOResultadoEsperado(decimal a, decimal b, decimal c)
    {
        Assert.Equal(c, new Subtracao().Executar(a, b));
    }

    // Multiplicacao

    [Fact]
    public void Multiplicacao_DevolveORespectivoNomeESimbolo()
    {
        var multiplicacao = new Multiplicacao();

        string nome = "Multiplicação";
        string simbolo = "x";

        Assert.Equal(nome, multiplicacao.Nome);
        Assert.Equal(simbolo, multiplicacao.Simbolo);
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 5, 10)]
    [InlineData(-2, 4, -8)]
    public void Multiplicacao_MultiplicaOsValores_DevolverOResultadoEsperado(decimal a, decimal b, decimal c)
    {
        Assert.Equal(c, new Multiplicacao().Executar(a, b));
    }

    // Divisao

    [Fact]
    public void Divisao_DevolveORespectivoNomeESimbolo()
    {
        var divisao = new Divisao();

        string nome = "Divisão";
        string simbolo = "÷";

        Assert.Equal(nome, divisao.Nome);
        Assert.Equal(simbolo, divisao.Simbolo);
    }

    [Theory]
    [InlineData(-2, 4, -0.5)]
    [InlineData(10, 5, 2)]
    [InlineData(50.5, 2, 25.25)]
    public void Divisao_DivideOsValores_DevolveOResultadoEsperado(decimal a, decimal b, decimal c)
    {
        Assert.Equal(c, new Divisao().Executar(a, b));
    }

    [Fact]
    public void Divisao_DividirValorPorZero_LancaExcecao()
    {
        Assert.Throws<DivideByZeroException>(() => new Divisao().Executar(1, 0));
    }
}
