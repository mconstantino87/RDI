using System.Linq;

namespace Domain.Extensions
{
    /// <summary>
    /// Classe utilitária com extensões para string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Método que verifica se a string está em um comprimento especificado.
        /// </summary>
        /// <param name="value">String a ser verificada.</param>
        /// <param name="minLength">Tamanho mínimo da string.</param>
        /// <param name="maxLength">Tamanho máximo da string.</param>
        /// <returns>True caso a string esteja no comprimento especificado, false se não estiver.</returns>
        public static bool IsInLength(this string value, uint minLength, uint maxLength)
        {
            return value.Length >= minLength 
                    && value.Length <= maxLength;
        }

        /// <summary>
        /// Método que verifica se a string é um número válido.
        /// </summary>
        /// <param name="value">String a ser verificada.</param>
        /// <returns>True caso a string contenha somente números, false do contrário.</returns>
        public static bool IsNumber(this string value)
        {
            return value.Length > 0 && value.All(c => char.IsDigit(c));
        }
    }
}
