using System;

namespace RDIChallengeWebAPI.Model
{
    /// <summary>
    /// Classe que armazena as informações sobre o token, devolvida como resposta na API 1.
    /// </summary>
    public class UserTokenInfo
    {
        /// <summary>
        /// ID do cliente.
        /// </summary>
        public int CustomerId { get; private set; }

        /// <summary>
        /// ID do cartão.
        /// </summary>
        public int CardId { get; private set; }

        /// <summary>
        /// Data de expiração do token.
        /// </summary>
        public DateTime ExpirationDate { get; private set; }

        /// <summary>
        /// Código do token.
        /// </summary>
        public long Token { get; private set; }

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="customerId"ID do cliente.></param>
        /// <param name="cardId">ID do cartão.</param>
        /// <param name="expirationDate">Data de expiração do token.</param>
        /// <param name="token">Código do token.</param>
        public UserTokenInfo(int customerId, int cardId, DateTime expirationDate, long token)
        {
            CustomerId = customerId;
            CardId = cardId;
            ExpirationDate = expirationDate;
            Token = token;
        }
    }
}
