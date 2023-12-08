using Dapper;
using System.Data;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Repositories;

public class FabricanteRepository : IBaseRepository<Fabricante>
{
    private IDbConnection _dbConexao;
    public FabricanteRepository(IDbConnection dbConexao)
    {
        _dbConexao = dbConexao;
    }
    public async Task DeleteAsync(int id)
    {
        await _dbConexao.ExecuteAsync("DELETE FROM Fabricantes WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Fabricante>> GetAllAsync()
    {
        return await _dbConexao.QueryAsync<Fabricante>("SELECT * FROM Fabricantes");
    }

    public async Task<Fabricante?> GetByIdAsync(int? id)
    {
        return await _dbConexao.QuerySingleOrDefaultAsync<Fabricante>("SELECT * FROM Fabricantes WHERE Id = @Id", new { Id = id });
    }

    public async Task InsertAsync(Fabricante fabricante)
    {
        var sql = @"INSERT INTO Fabricantes (Nome) VALUES (@Nome); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        fabricante.Id = await _dbConexao.QuerySingleOrDefaultAsync<int>(sql, fabricante);
    }

    public async Task UpdateAsync(Fabricante fabricante)
    {
        var sql = @"UPDATE Fabricantes SET Nome = @Nome WHERE Id = @Id";
        await _dbConexao.ExecuteAsync(sql, fabricante);
    }
}
