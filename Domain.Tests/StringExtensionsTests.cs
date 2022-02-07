using Domain.Extensions;
using Xunit;

namespace Domain.Tests
{
    /// <summary>
    /// Classe para teste de extensões de strings, encontrados em <see cref="StringExtensions"/>
    /// </summary>
    public class StringExtensionsTests
    {
        /// <summary>
        /// Teste que verifica se o método IsNumber é executado corretamente  ao receber uma string apenas com dígitos.
        /// </summary>
        /// <param name="data">string a ser testada.</param>
        [Theory]
        [InlineData("1")]
        [InlineData("30")]
        [InlineData("999")]
        [InlineData("102455699")]
        [InlineData("318341837349")]
        public void IsNumber_ValidNumbers_ShouldReturnTrue(string data)
        {
            Assert.True(data.IsNumber());
        }

        /// <summary>
        /// Teste que verifica se o método IsNumber é executado corretamente ao receber uma string contendo dados que não são dígitos.
        /// </summary>
        /// <param name="data">string a ser testada.</param>
        [Theory]
        [InlineData("")]
        [InlineData("1a")]
        [InlineData("f")]
        [InlineData("12345!")]
        public void IsNumber_InvalidNumbers_ShouldReturnFalse(string data)
        {
            Assert.False(data.IsNumber());
        }

        /// <summary>
        /// Teste que verificar se o método IsInLength é executado corretamente ao receber uma string 
        /// com comprimento dentro da faixa esperada.
        /// </summary>
        /// <param name="data">string a ser testada.</param>
        [Theory]
        [InlineData("a", 1, 1)]
        [InlineData("a", 1, 2)]
        [InlineData("abcd", 1, 5)]
        public void IsInLength_InCorrectRange_ShouldReturnTrue(string value, uint minLength, uint maxLength)
        {
            Assert.True(value.IsInLength(minLength,maxLength));
        }

        /// <summary>
        /// Teste que verificar se o método IsInLength é executado corretamente ao receber uma string 
        /// com comprimento fora da faixa esperada.
        /// </summary>
        /// <param name="data">string a ser testada.</param>
        [Theory]
        [InlineData("a", 0, 0)]
        [InlineData("abcd", 2, 2)]
        [InlineData("abcd", 1, 2)]
        public void IsInLength_InInvalidRange_ShouldReturnFalse(string value, uint minLength, uint maxLength)
        {
            Assert.False(value.IsInLength(minLength, maxLength));
        }
    }
}


