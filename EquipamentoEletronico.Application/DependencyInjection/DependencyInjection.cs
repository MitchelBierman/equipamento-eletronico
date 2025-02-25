using EquipamentoEletronico.Application.Services;
using EquipamentoEletronico.Domain.Interfaces;
using EquipamentoEletronico.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace EquipamentoEletronico.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(typeof(EquipamentoEletronicoValidator).Assembly);

            services.AddScoped<IEquipamentoService, EquipamentoService>();

            services.AddDbContext<EquipamentoEletronicoDbContext>(options =>
                options.UseInMemoryDatabase("EquipamentosDB"));

            return services;
        }
    }
}
