using FluentValidation;
using WebAppBtzTransports.Interfaces;
using WebAppBtzTransports.Models;
using WebAppBtzTransports.Repositories;

namespace WebAppBtzTransports.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Usuario>, UsuarioRepository>();
            services.AddScoped<IBaseRepository<CNHCategoria>, CNHCategoriaRepository>();
            services.AddScoped<IBaseRepository<Combustivel>, CombustivelRepository>();
            services.AddScoped<IBaseRepository<Fabricante>, FabricanteRepository>();
            services.AddScoped<IBaseRepository<Motorista>, MotoristaRepository>();
            services.AddScoped<IBaseRepository<Veiculo>, VeiculoRepository>();
            services.AddScoped<IBaseRepository<Abastecimento>, AbastecimentoRepository>();
            services.AddTransient<IValidator<Abastecimento>, AbastecimentoValidator>();

            return services;
        }
    }
}