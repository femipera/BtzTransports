namespace WebAppBtzTransports.Models
{
    public class Abastecimento
    {
        public int Id { get; set; }
        public int VeiculoId { get; set; }
        public int MotoristaId { get; set; }
        public int CombustivelId { get; set; }
        public DateTime Data { get; set; }
        public float QuantidadeAbastecida { get; set; }
        public decimal CombustivelPreco { get; set; }
        public Veiculo? Veiculo { get; set; }
        public Combustivel? Combustivel { get; set; }
        public Motorista? Motorista { get; set; }

    }
}
