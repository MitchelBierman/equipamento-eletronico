using EquipamentoEletronico.Application.Services;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;

public class EquipamentoServiceTests
{
    private readonly Mock<IValidator<Equipamento>> _validatorMock;
    private readonly EquipamentoEletronicoDbContext _dbContext;
    private readonly EquipamentoService _equipamentoService;

    public EquipamentoServiceTests()
    {
        _validatorMock = new Mock<IValidator<Equipamento>>();

        // Configuração do banco de dados em memória para testes
        var options = new DbContextOptionsBuilder<EquipamentoEletronicoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new EquipamentoEletronicoDbContext(options);
        _equipamentoService = new EquipamentoService(_validatorMock.Object, _dbContext);
    }

    [Fact]
    public void DeveValidarEquipamento_QuandoDadosSaoValidos()
    {
        // Arrange
        var equipamento = new Equipamento("Notebook", "Eletrônico", 10, DateTime.Now);
        _validatorMock.Setup(v => v.Validate(It.IsAny<Equipamento>())).Returns(new ValidationResult());

        // Act
        var resultado = _equipamentoService.Validate(equipamento, out var erros);

        // Assert
        Assert.True(resultado);
        Assert.Empty(erros);
    }

    [Fact]
    public void DeveFalharValidacao_QuandoDadosSaoInvalidos()
    {
        // Arrange
        var equipamento = new Equipamento("", "", -1, DateTime.Now);
        var errosValidacao = new List<ValidationFailure>
        {
            new ValidationFailure("Nome", "Nome é obrigatório"),
            new ValidationFailure("Tipo", "Tipo é obrigatório"),
            new ValidationFailure("QtdEmEstoque", "Quantidade não pode ser negativa")
        };

        _validatorMock.Setup(v => v.Validate(It.IsAny<Equipamento>()))
                      .Returns(new ValidationResult(errosValidacao));

        // Act
        var resultado = _equipamentoService.Validate(equipamento, out var erros);

        // Assert
        Assert.False(resultado);
        Assert.Equal(3, erros.Count);
    }

    [Fact]
    public void DeveAdicionarEquipamentoAoBanco()
    {
        // Arrange
        var equipamento = new Equipamento("Monitor", "Periférico", 5, DateTime.Now);

        // Act
        _equipamentoService.AdicionarEquipamento(equipamento);
        var equipamentoEncontrado = _dbContext.Equipamentos.FirstOrDefault(e => e.Nome == "Monitor");

        // Assert
        Assert.NotNull(equipamentoEncontrado);
        Assert.Equal("Monitor", equipamentoEncontrado.Nome);
    }

    [Fact]
    public void DeveRetornarEquipamentoPorId_QuandoIdExiste()
    {
        // Arrange
        var equipamento = new Equipamento("Teclado", "Periférico", 15, DateTime.Now);
        _dbContext.Equipamentos.Add(equipamento);
        _dbContext.SaveChanges();

        // Act
        var equipamentoEncontrado = _equipamentoService.GetById(equipamento.Id);

        // Assert
        Assert.NotNull(equipamentoEncontrado);
        Assert.Equal("Teclado", equipamentoEncontrado.Nome);
    }

    [Fact]
    public void DeveRetornarTodosOsEquipamentos()
    {
        // Arrange
        _dbContext.Equipamentos.AddRange(
            new Equipamento("Mouse", "Periférico", 20, DateTime.Now),
            new Equipamento("Impressora", "Eletrônico", 3, DateTime.Now)
        );
        _dbContext.SaveChanges();

        // Act
        var equipamentosCadastrados = _equipamentoService.GetListaEquipamentos();

        // Assert
        Assert.Equal(2, equipamentosCadastrados.Count);
    }

    [Fact]
    public void DeveEditarEquipamento()
    {
        // Arrange
        var equipamento = new Equipamento("SSD", "Armazenamento", 10, DateTime.Now);
        _dbContext.Equipamentos.Add(equipamento);
        _dbContext.SaveChanges();

        // Alterando propriedades
        equipamento.Nome = "HDD";
        equipamento.QtdEmEstoque = 20;

        // Act
        _equipamentoService.EditarEquipamento(equipamento);
        var equipamentoAtualizado = _dbContext.Equipamentos.Find(equipamento.Id);

        // Assert
        Assert.NotNull(equipamentoAtualizado);
        Assert.Equal("HDD", equipamentoAtualizado.Nome);
        Assert.Equal(20, equipamentoAtualizado.QtdEmEstoque);
    }

    [Fact]
    public void DeveExcluirEquipamento()
    {
        // Arrange
        var equipamento = new Equipamento("Tablet", "Eletrônico", 8, DateTime.Now);
        _dbContext.Equipamentos.Add(equipamento);
        _dbContext.SaveChanges();

        // Act
        _equipamentoService.ExcluirEquipamento(equipamento.Id);
        var equipamentoRemovido = _dbContext.Equipamentos.Find(equipamento.Id);

        // Assert
        Assert.Null(equipamentoRemovido);
    }
}