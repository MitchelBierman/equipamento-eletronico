using FluentValidation;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Domain.Interfaces;
using EquipamentoEletronico.Infrastructure;

namespace EquipamentoEletronico.Application.Services
{
    public class EquipamentoService : IEquipamentoService
    {
        private readonly IValidator<Equipamento> _validator;
        private readonly EquipamentoEletronicoDbContext _contexto;

        public EquipamentoService(IValidator<Equipamento> validator, EquipamentoEletronicoDbContext contexto)
        {
            _validator = validator;
            _contexto = contexto;
        }

        public bool Validate(Equipamento equipamento, out List<string> errors)
        {
            var result = _validator.Validate(equipamento);
            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return false;
            }

            errors = new List<string>();
            return true;
        }

        public List<Equipamento> GetListaEquipamentos()
        {
            var lista = _contexto.Equipamentos.ToList();

            return lista;
        }
        public Equipamento? GetById(int id)
        {
            return _contexto.Equipamentos.FirstOrDefault(e => e.Id == id);
        }

        public string AdicionarEquipamento(Equipamento equipamento)
        {
            var nomeUnico = _contexto.Equipamentos.Where(x => x.Nome == equipamento.Nome && x.Id != equipamento.Id).ToList().Count == 0;

            if (!nomeUnico)
            {
                return "Já existe um equipamento com este nome.";
            }
            else
            {
                _contexto.Equipamentos.Add(equipamento);
                _contexto.SaveChanges();
                return string.Empty;
            }
        }

        public string EditarEquipamento(Equipamento equipamento)
        {
            var equipamentoExistente = _contexto.Equipamentos.Find(equipamento.Id);
            var nomeUnico = _contexto.Equipamentos.Where(x => x.Nome == equipamento.Nome && x.Id != equipamento.Id).ToList().Count == 0;

            if (!nomeUnico)
                return "Já existe um equipamento com este nome.";

            if (equipamentoExistente != null)
            {
                _contexto.Entry(equipamentoExistente).CurrentValues.SetValues(equipamento);
                _contexto.SaveChanges();
                return string.Empty;
            }

            return "Equipamento não encontrado.";
        }

        public void ExcluirEquipamento(int id)
        {
            var equipamentoExistente = _contexto.Equipamentos.Find(id);
            if (equipamentoExistente != null)
            {
                _contexto.Equipamentos.Remove(equipamentoExistente);
                _contexto.SaveChanges();
            }
        }

    }
}