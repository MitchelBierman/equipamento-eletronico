namespace EquipamentoEletronico.Application.Shared
{
    public class ResultadoOperacao
    {
        public bool Sucesso { get; set; }
        public List<string> Erros { get; set; } = new List<string>();

        public ResultadoOperacao(bool sucesso = true)
        {
            Sucesso = sucesso;
        }

        public void AdicionarErro(string erro)
        {
            Sucesso = false;
            Erros.Add(erro);
        }
    }
}
