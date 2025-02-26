using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Infrastructure;

public class EquipamentoRepository : IEquipamentoRepository
{
    private readonly EquipamentoEletronicoDbContext _contexto;

    public EquipamentoRepository(EquipamentoEletronicoDbContext contexto)
    {
        _contexto = contexto;
    }

    public Equipamento GetById(int id)
    {
        var equipamento = _contexto.Equipamentos.Find(id);
        if (equipamento == null)
            return new Equipamento();

        return equipamento;
    }

    public List<Equipamento> GetListaEquipamentos()
    {
        return _contexto.Equipamentos.ToList();
    }

    public bool NomeJaExiste(string nome)
    {
        return _contexto.Equipamentos.Any(e => e.Nome == nome);
    }

    public string AdicionarEquipamento(Equipamento equipamento)
    {
        _contexto.Equipamentos.Add(equipamento);
        _contexto.SaveChanges();
        return string.Empty;
    }

    public string EditarEquipamento(Equipamento equipamento)
    {
        var equipamentoExistente = GetById(equipamento.Id);

        if (equipamentoExistente == null)
            return "Equipamento não encontrado.";

        if (!NomeUnico(equipamento.Nome, equipamento.Id))
            return "Já existe um equipamento com este nome.";

        _contexto.Entry(equipamentoExistente).CurrentValues.SetValues(equipamento);
        _contexto.SaveChanges();

        return string.Empty;
    }

    public string ExcluirEquipamento(int id)
    {
        var equipamento = GetById(id);
        if (equipamento == null)
            return "Equipamento não encontrado.";

        _contexto.Equipamentos.Remove(equipamento);
        _contexto.SaveChanges();

        return string.Empty;
    }

    public bool NomeUnico (string nome, int id)
    {
        return !_contexto.Equipamentos.Any(e => e.Nome == nome && e.Id != id);
    }

    public bool ExisteEquipamento(string nome)
    {
        return _contexto.Equipamentos.Any(e => e.Nome == nome);
    }
}
