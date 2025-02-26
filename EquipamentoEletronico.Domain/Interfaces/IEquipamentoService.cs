using EquipamentoEletronico.Domain.Entities;

namespace EquipamentoEletronico.Domain.Interfaces
{    public interface IEquipamentoService
    {
        Equipamento? GetById(int id);
        bool Validate(Equipamento equipamento, out List<string> errors);
        List<Equipamento> GetListaEquipamentos();
        string AdicionarEquipamento(Equipamento equipamento);
        string EditarEquipamento(Equipamento equipamento);
        void ExcluirEquipamento(int id);
    }
}
