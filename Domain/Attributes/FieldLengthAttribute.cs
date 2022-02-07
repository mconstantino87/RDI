using Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Attributes
{
    /// <summary>
    /// Classe responsável por fornecer um atributo de anotação para validar o tamanho mínimo e máximos de valores do tipo inteiro.
    /// </summary>
    public class FieldLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// Tamanho mínimo permitido pelo valor a ser validado.
        /// </summary>
        private readonly uint _minLength;

        /// <summary>
        /// Tamanho máximo permitido pelo valor a ser validado.
        /// </summary>
        private readonly uint _maxLength;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="minLength">Tamanho mínimo permitido pelo valor a ser validado.</param>
        /// <param name="maxLength">Tamanho máximo permitido pelo valor a ser validado.</param>
        /// <exception cref="ArgumentException">O valor mínimo é maior que o valor máximo.</exception>
        public FieldLengthAttribute(uint minLength, uint maxLength)
        {
            if (minLength > maxLength)
            {
                throw new ArgumentException($"{nameof(maxLength)} should be less than {nameof(minLength)}");
            }
            _minLength = minLength;
            _maxLength = maxLength;
        }

        /// <summary>
        /// Método que realiza a validação do atributo.
        /// </summary>
        /// <param name="value">Valor a ser validado.</param>
        /// <param name="validationContext">Contexto da validação.</param>
        /// <returns>Objeto ValidationResult com os resultados da validação.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            if (value != null)
            {
                string str = value.ToString();
                valid = str.IsInLength(_minLength, _maxLength) && str.IsNumber();
            }

            return valid ? ValidationResult.Success
                : new ValidationResult($"{validationContext.DisplayName} must have between {_minLength} and {_maxLength} digits.");
        }
    }
}
