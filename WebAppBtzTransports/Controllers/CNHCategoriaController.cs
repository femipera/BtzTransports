using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;
using WebAppBtzTransports.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAppBtzTransports.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CNHCategoriaController : ControllerBase
{
    private readonly IBaseRepository<CNHCategoria> _cnhCategoriaRepository;

    public CNHCategoriaController(IBaseRepository<CNHCategoria> cnhCategoriaRepository)
    {
        _cnhCategoriaRepository = cnhCategoriaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCNHCategoriasAsync()
    {
        try
        {
            var cnhCategoria = await _cnhCategoriaRepository.GetAllAsync();
            return Ok(cnhCategoria);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar categorias para CNH: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoriaByIAsync(int id)
    {
        try
        {
            var cnhCategoria = await _cnhCategoriaRepository.GetByIdAsync(id);
            if (cnhCategoria != null)
            {
                return Ok(cnhCategoria);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar categoria para CNH: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCNHCategoriasAsync([FromBody] CNHCategoria cnhCategoria)
    {
        try
        {
            await _cnhCategoriaRepository.InsertAsync(cnhCategoria);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar categorias para CNH: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCNHCategoriasAsync([FromBody] CNHCategoria cnhCategoria)
    {
        try
        {
            var cnhCategoriaExiste = await _cnhCategoriaRepository.GetByIdAsync(cnhCategoria.Id);
            if (cnhCategoriaExiste != null)
            {
                await _cnhCategoriaRepository.UpdateAsync(cnhCategoria);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar categoria para CNH: { ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCNHCategoriaAsync(int id)
    {
        try
        {
            var cnhCategoria = await _cnhCategoriaRepository.GetByIdAsync(id);
            if (cnhCategoria != null)
            {
                await _cnhCategoriaRepository.DeleteAsync(cnhCategoria.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar categoria para CNH: {ex.Message}");
        }
    }
}