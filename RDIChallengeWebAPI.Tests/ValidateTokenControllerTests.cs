using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RDIChallengeWebAPI.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace RDIChallengeWebAPI.Tests
{
    /// <summary>
    /// Classe para teste da controller <see cref="ValidateTokenController"/>.
    /// </summary>
    public class ValidateTokenControllerTests
    {
        /// <summary>
        /// Mock para simular o validador de token.
        /// </summary>
        private readonly Mock<ITokenInfoValidatorService> _tokenValidatorMock;

        /// <summary>
        /// Dados do token a serem confirmados
        /// </summary>
        private readonly TokenValidationDTO _tokenValidationDTO;

        /// <summary>
        /// Controller de teste.
        /// </summary>
        private readonly ValidateTokenController _controller;

        /// <summary>
        /// Inicializador dos testes.
        /// </summary>
        public ValidateTokenControllerTests()
        {
            // Arrange
            _tokenValidatorMock = new Mock<ITokenInfoValidatorService>();
            _tokenValidationDTO = new TokenValidationDTO();
            _controller = new ValidateTokenController(_tokenValidatorMock.Object);
        }


        /// <summary>
        /// Teste que verifica se o método GET retorna OK quando recebe um token válido e
        /// o valor true se a validação passar.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostMethod_WithTokenValid_ShouldReturn200Ok_WithContentAsTrue()
        {
            // Arrange
            _tokenValidatorMock.Setup(x => x.IsValidAsync(It.IsAny<TokenValidationDTO>()))
                                                                    .ReturnsAsync(true);

            // Act
            var result = await _controller.Post(_tokenValidationDTO);
            _tokenValidatorMock.Verify(x => x.IsValidAsync(_tokenValidationDTO), Times.Once);

            // Assert
            Assert.True(result is OkObjectResult);
            Assert.Equal(expected: true, actual: (result as OkObjectResult).Value);
        }

        /// <summary>
        /// Teste que verifica se o método POST retorna OK quando recebe um token válido e
        /// o valor true se a validação passar.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostMethod_WithTokenInvalid_ShouldReturn200Ok_WithContentAsFalse()
        {
            // Arrange
            _tokenValidatorMock.Setup(x => x.IsValidAsync(It.IsAny<TokenValidationDTO>()))
                                                                    .ReturnsAsync(false);

            // Act
            var result = await _controller.Post(_tokenValidationDTO);
            _tokenValidatorMock.Verify(x => x.IsValidAsync(_tokenValidationDTO), Times.Once);

            // Assert
            Assert.True(result is OkObjectResult);
            Assert.Equal(expected: false, actual: (result as OkObjectResult).Value);
        }

    }
}
