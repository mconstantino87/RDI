using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Data.Repository
{
    /// <summary>
    /// Interface responsável pelas operações de cadastro de cartões no repositório.
    /// </summary>
    public interface ICustomerCardRepository : IRepository<CustomerCardEntity>
    {
        /// <summary>
        /// Método para retornar as informações de um cartão de acordo com o número do cartão especificado.
        /// </summary>
        /// <param name="cardNumber">Número do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do cartão, nulo se não existir.</returns>
        Task<CustomerCardEntity> GetCustomerCardByCardNumberAsync(long cardNumber);

        /// <summary>
        /// Método para retornar as informações de um cartão de acordo com o número do cartão especificado.
        /// </summary>
        /// <param name="customerId">Id do cliente do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do cartão, nulo se não existir.</returns>
        Task<CustomerCardEntity> GetCustomerCardByCustomerIdAsync(int customerId);
    }
}
