using Dapper;
using System.Data;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Repositories;

public class CombustivelRepository : IBaseRepository<Combustivel>
{
    private IDbConnection _dbConexao;
    public CombustivelRepository(IDbConnection dbConexao)
    {
        _dbConexao = dbConexao;
    }
    public async Task DeleteAsync(int id)
    {
        await _dbConexao.ExecuteAsync("DELETE FROM Combustiveis WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Combustivel>> GetAllAsync()
    {
        return await _dbConexao.QueryAsync<Combustivel>("SELECT * FROM Combustiveis");
    }

    public async Task<Combustivel?> GetByIdAsync(int? id)
    {
        return await _dbConexao.QuerySingleOrDefaultAsync<Combustivel>("SELECT * FROM Combustiveis WHERE Id = @Id", new { Id = id });
    }

    public async Task InsertAsync(Combustivel combustivel)
    {
        var sql = @"INSERT INTO Combustiveis (Nome, Preco) 
                    VALUES (@Nome, @Preco); 
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        combustivel.Id = await _dbConexao.QuerySingleOrDefaultAsync<int>(sql, combustivel);
    }

    public async Task UpdateAsync(Combustivel combustivel)
    {
        var sql = @"UPDATE Combustiveis SET Nome = @Nome, Preco = @Preco WHERE Id = @Id";
        await _dbConexao.ExecuteAsync(sql, combustivel);
    }
}
