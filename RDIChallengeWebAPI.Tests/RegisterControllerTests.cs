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
    /// Classe para teste da controller <see cref="RegisterController"/>.
    /// </summary>
    public class RegisterControllerTests
    {
        /// <summary>
        /// Mock para simular interface de serviço.
        /// </summary>
        private readonly Mock<ICardRegisterService> _userServiceMock;

        /// <summary>
        /// Dados que serão registrados.
        /// </summary>
        private readonly CustomerCardDTO _customerCard;

        /// <summary>
        /// Controller de teste.
        /// </summary>
        private readonly RegisterController _controller; 

        /// <summary>
        /// Inicializador dos testes.
        /// </summary>
        public RegisterControllerTests()
        {
            // Arrange
            _userServiceMock = new Mock<ICardRegisterService>();
            _customerCard = new CustomerCardDTO();
            _controller = new RegisterController(_userServiceMock.Object);
        }

        /// <summary>
        /// Teste que verifica se o método POST retorna OK quando recebe um objeto válido.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostMethod_WithModelStateOk_ShouldReturn200Ok()
        {
            // Act
            var result = await _controller.Post(_customerCard);
            _userServiceMock.Verify(x => x.RegisterUserAsync(_customerCard), Times.Once);

            // Assert
            Assert.True(result is OkObjectResult);
        }

        /// <summary>
        /// Teste que verifica se o método POST retorna OK quando recebe um objeto válido.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostMethod_WithErrorsInModelState_ShouldReturn400BadRequest()
        {
            // Act
            _controller.ModelState.AddModelError("CVV", "CVV must have between 1 and 5 digits.");
            var result = await _controller.Post(_customerCard);

            // Assert
            Assert.True(result is BadRequestResult);
        }
    }
}
