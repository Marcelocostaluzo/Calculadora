using Calculadora.Helpers;
using Calculadora.Models;
using Calculadora.Operacoes;
using Calculadora.Services;

namespace Calculadora.UI;

public class Menu
{
    public static void MenuPrincipal(HistoricoService historico)
    {
        string[] opcoes = ["Calculadora", "Ver histórico", "Limpar histórico", "Sair"];

        while (true)
        {
            Helper.Mensagem("=== Menu ===\n", true, false);
            Helper.CriarMenu(opcoes);
            int escolha = Helper.ValidarInt("\nEscolha uma das opções");
            switch (escolha)
            {
                case 1: MenuCalculadora(historico); break;
                case 2: VerRegistro(historico); break;
                case 3: LimparRegistro(historico); break;
                case 4: Helper.Mensagem("Saindo...", true, true); return;
                default: Helper.Mensagem("Escolha uma opção válida.", true, true); break;
            }
        }
    }

    private static void MenuCalculadora(HistoricoService historico)
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
                Calculadora(operacoes[escolha - 1], historico);
            else
                Helper.Mensagem("Escolha uma opção válida.", true, true);
        }
    }

    private static void Calculadora(IOperacao operacao, HistoricoService historico)
    {
        Helper.Mensagem($"=== {operacao.Nome} ===", true, false);
        decimal a = Helper.ValidarDecimal("\nDigite o primeiro valor");
        decimal b = Helper.ValidarDecimal("Digite o segundo valor");

        RegistroCalculo registro = Calcular.Executar(operacao, a, b);
        historico.Adicionar(registro);

        if (registro.Erro is null)
            Helper.Mensagem($"\n{registro.A} {registro.Simbolo} {registro.B} = {registro.Resultado:f2}", false, true);
        else
            Helper.Mensagem($"{registro.A} {registro.Simbolo} {registro.B} = [ERRO] {registro.Erro}", true, true);
    }

    private static void VerRegistro(HistoricoService historico)
    {
        Helper.Mensagem("=== Histórico ===\n", true, false);

        Console.WriteLine($"{"Operação", -12} | {"Resultado", -12} \n----------------------------------------------------------------------------------");
        if (historico.Registros.Count == 0) Console.WriteLine("\nHistórico vazio.");
        for (var i = 0; i < historico.Registros.Count; i++)
        {
            if (historico.Registros[i].Erro is null)
                Console.WriteLine($"{historico.Registros[i].A} {historico.Registros[i].Simbolo} {historico.Registros[i].B, -8} | {historico.Registros[i].Resultado:f2}");
            
            else 
                Console.WriteLine($"{historico.Registros[i].A} {historico.Registros[i].Simbolo} {historico.Registros[i].B, -8} | [ERRO] {historico.Registros[i].Erro}");
        }
        Helper.Mensagem("", false, true);
    }

    private static void LimparRegistro(HistoricoService historico)
    {
        historico.Limpar();

        Helper.Mensagem("Histórico limpo.", true, true);
    }
}
