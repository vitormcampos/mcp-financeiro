using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class CreateConta
{
    /// <summary>
    /// Id do Tipo de conta
    /// </summary>
    [Required]
    public required string Description { get; init; }

    /// <summary>
    /// Valor da conta a ser registrada
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Amount { get; init; }

    /// <summary>
    /// Status da conta (PENDENTE, PAGO)
    /// </summary>
    [AllowedValues(
        "PENDING",
        "PAID",
        ErrorMessage = "Status inválido. Valores permitidos: PENDING, PAID."
    )]
    public required string Status { get; init; }

    [Required]
    [AllowedValues(
        "INCOME",
        "EXPENSE",
        "INVESTMENT",
        ErrorMessage = "Status inválido. Valores permitidos: INCOME, EXPENSE ou INVESTMENT."
    )]
    public required string Type { get; init; }
}
