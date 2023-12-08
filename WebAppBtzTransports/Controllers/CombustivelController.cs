using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CombustivelController : ControllerBase
{
    private readonly IBaseRepository<Combustivel> _combustivelRepository;

    public CombustivelController(IBaseRepository<Combustivel> combustivelRepository)
    {
        _combustivelRepository = combustivelRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCombustiveisAsync()
    {
        try
        {
            var combustivel = await _combustivelRepository.GetAllAsync();
            return Ok(combustivel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar combustiveis: {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCombustivelByIdAsync(int id)
    {
        try
        {
            var combustivel = await _combustivelRepository.GetByIdAsync(id);
            if (combustivel != null)
            {
                return Ok(combustivel);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar combustivel: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCombustivelAsync([FromBody] Combustivel combustivel)
    {
        try
        {
            await _combustivelRepository.InsertAsync(combustivel);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar combustivel: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCombustivelAsync([FromBody] Combustivel combustivel)
    {
        try
        {
            var combustivelExiste = await _combustivelRepository.GetByIdAsync(combustivel.Id);
            if (combustivelExiste != null)
            {
                await _combustivelRepository.UpdateAsync(combustivel);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar combustivel: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCombustivelAsync(int id)
    {
        try
        {
            var combustivel = await _combustivelRepository.GetByIdAsync(id);
            if (combustivel != null)
            {
                await _combustivelRepository.DeleteAsync(combustivel.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar combustivel: {ex.Message}");
        }
    }
}
