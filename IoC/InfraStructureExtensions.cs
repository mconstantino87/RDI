using Application.Services;
using Data.Context;
using Data.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    /// <summary>
    /// Classe responsável pelas extensões de infraestrutura do projeto.
    /// </summary>
    public static class InfraStructureExtensions
    {
        /// <summary>
        /// Método responsável por adicionar a infra-estrutura dos serviços na API.
        /// </summary>
        /// <param name="services">Coleção de serviços para adicionar os serviços da infra-estrutura.</param>
        /// <returns>Nova coleção de serviços com os registros de infra-estrutura.</returns>
        public static IServiceCollection AddInfraStructure(this IServiceCollection services)
        {
            services.AddScoped<ICustomerCardRepository, CustomerCardRepository>();
            services.AddScoped<ITokenCreatorService<CustomerCardEntity>, TokenCreatorService>();
            services.AddScoped<ICardRegisterService, CardRegisterService>();
            services.AddScoped<ITokenInfoValidatorService, TokenInfoValidatorService>();
            services.AddSingleton<IUserTokenInfoRepository, UserTokenInfoRepository>();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("RDIChallengeWebApi"));
            return services;
        }
    }
}
