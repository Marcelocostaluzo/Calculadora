using Calculadora.Operacoes;
using Calculadora.Services;

namespace Calculadora.Tests;

public class HistoricoTests
{
    [Fact]
    public void Adicionar_CriaNovoHistoricoERegistro_VerificaEstadoAntesEDepois()
    {
        var historico = new HistoricoService();
        var registro = Calcular.Executar(new Soma(), 1, 2);

        Assert.Empty(historico.Registros);

        historico.Adicionar(registro);

        Assert.Single(historico.Registros);
        Assert.Equal(3, historico.Registros[0].Resultado);
    }

    [Fact]
    public void Adicionar_MantemAOrdemDeInsercao()
    {
        var historico = new HistoricoService();

        Assert.Empty(historico.Registros);

        historico.Adicionar(Calcular.Executar(new Soma(), 1, 2));
        historico.Adicionar(Calcular.Executar(new Subtracao(), 1, 2));

        Assert.NotEmpty(historico.Registros);

        Assert.Equal("+", historico.Registros[0].Simbolo);
        Assert.Equal("-", historico.Registros[1].Simbolo);
    }

    [Fact]
    public void Limpar_AdicionaDadosAoRegistro_LimpaORegistro()
    {
        var historico = new HistoricoService();

        Assert.Empty(historico.Registros);

        historico.Adicionar(Calcular.Executar(new Soma(), 1, 2));
        historico.Adicionar(Calcular.Executar(new Multiplicacao(), 3, 4));

        Assert.NotEmpty(historico.Registros);

        historico.Limpar();

        Assert.Empty(historico.Registros);
    }
}
