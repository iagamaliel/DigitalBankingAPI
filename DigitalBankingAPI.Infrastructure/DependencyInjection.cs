using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using DigitalBankingAPI.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalBankingAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();

            return services;
        }
    }
}
