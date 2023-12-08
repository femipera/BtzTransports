using Dapper;
using System.Data;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Repositories;

public class VeiculoRepository : IBaseRepository<Veiculo>
{
    private IDbConnection _dbConexao;
    public VeiculoRepository(IDbConnection dbConexao)
    {
        _dbConexao = dbConexao;
    }

    public async Task DeleteAsync(int id)
    {
        await _dbConexao.ExecuteAsync("DELETE FROM Veiculos WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Veiculo>> GetAllAsync()
    {
        var sql = @"
        SELECT 
            v.Id, 
            v.Nome,
            v.Placa,
            v.AnoFabricacao,
            v.CapacidadeMaximaTanque,
            v.Observacao,
            v.FabricanteId,
            f.Id,
            f.Nome,
            v.CombustivelId,
            c.Id,
            c.Nome,
            c.Preco
        FROM Veiculos v
        LEFT JOIN Fabricantes f ON v.FabricanteId = f.Id
        LEFT JOIN Combustiveis c ON v.CombustivelId = c.Id";

        return await _dbConexao.QueryAsync<Veiculo, Fabricante, Combustivel, Veiculo>(
            sql,
            (veiculo, fabricante, combustivel) =>
            {
                veiculo.Fabricante = fabricante;
                veiculo.Combustivel = combustivel;
                return veiculo;
            },
            splitOn: "FabricanteId,CombustivelId"
        );
    }

    public async Task<Veiculo?> GetByIdAsync(int? id)
    {
        var sql = @"
        SELECT 
            v.Id, 
            v.Nome,
            v.Placa,
            v.AnoFabricacao,
            v.CapacidadeMaximaTanque,
            v.Observacao,
            v.FabricanteId,
            f.Id,
            f.Nome,
            v.CombustivelId,
            c.Id,
            c.Nome,
            c.Preco
        FROM Veiculos v
        LEFT JOIN Fabricantes f ON v.FabricanteId = f.Id
        LEFT JOIN Combustiveis c ON v.CombustivelId = c.Id
        WHERE 
            v.Id = @Id";

        var veiculoComRelacionamento = await _dbConexao.QueryAsync<Veiculo, Fabricante, Combustivel, Veiculo>(
            sql,
            (veiculo, fabricante, combustivel) =>
            {
                veiculo.Fabricante = fabricante;
                veiculo.Combustivel = combustivel;
                return veiculo;
            },
            new { Id = id },
            splitOn: "FabricanteId,CombustivelId"
            );
        return veiculoComRelacionamento.SingleOrDefault();
    }

    public async Task InsertAsync(Veiculo veiculo)
    {
        string sql = @"INSERT INTO Veiculos (Nome, Placa, AnoFabricacao, CapacidadeMaximaTanque, Observacao, FabricanteId, CombustivelId) 
                       VALUES (@Nome, @Placa, @AnoFabricacao, @CapacidadeMaximaTanque, @Observacao, @FabricanteId, @CombustivelId); 
                       SELECT CAST(SCOPE_IDENTITY() AS INT);";
        veiculo.Id = await _dbConexao.QuerySingleOrDefaultAsync<int>(sql, veiculo);
    }

    public async Task UpdateAsync(Veiculo veiculo)
    {
        string sql = @"UPDATE Veiculos SET Nome = @Nome, Placa = @Placa, AnoFabricacao = @AnoFabricacao, CapacidadeMaximaTanque = @CapacidadeMaximaTanque,
                       Observacao = @Observacao, FabricanteId = @FabricanteId, CombustivelId = @CombustivelId WHERE Id = @Id";
        await _dbConexao.ExecuteAsync(sql, veiculo);
    }
}
