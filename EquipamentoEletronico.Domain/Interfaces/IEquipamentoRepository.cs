using EquipamentoEletronico.Domain.Entities;

public interface IEquipamentoRepository
{
    bool NomeJaExiste(string nome);
    List<Equipamento> GetListaEquipamentos();
    bool ExisteEquipamento(string nome);
    Equipamento? GetById(int id);
    bool NomeUnico(string nome, int id);
    string AdicionarEquipamento(Equipamento equipamento);
    string EditarEquipamento(Equipamento equipamento);
    string ExcluirEquipamento(int id);
}
