using Dapper;
using System.Data;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Repositories;

public class MotoristaRepository : IBaseRepository<Motorista>
{
    private IDbConnection _dbConexao;
    public MotoristaRepository(IDbConnection dbConexao)
    {
        _dbConexao = dbConexao;
    }
    public async Task DeleteAsync(int id)
    {
        await _dbConexao.ExecuteAsync("DELETE FROM Motoristas WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Motorista>> GetAllAsync()
    {

        var sql = @"
            SELECT 
                m.Id,
                m.Nome,
                m.CPF,
                m.NumeroCNH,
                m.DataNascimento,
                m.Status,
                m.CNHCategoriaId,
                c.Id,
                c.Nome
            FROM 
                Motoristas m Left JOIN 
                CNHCategorias c ON m.CNHCategoriaId = c.Id"; 
 
        return await _dbConexao.QueryAsync<Motorista, CNHCategoria, Motorista>(
            sql,
            (motorista, cnhCategoria) =>
            {
                motorista.CNHCategoria = cnhCategoria;
                return motorista;
            },
            splitOn: "CNHCategoriaId"
        );
    }

    public async Task<Motorista?> GetByIdAsync(int? id)
    {
        var sql = @"
            SELECT 
                m.Id,
                m.Nome,
                m.CPF,
                m.NumeroCNH,
                m.DataNascimento,
                m.Status,
                m.CNHCategoriaId,
                c.Id,
                c.Nome
            FROM 
                Motoristas m Left JOIN 
                CNHCategorias c ON m.CNHCategoriaId = c.Id 
            WHERE 
                m.Id = @Id";

        var motoristaComRelacionamento = await _dbConexao.QueryAsync<Motorista, CNHCategoria, Motorista>(
            sql, 
            (motorista, cnhCategoria) =>
            {
                motorista.CNHCategoria = cnhCategoria;
                return motorista;
            },
            new { Id = id },
            splitOn: "CNHCategoriaId"
            );
        return motoristaComRelacionamento.SingleOrDefault();
    }

    public async Task InsertAsync(Motorista motorista)
    {
        var sql = @"INSERT INTO Motoristas (Nome, CPF, NumeroCNH, DataNascimento, Status, CNHCategoriaId) 
                       VALUES (@Nome, @CPF, @NumeroCNH, @DataNascimento, 1, @CNHCategoriaId); 
                       SELECT CAST(SCOPE_IDENTITY() AS INT);";
        motorista.Id = await _dbConexao.QuerySingleOrDefaultAsync<int>(sql, motorista);
    }

    public async Task UpdateAsync(Motorista motorista)
    {
        var sql = @"UPDATE Motoristas SET Nome = @Nome, CPF = @CPF, NumeroCNH = @NumeroCNH, DataNascimento = @DataNascimento, Status = @Status, CNHCategoriaId = @CNHCategoriaId WHERE Id = @Id";
        await _dbConexao.ExecuteAsync(sql, motorista);
    }
}
