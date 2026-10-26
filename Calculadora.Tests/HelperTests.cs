using Calculadora.Helpers;

namespace Calculadora.Tests;

public class HelperTests : IDisposable
{
    private readonly TextWriter _saidaOriginal = Console.Out;
    private readonly TextReader _entradaOriginal = Console.In;
    private readonly StringWriter _saida = new();

    public HelperTests()
    {
        Console.SetOut(_saida);
    }

    public void Dispose()
    {
        Console.SetOut(_saidaOriginal);
        Console.SetIn(_entradaOriginal);
        _saida.Dispose();
    }

    // Simula o usuário digitando uma linha por vez.
    // A ÚLTIMA linha deve ser sempre válida, senão o laço do Helper nunca termina.
    private static void Digitar(params string[] linhas)
    {
        Console.SetIn(new StringReader(string.Join(Environment.NewLine, linhas) + Environment.NewLine));
    }

    // CriarMenu

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
    public void CriarMenu_ComArrayVazio_NaoImprimeNada()
    {
        Helper.CriarMenu(Array.Empty<string>());

        Assert.Equal(string.Empty, _saida.ToString());
    }

    // Mensagem

    [Fact]
    public void Mensagem_SemLimparNemPausar_ImprimeOTextoComQuebraDeLinha()
    {
        Helper.Mensagem("Olá", limpar: false, pausar: false);

        Assert.Equal("Olá" + Environment.NewLine, _saida.ToString());
    }

    // FormatarNumero

    [Theory]
    [InlineData(5, "5")]
    [InlineData(0, "0")]
    [InlineData(2.5, "2,5")]
    [InlineData(-2.5, "-2,5")]
    [InlineData(0.001, "0,001")]
    [InlineData(3.33333, "3,3333")]
    [InlineData(1234.5, "1234,5")]
    public void FormatarNumero_RemoveZerosSobrando_UsaVirgulaELimitaA4Casas(decimal valor, string esperado)
    {
        Assert.Equal(esperado, Helper.FormatarNumero(valor));
    }

    [Fact]
    public void FormatarNumero_DizimaPeriodica_LimitaA4Casas()
    {
        Assert.Equal("3,3333", Helper.FormatarNumero(10m / 3));
    }

    // ValidarDecimal

    [Fact]
    public void ValidarDecimal_VirgulaComoSeparador_Aceita()
    {
        Digitar("3,5");

        decimal valor = Helper.ValidarDecimal("Valor");

        Assert.Equal(3.5m, valor);
        Assert.DoesNotContain("[ERRO]", _saida.ToString());
    }

    [Fact]
    public void ValidarDecimal_NumeroNegativo_Aceita()
    {
        Digitar("-2,5");

        Assert.Equal(-2.5m, Helper.ValidarDecimal("Valor"));
    }

    [Theory]
    [InlineData("3.5")]       // ponto não é separador decimal em pt-BR (e não vira 35)
    [InlineData("1.000,5")]   // separador de milhar não é aceito
    [InlineData("abc")]
    [InlineData("")]
    public void ValidarDecimal_EntradaInvalida_MostraErroEPedeDeNovo(string entradaInvalida)
    {
        Digitar(entradaInvalida, "2,5");

        decimal valor = Helper.ValidarDecimal("Valor");

        Assert.Equal(2.5m, valor);
        Assert.Contains("[ERRO]", _saida.ToString());
    }

    [Fact]
    public void ValidarDecimal_ExibeAMensagemInformada()
    {
        Digitar("1");

        Helper.ValidarDecimal("Digite o primeiro valor");

        Assert.Contains("Digite o primeiro valor: ", _saida.ToString());
    }

    // ValidarInt

    [Fact]
    public void ValidarInt_NumeroInteiro_Aceita()
    {
        Digitar("7");

        Assert.Equal(7, Helper.ValidarInt("Opção"));
        Assert.DoesNotContain("[ERRO]", _saida.ToString());
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("3,5")]
    [InlineData("")]
    public void ValidarInt_EntradaInvalida_MostraErroEPedeDeNovo(string entradaInvalida)
    {
        Digitar(entradaInvalida, "2");

        Assert.Equal(2, Helper.ValidarInt("Opção"));
        Assert.Contains("[ERRO]", _saida.ToString());
    }
}
