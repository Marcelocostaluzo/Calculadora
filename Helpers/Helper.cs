namespace Calculadora.Helpers;

public class Helper
{
    public static void CriarMenu(string[] opcoes)
    {
        for (var i = 0; i < opcoes.Length; i++)
        {
            Console.WriteLine($"[{i + 1}] {opcoes[i]}");
        }
    }

    public static void Mensagem(string mensagem, bool limpar, bool pausar)
    {
        if (limpar) Console.Clear();
        Console.WriteLine($"{mensagem}");
        if (pausar) Console.ReadKey();
    }

    public static int ValidarInt(string mensagem)
    {
        while (true)
        {
            Console.Write($"{mensagem}: ");
            if (int.TryParse(Console.ReadLine(), out int valido))
                return valido;
            
            Console.WriteLine("[ERRO] valor inválido");
        }
    }

    public static decimal ValidarDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write($"{mensagem}: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal valido))
                return valido;
            
            Console.WriteLine("[ERRO] valor inválido");
        }
    }
}
