using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;
using WebAppBtzTransports.Repositories;

namespace WebAppBtzTransports.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AbastecimentoController : ControllerBase
{
    private readonly IBaseRepository<Abastecimento> _abastecimentoRepository;
    private readonly IValidator<Abastecimento> _abastecimentoValidator;

    public AbastecimentoController(IBaseRepository<Abastecimento> abastecimentoRepository, IValidator<Abastecimento> abastecimentoValidator)
    {
        _abastecimentoRepository = abastecimentoRepository;
        _abastecimentoValidator = abastecimentoValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAbastecimentosAsync()
    {
        try
        {
            var abastecimento = await _abastecimentoRepository.GetAllAsync();
            return Ok(abastecimento);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar abastecimentos: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAbastecimentoByIdAsync(int id)
    {
        try
        {
            var abastecimento = await _abastecimentoRepository.GetByIdAsync(id);
            if (abastecimento != null)
            {
                return Ok(abastecimento);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar abastecimento: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAbastecimentoAsync([FromBody] Abastecimento abastecimento)
    {
        var validationResult = await _abastecimentoValidator.ValidateAsync(abastecimento);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            await _abastecimentoRepository.InsertAsync(abastecimento);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar abastecimento: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAbastecimentoAsync([FromBody] Abastecimento abastecimento)
    {
        try
        {
            var abastecimentoExiste = await _abastecimentoRepository.GetByIdAsync(abastecimento.Id);
            if (abastecimentoExiste != null)
            {
                await _abastecimentoRepository.UpdateAsync(abastecimento);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar abastecimento: {ex.Message}");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAbastecimentoAsync(int id)
    {
        try
        {
            var abastecimentoExiste = await _abastecimentoRepository.GetByIdAsync(id);
            if (abastecimentoExiste != null)
            {
                await _abastecimentoRepository.DeleteAsync(abastecimentoExiste.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar abastecimento: {ex.Message}");
        }
    }
}



