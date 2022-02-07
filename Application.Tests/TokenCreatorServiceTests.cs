using Application.Services;
using Domain.Entities;
using RDIChallengeWebAPI.Utils;
using System;
using System.Linq;
using Xunit;

namespace Application.Tests
{
    /// <summary>
    /// Classe para testes de geração do token. 
    /// </summary>
    public class TokenCreatorServiceTests 
    {
        /// <summary>
        /// Método para testar o token de acordo com o seguinte algoritmo: 
        /// 
        /// Pegar os 4 ultimos dígitos do cartão. 
        /// Rotacionar os dígitos a quantidade de vezes especificadas no campo CVV.
        /// </summary>
        /// <param name="CardNumber">Número do cartão.</param>
        /// <param name="CVV">Código que indica quantas vezes o número será rotacionado.</param>
        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 1000)]
        [InlineData(1111222233334444, 523)]
        [InlineData(1234123412341234, 10)]
        public void CreateToken_ShouldReturnLastDigitsRotatedWithCVV(long CardNumber, int CVV)
        {
            // Arrange
            TokenCreatorService tokenCreatorService = new TokenCreatorService();
            CustomerCardEntity customerCard = new CustomerCardEntity();

            var paddingNumber = CardNumber.ToString().PadLeft(16, '0');
            char[] lastFourDigits = paddingNumber.Skip(paddingNumber.Length - 4).ToArray();
            ArrayUtils.ArrayRotate(lastFourDigits, CVV);
            long newToken = Convert.ToInt64(new string(lastFourDigits));

            customerCard.CardNumber = CardNumber;
            customerCard.CVV = CVV;

            // Act
            long token = tokenCreatorService.CreateToken(customerCard);
            
            // Assert
            Assert.Equal(newToken, token);
        }
    }
}
