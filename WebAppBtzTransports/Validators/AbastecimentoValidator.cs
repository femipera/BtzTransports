using FluentValidation;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;
using WebAppBtzTransports.Repositories;

public class AbastecimentoValidator : AbstractValidator<Abastecimento>
{
    private readonly IBaseRepository<Veiculo> _veiculoRepository;

    public AbastecimentoValidator(IBaseRepository<Veiculo> veiculoRepository)
    {
        _veiculoRepository = veiculoRepository;

        RuleFor(abastecimento => abastecimento)
             .MustAsync(async (abastecimento, cancellationToken) =>
             {
                 var veiculo = await _veiculoRepository.GetByIdAsync(abastecimento.VeiculoId);
                 return veiculo != null && abastecimento.QuantidadeAbastecida <= veiculo.CapacidadeMaximaTanque;
             })
             .WithMessage("A quantidade abastecida excede a capacidade máxima do tanque do veículo.");

        //RuleFor(abastecimento => abastecimento)
        //     .MustAsync(async (abastecimento, cancellationToken) =>
        //     {
        //         var veiculo = await _veiculoRepository.GetByIdAsync(abastecimento.VeiculoId);

        //         return veiculo != null && abastecimento != null && veiculo.CombustivelId == abastecimento.CombustivelId;
        //     })
        //     .WithMessage("O combustível não pode ser usado por este veículo.");

    }
}






