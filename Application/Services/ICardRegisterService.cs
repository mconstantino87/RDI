using Domain.DTOs;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Interface responsável por cadastrar um cartão no sistema.
    /// </summary>
    public interface ICardRegisterService
    {
        /// <summary>
        /// Método para cadastrar um novo cartão no sistema.
        /// </summary>
        /// <param name="card">Informações do cartão cadastrado</param>
        /// <returns>Dados de registro do cartão para se obter o token posteriormente.</returns>
        Task<CardRegistrationResultDTO> RegisterUserAsync(CustomerCardDTO card);
    }
}
