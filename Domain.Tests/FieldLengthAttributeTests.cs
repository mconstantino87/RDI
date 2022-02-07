using Domain.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Domain.Tests
{
    /// <summary>
    /// Classe para teste para o atributo <see cref="FieldLengthAttribute"/>.
    /// </summary>
    public class FieldLengthAttributeTests
    {
        /// <summary>
        /// Teste que verifica se há notificações no objeto que usa o atributo quando dados inválidos são passados.
        /// </summary>
        [Fact]
        public void InvalidFields_ShouldResultInErrorMessage()
        {
            // Arrange
            CustomerCardDTO customerCard = new CustomerCardDTO();
            var results = new List<ValidationResult>();

            // Act
            customerCard.CardNumber = 1234567890123456789;
            customerCard.CVV = 1234567890;
            var ctx = new ValidationContext(customerCard);
            Validator.TryValidateObject(customerCard, ctx, results, true);

            // Assert
            Assert.Equal(expected: 2, actual: results.Count);

            Assert.Equal(expected: $"{nameof(customerCard.CardNumber)} must have between 1 and 16 digits.", 
                            actual: results[0].ErrorMessage);

            Assert.Equal(expected: $"{nameof(customerCard.CVV)} must have between 1 and 5 digits.", 
                            actual: results[1].ErrorMessage);
        }

        /// <summary>
        /// Teste que verifica se não há erros quando um objeto que usa o atributo está em estado consistente.
        /// </summary>
        [Fact]
        public void ValidFields_ShouldResultNoErrors()
        {
            // Arrange
            CustomerCardDTO customerCard = new CustomerCardDTO();
            var results = new List<ValidationResult>();

            // Act
            customerCard.CardNumber = 12345;
            customerCard.CVV = 1;
            var ctx = new ValidationContext(customerCard);
            Validator.TryValidateObject(customerCard, ctx, results, true);

            // Assert
            Assert.Empty(results);
        }

    }
}
