using EquipamentoEletronico.Application.Services;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EquipamentoEletronicoAPI.Controllers
{
    [Route("api/estoque")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;
        private readonly IValidator<EquipamentoEletronicoValidator> _equipamentoValidator;

        public EstoqueController(IEquipamentoService equipamentoService, IValidator<EquipamentoEletronicoValidator> equipamentoValidator)
        {
            _equipamentoService = equipamentoService;
            _equipamentoValidator = equipamentoValidator;
        }

        // GET: api/equipamento
        [HttpGet]
        public ActionResult<Equipamento> GetEstoque()
        {
            var equipamentos = _equipamentoService.GetListaEquipamentos();
            foreach (var e in equipamentos)
            {
                Console.WriteLine(e);
            }

            return Ok(equipamentos);
        }

        // GET: api/equipamento/{id}
        [HttpGet("{id}")]
        public IActionResult GetEquipamentoById(int id)
        {
            if (id < 1) return BadRequest("ID inválido.");

            var equipamento = $"Equipamento {id}";
            return Ok(equipamento);
        }

        // POST: api/equipamento
        [HttpPost]
        public IActionResult CreateEquipamento([FromBody] string nome)
        {
            if (string.IsNullOrEmpty(nome)) return BadRequest("Nome do equipamento é obrigatório.");

            return CreatedAtAction(nameof(GetEquipamentoById), new { id = 1 }, nome);
        }
    }
}
