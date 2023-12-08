using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VeiculoController : ControllerBase
{
    private readonly IBaseRepository<Veiculo> _veiculoRepository;

    public VeiculoController(IBaseRepository<Veiculo> veiculoRepository)
    {
        _veiculoRepository = veiculoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVeiculosAsync()
    {
        try
        {
            var veiculo = await _veiculoRepository.GetAllAsync();
            return Ok(veiculo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar veículos: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVeiculoByIdAsync(int id)
    {
        try
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);
            if (veiculo != null)
            {
                return Ok(veiculo);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar veículo: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateVeiculoAsync([FromBody] Veiculo veiculo)
    {
        try
        {
            await _veiculoRepository.InsertAsync(veiculo);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar veículo: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVeiculoAsync([FromBody] Veiculo veiculo)
    {
        try
        {
            var veiculoExiste = await _veiculoRepository.GetByIdAsync(veiculo.Id);
            if (veiculoExiste != null)
            {
                await _veiculoRepository.UpdateAsync(veiculo);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar veículo: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVeiculoAsync(int id)
    {
        try
        {
            var motoristaExiste = await _veiculoRepository.GetByIdAsync(id);
            if (motoristaExiste != null)
            {
                await _veiculoRepository.DeleteAsync(motoristaExiste.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar veículo: {ex.Message}");
        }
    }
}

