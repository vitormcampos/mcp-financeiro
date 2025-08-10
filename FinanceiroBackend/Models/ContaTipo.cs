using System;
using System.Text.Json.Serialization;

namespace FinanceiroBackend.Models;

public class ContaTipo
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Conta> Contas { get; set; }
}
