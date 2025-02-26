using EquipamentoEletronico.Application.DTOs;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EquipamentoEletronico.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/equipamento")]
    public class EquipamentoApiController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;
        private readonly IValidator<Equipamento> _equipamentoValidator;

        public EquipamentoApiController(IEquipamentoService equipamentoService, IValidator<Equipamento> equipamentoValidator)
        {
            _equipamentoService = equipamentoService;
            _equipamentoValidator = equipamentoValidator;
        }

        [HttpGet]
        public IActionResult GetListaEquipamentos()
        {
            var lista = _equipamentoService.GetListaEquipamentos()
                .Select(e => new EquipamentoDTO
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Tipo = e.Tipo,
                    QtdEmEstoque = e.QtdEmEstoque,
                }).ToList();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetEquipamentoById(int id)
        {
            var equipamento = _equipamentoService.GetById(id);
            if (equipamento == null)
                return NotFound();

            return Ok(equipamento);
        }

        [HttpPost]
        public IActionResult AdicionarEquipamento([FromBody] EquipamentoDTO equipamentoDTO)
        {
            var equipamento = new Equipamento
            {
                Nome = equipamentoDTO.Nome,
                Tipo = equipamentoDTO.Tipo,
                QtdEmEstoque = equipamentoDTO.QtdEmEstoque,
                DataInclusao = DateTime.Now
            };

            var validationResult = _equipamentoValidator.Validate(equipamento);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var resultado = _equipamentoService.AdicionarEquipamento(equipamento);
            if (!string.IsNullOrEmpty(resultado))
                return BadRequest(resultado);

            return CreatedAtAction(nameof(GetEquipamentoById), new { id = equipamento.Id }, equipamento);
        }

        [HttpPut("{id}")]
        public IActionResult EditarEquipamento(int id, [FromBody] EquipamentoDTO equipamentoDTO)
        {
            var equipamento = _equipamentoService.GetById(id);
            if (equipamento == null)
                return NotFound();

            equipamento.Nome = equipamentoDTO.Nome;
            equipamento.Tipo = equipamentoDTO.Tipo;
            equipamento.QtdEmEstoque = equipamentoDTO.QtdEmEstoque;

            var validationResult = _equipamentoValidator.Validate(equipamento);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var resultado = _equipamentoService.EditarEquipamento(equipamento);
            if (!string.IsNullOrEmpty(resultado))
                return BadRequest(resultado);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirEquipamento(int id)
        {
            var equipamento = _equipamentoService.GetById(id);
            if (equipamento == null)
                return NotFound();

            _equipamentoService.ExcluirEquipamento(id);
            return NoContent();
        }
    }
}