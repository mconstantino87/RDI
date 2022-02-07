using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Base
{
    /// <summary>
    /// Classe que representa uma entidade base para geração de dados no banco de dados.
    /// </summary>
    /// <typeparam name="T">Tipo do atributo que representa a chave primária.</typeparam>
    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// Id do cliente, sendo a chave primária.
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public T CustomerId { get; set; }
    }
}
