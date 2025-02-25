using EquipamentoEletronico.Infrastructure;

public class EquipamentoRepository : IEquipamentoRepository
{
    private readonly EquipamentoEletronicoDbContext _contexto;

    public EquipamentoRepository(EquipamentoEletronicoDbContext contexto)
    {
        _contexto = contexto;
    }

    public bool NomeJaExiste(string nome)
    {
        return _contexto.Equipamentos.Any(e => e.Nome == nome);
    }
}
