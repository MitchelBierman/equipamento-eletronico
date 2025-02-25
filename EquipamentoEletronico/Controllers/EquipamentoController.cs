using EquipamentoEletronico.API.Models;
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

        // GET: /Equipamento
        public IActionResult Index()
        {
            try
            {
                var lista = _equipamentoService.GetListaEquipamentos()
                    .Select(e => new EquipamentoModel
                    {
                        Id = e.Id,
                        Nome = e.Nome,
                        Tipo = e.Tipo,
                        QtdEmEstoque = e.QtdEmEstoque,
                    }).ToList();

                return View(lista);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View("Error");
            }
        }

        // GET: /Equipamento/Detalhes/{id}
        public IActionResult Detalhes(int id)
        {
            var equipamento = _equipamentoService.GetById(id);
            if (equipamento == null)
            {
                return NotFound();
            }

            var viewModel = new EquipamentoModel
            {
                Id = equipamento.Id,
                Nome = equipamento.Nome,
                Tipo = equipamento.Tipo,
                QtdEmEstoque = equipamento.QtdEmEstoque
            };

            return View(viewModel);
        }

        // GET: /Equipamento/Criar (renders form)
        public IActionResult Criar()
        {
            return View(new EquipamentoModel());
        }

        // POST: /Equipamento/Criar (handles form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(EquipamentoModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

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
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(viewModel);
            }

            _equipamentoService.AdicionarEquipamento(equipamento);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Equipamento/Editar/{id}
        public IActionResult Editar(int id)
        {
            var equipamento = _equipamentoService.GetById(id);
            if (equipamento == null)
            {
                return NotFound();
            }

            var viewModel = new EquipamentoModel
            {
                Id = equipamento.Id,
                Nome = equipamento.Nome,
                Tipo = equipamento.Tipo,
                QtdEmEstoque = equipamento.QtdEmEstoque
            };

            return View(viewModel);
        }

        // POST: /Equipamento/Editar
        [HttpPost]
        public IActionResult Editar(EquipamentoModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var equipamento = new Equipamento
            {
                Id = viewModel.Id,
                Nome = viewModel.Nome,
                Tipo = viewModel.Tipo,
                QtdEmEstoque = viewModel.QtdEmEstoque
            };

            _equipamentoService.EditarEquipamento(equipamento);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            try
            {
                _equipamentoService.ExcluirEquipamento(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View("Error");
            }
        }

    }
}
