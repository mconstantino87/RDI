using Domain.Interfaces;
using RDIChallengeWebAPI.Model;

namespace Data.Repository
{
    /// <summary>
    /// Interface responsável pelas operações de cadastro de tokens no repositório.
    /// </summary>
    public interface IUserTokenInfoRepository 
    { 
        /// <summary>
        /// Método para adicionar a informação de um token no repositório.
        /// </summary>
        /// <param name="tokenInfo">Informação do token a ser adicionada.</param>
        void AddUserTokenInfo(UserTokenInfo tokenInfo);

        /// <summary>
        /// Método para obter os dados um token cadastrad pelo id do cliente.
        /// </summary>
        /// <param name="customerId">Id do cliente do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do token, nulo se não existir.</returns>
        UserTokenInfo GetUserTokenInfoById(int customerId);

        /// <summary>
        /// Método para obter os dados um token cadastrad pelo id do cartão.
        /// </summary>
        /// <param name="cardId">Id do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do token, nulo se não existir.</returns>
        UserTokenInfo GeUserTokenInfoByCardId(int cardId);
    }
}
