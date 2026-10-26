using Calculadora.Services;
using Calculadora.UI;

namespace Calculadora;

public class Program
{
    public static void Main()
    {
        HistoricoService historico = new HistoricoService();

        Menu.MenuPrincipal(historico);
    }
}
