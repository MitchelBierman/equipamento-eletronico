using EquipamentoEletronico.Domain.Entities;

namespace EquipamentoEletronico.Application.DTOs
{
    public class EquipamentoDTO : BaseEntity
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int QtdEmEstoque { get; set; }
        public bool TemEstoque => QtdEmEstoque > 0;

        public Equipamento ToEntity()
        {
            return new Equipamento
            {
                Id = this.Id,
                Nome = this.Nome,
                Tipo = this.Tipo,
                QtdEmEstoque = this.QtdEmEstoque,
                DataInclusao = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond)

            };
        }
    }
}
