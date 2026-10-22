using Calculadora.Helpers;
using Calculadora.Models;
using Calculadora.Operacoes;
using Calculadora.Services;

namespace Calculadora.UI;

public class Menu
{
    public static void MenuPrincipal()
    {
        string[] opcoes = ["Calculadora", "Ver histórico", "Limpar histórico", "Sair"];

        while (true)
        {
            Helper.Mensagem("=== Menu ===\n", true, false);
            Helper.CriarMenu(opcoes);
            int escolha = Helper.ValidarInt("\nEscolha uma das opções");
            switch (escolha)
            {
                case 1: MenuCalculadora(); break;
                case 2: Helper.Mensagem("A faser...", true, true); break;
                case 3: Helper.Mensagem("A fazer...", true, true); break;
                case 4: Helper.Mensagem("Saindo...", true, true); return;
                default: Helper.Mensagem("Escolha uma opção válida.", true, true); break;
            }
        }
    }

    private static void MenuCalculadora()
    {
        List<IOperacao> operacoes = [ new Soma(), new Subtracao(), new Divisao(), new Multiplicacao() ];
        string[] opcoes = [.. operacoes.Select(o => o.Nome), "Voltar"];

        int voltar = opcoes.Length;

        while (true)
        {
            Helper.Mensagem("=== Calculadora ===\n", true, false);
            Helper.CriarMenu(opcoes);
            int escolha = Helper.ValidarInt("\nEscolha uma das opções");

            if (escolha == voltar) return;

            if (escolha >= 1 && escolha <= operacoes.Count)
                Calculadora(operacoes[escolha - 1]);
            else
                Helper.Mensagem("Escolha uma opção válida.", true, true);
        }
    }

    private static void Calculadora(IOperacao operacao)
    {
        Helper.Mensagem($"=== {operacao.Nome} ===", true, false);
        decimal a = Helper.ValidarDecimal("\nDigite o primeiro valor");
        decimal b = Helper.ValidarDecimal("Digite o segundo valor");

        RegistroCalculo registro = Calcular.Executar(operacao, a, b);

        if (registro.Erro is null)
            Helper.Mensagem($"\n{registro.A} {registro.Simbolo} {registro.B} = {registro.Resultado:f2}", false, true);
        else
            Helper.Mensagem($"{registro.A} {registro.Simbolo} {registro.B} = [ERRO] {registro.Erro}", true, true);
    }
}
