using System.ComponentModel.DataAnnotations;

namespace FinanceiroBackend.Dtos;

public class CreateConta
{
    /// <summary>
    /// Id do Tipo de conta
    /// </summary>
    public string TipoId { get; init; }

    /// <summary>
    /// Valor da conta a ser registrada
    /// </summary>
    public decimal Valor { get; init; }

    /// <summary>
    /// Status da conta (PENDENTE, PAGO)
    /// </summary>
    [AllowedValues("PENDENTE", "PAGO")]
    public string Status { get; init; }
}
