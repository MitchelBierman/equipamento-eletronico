using Moq;
using FluentValidation;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Domain.Interfaces;
using EquipamentoEletronico.Application.Services;

public class EquipamentoServiceTests
{
    private readonly Mock<IEquipamentoRepository> _repositoryMock;
    private readonly IEquipamentoService _equipamentoService;
    private readonly IValidator<Equipamento> _validator;
    private readonly DateTime _dtNowSemMilissegundos = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond); //Evita falha de validação de data futura

    public EquipamentoServiceTests()
    {
        _repositoryMock = new Mock<IEquipamentoRepository>();

        _repositoryMock.Setup(repo => repo.AdicionarEquipamento(It.IsAny<Equipamento>()))
            .Verifiable();

        _repositoryMock.Setup(repo => repo.ExisteEquipamento(It.IsAny<string>()))
            .Returns(false);

        _validator = new EquipamentoEletronicoValidator(_repositoryMock.Object);

        _equipamentoService = new EquipamentoService(_validator, _repositoryMock.Object);
    }

    [Fact]
    public void DeveValidarEquipamento_QuandoDadosSaoValidos()
    {
        var equipamento = new Equipamento("Notebook", "Eletrônico", 10, _dtNowSemMilissegundos);

        var resultado = _equipamentoService.Validate(equipamento, out var erros);

        Assert.True(resultado);
        Assert.Empty(erros);
    }

    [Fact]
    public void DeveAdicionarEquipamentoAoBanco()
    {
        var equipamento = new Equipamento("Monitor", "Periférico", 5, _dtNowSemMilissegundos);

        _equipamentoService.AdicionarEquipamento(equipamento);

        _repositoryMock.Verify(repo => repo.AdicionarEquipamento(It.IsAny<Equipamento>()), Times.Once);
    }

    [Fact]
    public void DeveRetornarEquipamentoPorId_QuandoIdExiste()
    {
        var equipamento = new Equipamento("Teclado", "Periférico", 15, _dtNowSemMilissegundos);

        _repositoryMock.Setup(repo => repo.GetById(equipamento.Id)).Returns(equipamento);

        var equipamentoEncontrado = _equipamentoService.GetById(equipamento.Id);

        Assert.NotNull(equipamentoEncontrado);
        Assert.Equal("Teclado", equipamentoEncontrado.Nome);
    }

    [Fact]
    public void DeveRetornarTodosOsEquipamentos()
    {
        var equipamentos = new List<Equipamento>
    {
        new Equipamento("Mouse", "Periférico", 20, _dtNowSemMilissegundos),
        new Equipamento("Impressora", "Eletrônico", 3, _dtNowSemMilissegundos)
    };

        _repositoryMock.Setup(repo => repo.GetListaEquipamentos()).Returns(equipamentos);

        var equipamentosCadastrados = _equipamentoService.GetListaEquipamentos();

        Assert.Equal(2, equipamentosCadastrados.Count);
    }

    [Fact]
    public void DeveEditarEquipamento()
    {
        var equipamento = new Equipamento("SSD", "Armazenamento", 10, _dtNowSemMilissegundos);

        _repositoryMock.Setup(repo => repo.GetById(equipamento.Id)).Returns(equipamento);
        _repositoryMock.Setup(repo => repo.EditarEquipamento(It.IsAny<Equipamento>())).Verifiable();

        equipamento.Nome = "HDD";
        equipamento.QtdEmEstoque = 20;

        _equipamentoService.EditarEquipamento(equipamento);

        Assert.Equal("HDD", equipamento.Nome);
        Assert.Equal(20, equipamento.QtdEmEstoque);
    }

    [Fact]
    public void DeveExcluirEquipamento()
    {
        var equipamento = new Equipamento("Gabinete", "Computador", 2, _dtNowSemMilissegundos);

        _repositoryMock.Setup(repo => repo.GetById(equipamento.Id)).Returns(equipamento);
        _repositoryMock.Setup(repo => repo.ExcluirEquipamento(equipamento.Id)).Verifiable();

        _equipamentoService.ExcluirEquipamento(equipamento.Id);

        _repositoryMock.Verify(repo => repo.ExcluirEquipamento(equipamento.Id), Times.Once);
    }

    [Fact]
    public void DeveRetornarFalso_QuandoNaoTemEstoque()
    {
        var equipamento = new Equipamento("Tela Mtek", "Monitor", 0, _dtNowSemMilissegundos);

        Assert.False(equipamento.TemEstoque);
    }

    [Fact]
    public void NaoDeveAdicionarEquipamentoComNomeVazio()
    {
        var equipamento = new Equipamento("", "Eletrônico", 5, _dtNowSemMilissegundos);

        var resultado = _equipamentoService.Validate(equipamento, out var erros);

        Assert.False(resultado);
        Assert.Contains(erros, erro => erro.Contains("O Nome é obrigatório."));
    }
}