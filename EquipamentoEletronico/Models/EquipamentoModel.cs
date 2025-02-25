using EquipamentoEletronico.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace EquipamentoEletronico.API.Models
{
    public class EquipamentoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        [Range(0, 10000, ErrorMessage = "A quantidade deve ser 0 e 10000.")]
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
