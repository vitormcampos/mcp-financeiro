using System;

namespace FinanceiroBackend.Models;

public class Conta
{
    public string Id { get; set; }
    public ContaTipo Tipo { get; set; }
    public string TipoId { get; set; }
    public decimal Valor { get; set; }
    public string Status { get; set; }
    public DateTime DataCriacao { get; set; }
}
