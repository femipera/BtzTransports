using Dapper;
using System.Data;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;

namespace WebAppBtzTransports.Repositories;

public class AbastecimentoRepository : IBaseRepository<Abastecimento>
{
    private IDbConnection _dbConexao;
    public AbastecimentoRepository(IDbConnection dbConexao)
    {
        _dbConexao = dbConexao;
    }
    
    public async Task DeleteAsync(int id)
    {
        await _dbConexao.ExecuteAsync("DELETE FROM Abastecimentos WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Abastecimento>> GetAllAsync()
    {
        var sql = @"
        SELECT 
            a.Id, 
            a.MotoristaId,
            m.Nome, 
            m.CPF,
            m.NumeroCNH,
            m.DataNascimento,
            m.Status,
            m.CNHCategoriaId,
            c.Id,
            c.Nome,
            a.VeiculoId,
            v.Nome,
            v.Placa,
            v.AnoFabricacao,
            v.CapacidadeMaximaTanque,
            v.Observacao,
            v.FabricanteId,
            f.Id,
            f.Nome,
            a.CombustivelId,
            co.Id,
            co.Nome,
            co.Preco,
            a.Data,
            a.QuantidadeAbastecida,
            a.CombustivelPreco
        FROM Abastecimentos a
        LEFT JOIN Motoristas m ON a.MotoristaId = m.Id
        LEFT JOIN CNHCategorias c ON m.CNHCategoriaId = c.Id
        LEFT JOIN Veiculos v ON a.VeiculoId = v.Id
        LEFT JOIN Fabricantes f ON v.FabricanteId = f.Id
        LEFT JOIN Combustiveis co ON a.CombustivelId = co.Id";

        return await _dbConexao.QueryAsync<Abastecimento, Motorista, CNHCategoria, Veiculo, Fabricante, Combustivel, Abastecimento>(
            sql,
            (abastecimento, motorista, categoriaCNH, veiculo, fabricante, combustivel) =>
            {
                abastecimento.Motorista = motorista;
                motorista.CNHCategoria = categoriaCNH;
                abastecimento.Veiculo = veiculo;
                veiculo.Fabricante = fabricante;
                veiculo.Combustivel = combustivel;
                return abastecimento;
            },

            splitOn: "MotoristaId,CNHCategoriaId,VeiculoId,FabricanteId,CombustivelId"
        );
    }

    public async Task<Abastecimento?> GetByIdAsync(int? id)
    {
        var sql = @"
        SELECT 
            a.Id, 
            a.MotoristaId,
            m.Nome, 
            m.CPF,
            m.NumeroCNH,
            m.DataNascimento,
            m.Status,
            m.CNHCategoriaId,
            c.Id,
            c.Nome,
            a.VeiculoId,
            v.Nome,
            v.Placa,
            v.AnoFabricacao,
            v.CapacidadeMaximaTanque,
            v.Observacao,
            v.FabricanteId,
            f.Id,
            f.Nome,
            a.CombustivelId,
            co.Id,
            co.Nome,
            co.Preco,
            a.Data,
            a.QuantidadeAbastecida,
            a.CombustivelPreco
        FROM Abastecimentos a
        LEFT JOIN Motoristas m ON a.MotoristaId = m.Id
        LEFT JOIN CNHCategorias c ON m.CNHCategoriaId = c.Id
        LEFT JOIN Veiculos v ON a.VeiculoId = v.Id
        LEFT JOIN Fabricantes f ON v.FabricanteId = f.Id
        LEFT JOIN Combustiveis co ON a.CombustivelId = co.Id
        WHERE a.Id = @Id";

        var abastecimentoComRelacionamentos = await _dbConexao.QueryAsync<Abastecimento, Motorista, CNHCategoria, Veiculo, Fabricante, Combustivel, Abastecimento>(
            sql,
            (abastecimento, motorista, categoriaCNH, veiculo, fabricante, combustivel) =>
            {
                abastecimento.Motorista = motorista;
                motorista.CNHCategoria = categoriaCNH;
                abastecimento.Veiculo = veiculo;
                veiculo.Fabricante = fabricante;
                veiculo.Combustivel = combustivel;
                return abastecimento;
            },
            new { Id = id },
            splitOn: "MotoristaId,CNHCategoriaId,VeiculoId,FabricanteId,CombustivelId"
        );
        return abastecimentoComRelacionamentos.SingleOrDefault();
    }

    public async Task InsertAsync(Abastecimento abastecimento)
    {
        var sql = @"INSERT INTO Abastecimentos(MotoristaId, VeiculoId, CombustivelId, Data, QuantidadeAbastecida, CombustivelPreco)
                    VALUES(@MotoristaId, @VeiculoId, @CombustivelId, @Data, @QuantidadeAbastecida, @CombustivelPreco);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        
        var parameters = new
        {
            abastecimento.MotoristaId,
            abastecimento.VeiculoId,
            abastecimento.CombustivelId,
            abastecimento.Data,
            abastecimento.QuantidadeAbastecida,
            CombustivelPreco = abastecimento.Combustivel.Preco,
        };

        abastecimento.Id = await _dbConexao.QuerySingleOrDefaultAsync<int>(sql, parameters);      
    }

    public async Task UpdateAsync(Abastecimento abastecimento)
    {
        var sql = @"UPDATE Abastecimentos SET MotoristaId = @MotoristaId, VeiculoId = @VeiculoId, CombustivelId = @CombustivelId, 
                    Data = @Data, QuantidadeAbastecida = @QuantidadeAbastecida, CombustivelPreco = @CombustivelPreco WHERE Id = @Id";
            
        await _dbConexao.ExecuteAsync(sql, abastecimento);
    }
}
