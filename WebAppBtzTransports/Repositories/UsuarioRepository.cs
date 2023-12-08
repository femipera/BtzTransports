using Dapper;
using System.Data;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Repositories;

public class UsuarioRepository : IBaseRepository<Usuario>
{
    private IDbConnection _dbConexao;
    public UsuarioRepository(IDbConnection dbConexao) 
    {
        _dbConexao = dbConexao;
    }
    public async Task DeleteAsync(int id)
    {
        await _dbConexao.ExecuteAsync("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _dbConexao.QueryAsync<Usuario>("SELECT * FROM Usuarios");
    }

    public async Task<Usuario?> GetByIdAsync(int? id)
    {
        return await _dbConexao.QuerySingleOrDefaultAsync<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });
    }

    public async Task InsertAsync(Usuario usuario)
    {
        var sql = @"INSERT INTO Usuarios (Nome, Senha) VALUES (@Nome, @Senha); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        usuario.Id = await _dbConexao.QuerySingleOrDefaultAsync<int>(sql, usuario);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        var sql = @"UPDATE Usuarios SET Senha = @Senha WHERE Id = @Id";
        await _dbConexao.ExecuteAsync(sql, usuario);
    }
}