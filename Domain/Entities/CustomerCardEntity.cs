using Domain.Entities.Base;

namespace Domain.Entities
{
    /// <summary>
    /// Entidade a ser gravada no banco de dados com informações sobre o cartão.
    /// </summary>
    public class CustomerCardEntity : BaseEntity<int>
    {
        /// <summary>
        /// Número do cartão.
        /// </summary>
        public long CardNumber { get; set; }

        /// <summary>
        /// Número de verificação (CVV).
        /// </summary>
        public int CVV { get; set; }
    }
}
