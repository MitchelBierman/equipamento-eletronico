namespace EquipamentoEletronico.Application.DTOs
{
    public class EquipamentoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int QtdEmEstoque { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
