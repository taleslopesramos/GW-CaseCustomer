using CaseCustumer.App.Services;
using CaseCustumer.App.Services.Interfaces;
using CaseCustumer.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaseCustumer.App.DI
{
    public static class DependencyServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ClienteDbContext>(options => options.UseSqlServer(connection));
            services.AddSingleton<IEnderecoService, EnderecoService>();
            services.AddScoped<IClienteService, ClienteService>();

            return services;
        }
    }
}
