namespace WebAppBtzTransports.Models;

public class Motorista : BaseIdNome
{
    public string? CPF { get; set; }
    public string? NumeroCNH { get; set; }
    public DateTime DataNascimento { get; set; }
    public bool Status { get; set; }
    public int CNHCategoriaId { get; set; }
    public CNHCategoria? CNHCategoria { get; set; }
}
