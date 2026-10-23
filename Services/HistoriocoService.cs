using Calculadora.Models;

namespace Calculadora.Services;

public class HistoricoService
{
    private readonly List<RegistroCalculo> _registro = [];

    public IReadOnlyList<RegistroCalculo> Registros => _registro;

    public void Adicionar(RegistroCalculo r) => _registro.Add(r);

    public void Limpar() => _registro.Clear();
}
