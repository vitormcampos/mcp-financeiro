namespace FinanceiroBackend.Dtos;

public record class CreateConta
{
    public string TipoId { get; init; }
    public decimal Valor { get; init; }
    public string Status { get; init; }
}
