﻿using FluentValidation;
using EquipamentoEletronico.Domain.Entities;
using System.Text.RegularExpressions;

public class EquipamentoEletronicoValidator : AbstractValidator<Equipamento>
{
    private readonly IEquipamentoRepository _equipamentoRepository;

    public EquipamentoEletronicoValidator(IEquipamentoRepository equipamentoRepository)
    {
        _equipamentoRepository = equipamentoRepository;

        RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("O Nome é obrigatório.")
            .MaximumLength(100).WithMessage("O Nome não pode ter mais de 100 caracteres.")
            .MinimumLength(3).WithMessage("O Nome deve ter pelo menos 3 caracteres.")
            .Must(ContemNaoNumerico).WithMessage("O Nome não pode conter apenas números.");

        RuleFor(e => e.Tipo)
            .NotEmpty().WithMessage("O Tipo é obrigatório.")
            .MinimumLength(3).WithMessage("O Tipo deve ter pelo menos 3 caracteres.")
            .MaximumLength(50).WithMessage("O Tipo não pode ter mais de 50 caracteres.")
            .Must(ContemApenasLetras).WithMessage("O Tipo deve conter apenas letras.");

        RuleFor(e => e.QtdEmEstoque)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade deve ser maior ou igual a zero.")
            .LessThanOrEqualTo(10000).WithMessage("A quantidade não pode exceder 10.000 unidades.");

        RuleFor(e => e.DataInclusao)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A Data de Inclusão não pode ser no futuro.");
    }

    private bool ContemNaoNumerico(string value)
    {
        if (value == null)
            return false;
        else
            return Regex.IsMatch(value, @"\D");
    }

    private bool ContemApenasLetras(string value)
    {
        if (value == null)
            return false;
        else
            return Regex.IsMatch(value, @"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$");
    }
}
