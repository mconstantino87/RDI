using RDIChallengeWebAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    /// <summary>
    /// Repositório de dados em memória para salvar informações dos tokens gerados.
    /// </summary>
    public class UserTokenInfoRepository : IUserTokenInfoRepository
    {
        /// <summary>
        /// Lista de tokens gerados.
        /// </summary>
        private List<UserTokenInfo> _tokenInfoList;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        public UserTokenInfoRepository()
        {
            _tokenInfoList = new List<UserTokenInfo>();
        }

        /// <summary>
        /// Método para adicionar a informação de um token no repositório.
        /// </summary>
        /// <param name="tokenInfo">Informação do token a ser adicionada.</param>
        public void AddUserTokenInfo(UserTokenInfo tokenInfo)
        {
            _tokenInfoList.Add(tokenInfo);
        }

        /// <summary>
        /// Método para obter os dados um token cadastrad pelo id do cliente.
        /// </summary>
        /// <param name="customerId">Id do cliente do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do token, nulo se não existir.</returns>
        public UserTokenInfo GeUserTokenInfoByCardId(int customerId)
        {
            return _tokenInfoList.FirstOrDefault(x => x.CardId == customerId);
        }

        /// <summary>
        /// Método para obter os dados um token cadastrad pelo id do cartão.
        /// </summary>
        /// <param name="cardId">Id do cartão para retornar a informação.</param>
        /// <returns>Entidade com os dados do token, nulo se não existir.</returns>
        public UserTokenInfo GetUserTokenInfoById(int cardId)
        {
            return _tokenInfoList.FirstOrDefault(x => x.CustomerId == cardId);
        }
    }
}
