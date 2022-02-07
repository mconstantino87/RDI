using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    /// <summary>
    /// Classe que representa o DTO com os dados do cartão, recebido na requisição da API 2.
    /// </summary>
    public class TokenValidationDTO
    {
        /// <summary>
        /// ID do cliente.
        /// </summary>
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        /// <summary>
        /// ID do cartão
        /// </summary>
        [Required(ErrorMessage = "CardId is required.")]
        public int CardId { get; set; }

        /// <summary>
        /// Token para validar.
        /// </summary>
        [Required(ErrorMessage = "Token is required.")]
        public long Token { get; set; }

        /// <summary>
        /// Número de verificação (CVV).
        /// </summary>
        [Required(ErrorMessage = "CVV is required.")]
        [Range(0, 99999)]
        public int CVV { get; set; }
    }
}
