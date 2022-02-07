using Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    /// <summary>
    /// Classe que representa o DTO com os dados do cartão, recebido na requisição da API 1.
    /// </summary>
    public class CustomerCardDTO
    {
        /// <summary>
        /// ID do cliente.
        /// </summary>
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Número do cartão.
        /// </summary>
        [FieldLength(1,16)]
        public long CardNumber { get; set; }

        /// <summary>
        /// Número de verificação (CVV).
        /// </summary>
        [FieldLength(1,5)]
        public int CVV { get; set; }
    }

}
