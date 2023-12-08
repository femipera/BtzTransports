namespace WebAppBtzTransports.Models
{
    public class Veiculo : BaseIdNome
    {
        public string? Placa { get; set; }
        public DateTime AnoFabricacao { get; set; }
        public int CapacidadeMaximaTanque { get; set; }
        public string? Observacao { get; set; }
        public int FabricanteId { get; set; }
        public int CombustivelId { get; set; }

        public Fabricante? Fabricante { get; set; }
        public Combustivel? Combustivel { get; set; }
    }
}
