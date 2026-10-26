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
            Helper.Mensagem($"\n{Helper.FormatarNumero(registro.A)} {registro.Simbolo} {Helper.FormatarNumero(registro.B)} = {Helper.FormatarNumero(registro.Resultado!.Value)}", false, true);
        else
            Helper.Mensagem($"{Helper.FormatarNumero(registro.A)} {registro.Simbolo} {Helper.FormatarNumero(registro.B)} = [ERRO] {registro.Erro}", true, true);
    }

    private static void VerRegistro(HistoricoService historico)
    {
        Helper.Mensagem("=== Histórico ===\n", true, false);

        Console.WriteLine($"{"Nome", -18} | {"Operação", -12} | {"Resultado", -40} | {"Data", -12} \n----------------------------------------------------------------------------------------------");
        if (historico.Registros.Count == 0) Console.WriteLine("\nHistórico vazio.");
        foreach (var r in historico.Registros)
        {
            string nome = r.Nome;
            string data = r.Data.ToString("dd/MM/yyyy");
            string operacao = $"{Helper.FormatarNumero(r.A)} {r.Simbolo} {Helper.FormatarNumero(r.B)}";
            string resultatado = r.Erro is null
                ? $"{Helper.FormatarNumero(r.Resultado!.Value)}"
                : $"[ERRO] {r.Erro}";
            
            Console.WriteLine($"{nome, -18} | {operacao, -12} | {resultatado, -40} | {data, -12}");
        }
        Helper.Mensagem("", false, true);
    }

    private static void LimparRegistro(HistoricoService historico)
    {
        historico.Limpar();

        Helper.Mensagem("Histórico limpo.", true, true);
    }
}
