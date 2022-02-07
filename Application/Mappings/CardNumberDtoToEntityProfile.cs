using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace RDIChallengeWebAPI.Mappings
{
    /// <summary>
    /// Perfil do AutoMapper que relaciona os dados de registro do cartão, entre entidade e DTO.
    /// </summary>
    public class CardNumberDtoToEntityProfile : Profile
    {
        /// <summary>
        /// Construtor da classe, realiza o mapeamento entre as entidades.
        /// </summary>
        public CardNumberDtoToEntityProfile()
        {
            CreateMap<CustomerCardEntity, CustomerCardDTO>().ReverseMap();
        }
    }
}
