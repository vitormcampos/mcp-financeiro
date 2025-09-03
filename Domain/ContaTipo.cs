using System.Text.Json.Serialization;

namespace Domain;

public class ContaTipo
{
    public string Id { get; set; }
    public string Nome { get; set; }

    [JsonIgnore]
    public ICollection<Conta> Contas { get; set; }
}
