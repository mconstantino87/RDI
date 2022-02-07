using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    /// <summary>
    /// Repositório de dados (Usando Entity Framework) para salvar os dados dos cartões registrados.
    /// </summary>
    public class CustomerCardRepository : ICustomerCardRepository
    {
        /// <summary>
        /// Contexto do banco de dados (Entity framework)
        /// </summary>
        private ApiContext _apiContext;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="apiContext">Contexto do banco de dados.</param>
        public CustomerCardRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        /// <summary>
        /// Método para retornar as informações de um cartão de acordo com o número do cartão especificado.
        /// </summary>
        /// <param name="cardNumber">Número do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do cartão, nulo se não existir.</returns>
        public async Task CreateAsync(CustomerCardEntity customerCard)
        {
            await _apiContext.AddAsync(customerCard);
            await _apiContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para retornar as informações de um cartão de acordo com o número do cartão especificado.
        /// </summary>
        /// <param name="customerId">Id do cliente do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do cartão, nulo se não existir.</returns>
        public async Task<IEnumerable<CustomerCardEntity>> GetAllAsync()
        {
            return await _apiContext.CustomerCards.ToListAsync();
        }

        /// <summary>
        /// Método para retornar as informações de um cartão de acordo com o número do cartão especificado.
        /// </summary>
        /// <param name="cardNumber">Número do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do cartão, nulo se não existir.</returns>
        public async Task<CustomerCardEntity> GetCustomerCardByCardNumberAsync(long cardNumber)
        {
            return await _apiContext.CustomerCards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

        /// <summary>
        /// Método para retornar as informações de um cartão de acordo com o número do cartão especificado.
        /// </summary>
        /// <param name="customerId">Id do cliente do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do cartão, nulo se não existir.</returns>
        public async Task<CustomerCardEntity> GetCustomerCardByCustomerIdAsync(int customerId)
        {
            return await _apiContext.CustomerCards.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
    }
}
