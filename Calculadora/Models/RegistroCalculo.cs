namespace Calculadora.Models;

public record RegistroCalculo(
        decimal A, 
        string Simbolo, 
        decimal B, 
        decimal? Resultado, 
        string? Erro, 
        DateTime Data, 
        string Nome
    );
