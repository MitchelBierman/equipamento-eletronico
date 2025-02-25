﻿namespace EquipamentoEletronico.Domain.Entities
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int QtdEmEstoque { get; set; }
        public DateTime DataInclusao { get; set; }
        public bool TemEstoque => QtdEmEstoque > 0;

        public Equipamento() { }

        public Equipamento(string nome, string tipo, int qtdEmEstoque, DateTime? dataInclusao)
        {
            Nome = nome;
            Tipo = tipo;
            QtdEmEstoque = qtdEmEstoque;
            DataInclusao = dataInclusao ?? DateTime.Now;
        }
    }
}
