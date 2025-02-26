using FluentValidation;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Domain.Interfaces;

namespace EquipamentoEletronico.Application.Services
{
    public class EquipamentoService : IEquipamentoService
    {
        private readonly IValidator<Equipamento> _validator;
        private readonly IEquipamentoRepository _contexto;

        public EquipamentoService(IValidator<Equipamento> validator, IEquipamentoRepository contexto)
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
            var lista = _contexto.GetListaEquipamentos();

            return lista;
        }
        public Equipamento? GetById(int id)
        {
            return _contexto.GetById(id);
        }

        public string AdicionarEquipamento(Equipamento equipamento)
        {
            var nomeUnico = !_contexto.ExisteEquipamento(equipamento.Nome);

            if (!nomeUnico)
                return "Já existe um equipamento com este nome.";

            _contexto.AdicionarEquipamento(equipamento);
            return string.Empty;
        }

        public string EditarEquipamento(Equipamento equipamento)
        {
            var equipamentoExistente = _contexto.GetById(equipamento.Id);
            var nomeUnico = _contexto.NomeUnico(equipamento.Nome, equipamento.Id);

            if (!nomeUnico)
                return "Já existe um equipamento com este nome.";

            if (equipamentoExistente != null)
            {
                _contexto.EditarEquipamento(equipamento);
                return string.Empty;
            }

            return "Equipamento não encontrado.";
        }

        public void ExcluirEquipamento(int id)
        {
            var equipamentoExistente = _contexto.GetById(id);
            if (equipamentoExistente != null)
            {
                _contexto.ExcluirEquipamento(id);
            }
        }
    }
}