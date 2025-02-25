using EquipamentoEletronico.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace EquipamentoEletronico.API.Models
{
    public class EquipamentoModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Tipo { get; set; }
        public int QtdEmEstoque { get; set; }
        public DateTime DataInclusao { get; set; }

        public Equipamento ToEntity()
        {
            return new Equipamento
            {
                Nome = this.Nome,
                Tipo = this.Tipo,
                QtdEmEstoque = this.QtdEmEstoque,
                DataInclusao = DateTime.Now
            };
        }
    }
}
