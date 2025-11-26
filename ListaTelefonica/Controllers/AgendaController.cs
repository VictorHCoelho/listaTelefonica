using ListaTelefonica.Models;
using ListaTelefonica.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ListaTelefonica.Controllers;

public class AgendaController : ControllerBase
{
    private readonly IContatoRepository _contatoRepository;

    public AgendaController(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    [HttpGet("api/contatos")]
    public IActionResult GetAllContatos()
    {
        var contatos = _contatoRepository.GetAll();

        if (!contatos.Any())
        {
            return NotFound("Nenhum contato encontrado.");
        }   

        return Ok(contatos);
    }

    [HttpGet("api/contatos/{id}")]
    public IActionResult GetContatoById(int id)
    {
        var contato = _contatoRepository.GetById(id);
        if (contato == null)
        {
            return NotFound("Contato não encontrado");
        }
        return Ok(contato);
    }

    [HttpGet("api/contatos/pesquisar")]
    public IActionResult GetContatoByName([FromQuery] string nome)
    {
        var contato = _contatoRepository.GetByName(nome);
        if (contato == null)
        {
            return NotFound("Contato não encontrado");
        }
        return Ok(contato);
    }

    [HttpPost("api/contatos")]
    public IActionResult CreateContato([FromBody] Contato contato)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = _contatoRepository.GetByName(contato.Nome);

        if (existing != null)
        {
            return Conflict($"Contato com o nome {contato.Nome} já existe.");
        }

        var createdContato = _contatoRepository.Create(contato);

        return CreatedAtAction(nameof(GetContatoById), new { id = createdContato.Id }, createdContato);
    }

    [HttpPut("api/contatos/{id}")]
    public IActionResult UpdateContato([FromBody] Contato contato, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != contato.Id)
            return BadRequest("O ID deve ser o mesmo");

        var existingById = _contatoRepository.GetById(id);

        if (existingById == null)
        {
            return NotFound($"Contato com o ID {id} não existe.");
        }

        var existingByName = _contatoRepository.GetByName(contato.Nome);

        if (contato.Nome == existingByName?.Nome)
        {
            return Conflict($"Contato com o nome {contato.Nome} já existe.");
        }

        _contatoRepository.Update(existingById, contato);

        return Ok(existingById);
    }

    [HttpDelete("api/contatos/{id}")]
    public IActionResult DeleteContato(int id)
    { 
        var existing = _contatoRepository.GetById(id);

        if (existing == null)
        {
            return NotFound($"Contato com o ID {id} não existe.");
        }

        _contatoRepository.Delete(id);

        return NoContent();
    }
}
