using Calculadora.Helpers;

namespace Calculadora.Tests;

public class HelperTests : IDisposable
{
    private readonly TextWriter _saidaOriginal = Console.Out;
    private readonly StringWriter _saida = new();

    public HelperTests()
    {
        Console.SetOut(_saida);
    }

    public void Dispose()
    {
        Console.SetOut(_saidaOriginal);
        _saida.Dispose();
    }

    [Fact]
    public void CriarMenu_ComTresOpcoes_ImprimeMenuNumerado()
    {
        var opcoes = new[] { "Somar", "Subtrair", "Sair" };

        Helper.CriarMenu(opcoes);

        var esperado = 
            "[1] Somar" + Environment.NewLine +
            "[2] Subtrair" + Environment.NewLine +
            "[3] Sair" + Environment.NewLine;
        
        Assert.Equal(esperado, _saida.ToString());
    }

    [Fact]
    public void CriaMenu_ComArrayVazio_NaoImprimeNada()
    {
        Helper.CriarMenu(Array.Empty<string>());

        Assert.Equal(string.Empty, _saida.ToString());
    }

    [Theory]
    [InlineData(5, "5")]
    [InlineData(2.5, "2,5")]
    [InlineData(0.001, "0,001")]
    [InlineData(3.33333, "3,3333")]
    public void FormatarNumero_RemoveZerosSobrando_LimitaA4Casas(decimal valor, string esperado)
    {
        Assert.Equal(esperado, Helper.FormatarNumero(valor));
    }
}
