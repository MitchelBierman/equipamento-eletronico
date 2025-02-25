using EquipamentoEletronico.Domain.Entities;

namespace EquipamentoEletronico.Domain.Interfaces
{    public interface IEquipamentoService
    {
        Equipamento? GetById(int id);
        bool Validate(Equipamento equipamento, out List<string> errors);
        List<Equipamento> GetListaEquipamentos();
        void AdicionarEquipamento(Equipamento equipamento);
        void EditarEquipamento(Equipamento equipamento);
        void ExcluirEquipamento(int id);
    }
}
