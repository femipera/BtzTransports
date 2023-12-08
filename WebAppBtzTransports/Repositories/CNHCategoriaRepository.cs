using Dapper;
using System.Data;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Repositories;

public class CNHCategoriaRepository : IBaseRepository<CNHCategoria>
{
    private IDbConnection _dbConexao;
    public CNHCategoriaRepository(IDbConnection dbConexao)
    {
        _dbConexao = dbConexao;
    }
    public async Task DeleteAsync(int id)
    {
        await _dbConexao.ExecuteAsync("DELETE FROM CNHCategorias WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<CNHCategoria>> GetAllAsync()
    {
        return await _dbConexao.QueryAsync<CNHCategoria>("SELECT * FROM CNHCategorias");
    }

    public async Task<CNHCategoria?> GetByIdAsync(int? id)
    {
        return await _dbConexao.QuerySingleOrDefaultAsync<CNHCategoria>("SELECT * FROM CNHCategorias WHERE Id = @Id", new { Id = id });
    }

    public async Task InsertAsync(CNHCategoria cnhCategoria)
    {
        var sql = @"INSERT INTO CNHCategorias (Nome) VALUES (@Nome); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        cnhCategoria.Id = await _dbConexao.QuerySingleOrDefaultAsync<int>(sql, cnhCategoria);
    }

    public async Task UpdateAsync(CNHCategoria cnhCategoria)
    {
        var sql = @"UPDATE CNHCategorias SET Nome = @Nome WHERE Id = @Id";
        await _dbConexao.ExecuteAsync(sql, cnhCategoria);
    }
}
