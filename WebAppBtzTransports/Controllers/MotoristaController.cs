using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MotoristaController : ControllerBase
{
    private readonly IBaseRepository<Motorista> _motoristaRepository;

    public MotoristaController(IBaseRepository<Motorista> motoristaRepository)
    {
        _motoristaRepository = motoristaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMotoristasAsync()
    {
        try
        {
            var motoristas = await _motoristaRepository.GetAllAsync();
            return Ok(motoristas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar motoristas: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMotoristaByIdAsync(int id)
    {
        try
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id);
            if (motorista != null)
            {
                return Ok(motorista);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar motorista: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateMotoristaAsync([FromBody] Motorista motorista)
    {
        try
        {
            await _motoristaRepository.InsertAsync(motorista);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar motorista: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMotoristaAsync([FromBody] Motorista motorista)
    {
        try
        {
            var motoristaExiste = await _motoristaRepository.GetByIdAsync(motorista.Id);
            if (motoristaExiste != null)
            {
                await _motoristaRepository.UpdateAsync(motorista);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar motorista: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMotoristaAsync(int id)
    {
        try
        {
            var motoristaExiste = await _motoristaRepository.GetByIdAsync(id);
            if (motoristaExiste != null)
            {
                await _motoristaRepository.DeleteAsync(id);
                return Ok();
            }
            else 
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar motorista: {ex.Message}");
        }
    }
}
