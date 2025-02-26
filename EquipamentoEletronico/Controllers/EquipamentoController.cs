using EquipamentoEletronico.Application.DTOs;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EquipamentoEletronico.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly IEquipamentoService _equipamentoService;
        private readonly IValidator<Equipamento> _equipamentoValidator;

        public EquipamentoController(IEquipamentoService equipamentoService, IValidator<Equipamento> equipamentoValidator)
        {
            _equipamentoService = equipamentoService;
            _equipamentoValidator = equipamentoValidator;
        }

        public IActionResult Index()
        {
            var lista = _equipamentoService.GetListaEquipamentos()
                .Select(e => new EquipamentoDTO
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Tipo = e.Tipo,
                    QtdEmEstoque = e.QtdEmEstoque,
                }).ToList();

            return View(lista);
        }

        public IActionResult Detalhes(int id)
        {
            var equipamento = _equipamentoService.GetById(id);
            if (equipamento == null)
            {
                return NotFound();
            }

            var viewModel = new EquipamentoDTO
            {
                Id = equipamento.Id,
                Nome = equipamento.Nome,
                Tipo = equipamento.Tipo,
                QtdEmEstoque = equipamento.QtdEmEstoque
            };

            return View(viewModel);
        }

        public IActionResult Criar()
        {
            return View(new EquipamentoDTO
            {
                Nome = string.Empty,
                Tipo = string.Empty
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(EquipamentoDTO viewModel)
        {
            ModelState.Clear();

            var equipamento = new Equipamento
            {
                Nome = viewModel.Nome,
                Tipo = viewModel.Tipo,
                QtdEmEstoque = viewModel.QtdEmEstoque,
                DataInclusao = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond)
            };

            var validationResult = _equipamentoValidator.Validate(equipamento);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(viewModel);
            }

            var resultado = _equipamentoService.AdicionarEquipamento(equipamento);

            if (!string.IsNullOrEmpty(resultado))
            {
                ModelState.AddModelError(nameof(viewModel.Nome), resultado);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            var equipamento = _equipamentoService.GetById(id);
            if (equipamento == null)
            {
                return NotFound();
            }

            var viewModel = new EquipamentoDTO
            {
                Id = equipamento.Id,
                Nome = equipamento.Nome,
                Tipo = equipamento.Tipo,
                QtdEmEstoque = equipamento.QtdEmEstoque
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(EquipamentoDTO viewModel)
        {
            ModelState.Clear();

            var equipamento = viewModel.ToEntity();

            var validationResult = _equipamentoValidator.Validate(equipamento);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(viewModel);
            }

            var resultado = _equipamentoService.EditarEquipamento(equipamento);

            if (!string.IsNullOrEmpty(resultado))
            {
                ModelState.AddModelError(nameof(viewModel.Nome), resultado);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            _equipamentoService.ExcluirEquipamento(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
