using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IBaseRepository<Usuario> _usuarioRepository;

    public UsuarioController(IBaseRepository<Usuario> usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsuariosAsync()
    {
        try
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar usuários: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioByIAsync(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar usuário: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUsuarioAsync([FromBody] Usuario usuario)
    {
        try
        {
            await _usuarioRepository.InsertAsync(usuario);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUsuarioAsync([FromBody] Usuario usuario)
    {
        try
        {
            var usuarioExiste = await _usuarioRepository.GetByIdAsync(usuario.Id);
            if (usuarioExiste != null)
            {
                await _usuarioRepository.UpdateAsync(usuario);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao alterar usuário: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuarioAsync(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario != null)
            {
                await _usuarioRepository.DeleteAsync(usuario.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar usuário: {ex.Message}");
        }
    }
}