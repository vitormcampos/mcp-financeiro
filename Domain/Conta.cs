namespace Domain;

public class Conta
{
    public string? Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Mouth { get; set; }
    public int Year { get; set; }
    public DateTime CreatedAt { get; set; }
}
