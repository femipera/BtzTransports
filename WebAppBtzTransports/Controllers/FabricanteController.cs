using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FabricanteController : ControllerBase
{
    private readonly IBaseRepository<Fabricante> _fabricanteRepository;

    public FabricanteController(IBaseRepository<Fabricante> fabricanteRepository)
    {
        _fabricanteRepository = fabricanteRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllFabricantesAsync()
    {
        try
        {
            var fabricante = await _fabricanteRepository.GetAllAsync();
            return Ok(fabricante);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar fabricantes: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFabricanteByIdAsync(int id)
    {
        try
        {
            var fabricante = await _fabricanteRepository.GetByIdAsync(id);
            if (fabricante != null)
            {
                return Ok(fabricante);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar fabricante: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateFabricanteAsync([FromBody]Fabricante fabricante)
    {
        try
        {
            await _fabricanteRepository.InsertAsync(fabricante);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar fabricante: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFabricanteAsync([FromBody]Fabricante fabricante)
    {
        try
        {
            var fabricanteExiste = await _fabricanteRepository.GetByIdAsync(fabricante.Id);
            if (fabricanteExiste != null)
            {
                await _fabricanteRepository.UpdateAsync(fabricante);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar fabricante: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFabricanteAsync(int id)
    {
        try
        {
            var fabricanteExiste = await _fabricanteRepository.GetByIdAsync(id);
            if (fabricanteExiste != null)
            {
                await _fabricanteRepository.DeleteAsync(fabricanteExiste.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar fabricante: {ex.Message}");
        }
    }
}