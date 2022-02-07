using System;

namespace Domain.DTOs
{
    /// <summary>
    /// Modelo de resposta da requisição da API 1 com os dados do Token.
    /// </summary>
    public class CardRegistrationResultDTO
    {
        /// <summary>
        /// Dada de registro do cartão.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Token para validação das requisições.
        /// </summary>
        public long Token { get; set; }

        /// <summary>
        /// ID do cartão.
        /// </summary>
        public int CardId { get; set; }
    }
}
