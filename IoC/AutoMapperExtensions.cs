using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RDIChallengeWebAPI.Mappings;

namespace IoC
{
    /// <summary>
    /// Classe responsável pelas extensões do AutoMapper.
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Método para adicionar o AutoMapper em um container IoC.
        /// </summary>
        /// <param name="services">Serviço para adicionar o container.</param>
        /// <returns>Serviço com o container do AutoMapper adicionado.</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapper = CreateAutoMapper();
            services.AddSingleton(mapper);
            return services;
        }

        /// <summary>
        /// Método para criar as configurações do AutoMapper.
        /// </summary>
        /// <returns>Objeto configurado para os mapeamentos necessários no projeto.</returns>
        public static IMapper CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CardNumberDtoToEntityProfile());
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}
