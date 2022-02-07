using Application.Exceptions;
using Application.Services;
using Data.Repository;
using Domain.DTOs;
using Domain.Entities;
using IoC;
using Moq;
using RDIChallengeWebAPI.Model;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    /// <summary>
    /// Classe para testar o registro de cartões no sistema. 
    /// Deve garantir que ao salvar um novo cartão as informações do token sejam salvar para futuras consultas 
    /// e que dados já existentes lancem exceções ao cadastrar.
    /// </summary>
    public class CardRegisterServiceTests
    {
        /// <summary>
        /// Serviço de registro de cartões que será testado.
        /// </summary>
        private readonly CardRegisterService _userRegisterService;

        /// <summary>
        /// Mock para criação de token.
        /// </summary>
        private Mock<ITokenCreatorService<CustomerCardEntity>> _mockTokenCreatorService;

        /// <summary>
        /// Mock para registro do cartao no sistema.
        /// </summary>
        private Mock<ICustomerCardRepository> _mockCustomerCardRepository;

        /// <summary>
        /// Mock para registro de token apos salvar as informações do novo cartão.
        /// </summary>
        private Mock<IUserTokenInfoRepository> _mockUserTokenInfoRepository;

        /// <summary>
        /// Construtor da classe, inicialização dos mocks.
        /// </summary>
        public CardRegisterServiceTests()
        {
            // Arrange
            var autoMapper = AutoMapperExtensions.CreateAutoMapper();
            _mockTokenCreatorService = new Mock<ITokenCreatorService<CustomerCardEntity>>();
            _mockCustomerCardRepository = new Mock<ICustomerCardRepository>();     
            _mockUserTokenInfoRepository = new Mock<IUserTokenInfoRepository>();
            _userRegisterService = new CardRegisterService(_mockCustomerCardRepository.Object, 
                                                            _mockUserTokenInfoRepository.Object, 
                                                            _mockTokenCreatorService.Object, 
                                                            autoMapper);
        }

        /// <summary>
        /// Método para testar se ao registrar um ID já existente, uma exceção será disparada.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegisterUserAsync_WithAlreadyExistsCustomerID_ShouldThrowItemAlreadyExistsException()
        {
            // Arrange
            CustomerCardDTO dataToRegister = new CustomerCardDTO();
            CustomerCardEntity existing = new CustomerCardEntity();
            _mockCustomerCardRepository.Setup(x => x.GetCustomerCardByCustomerIdAsync(It.IsAny<int>()))
                                        .ReturnsAsync(existing);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ItemsAlreadyExistsException>(
                async () =>
                {
                    await _userRegisterService.RegisterUserAsync(dataToRegister);
                }
            );

            Assert.Single(exception.Items);
            Assert.Equal(expected: $"The card with {nameof(existing.CustomerId)} {existing.CustomerId} already exists.",  actual: exception.Items[0]);
            _mockUserTokenInfoRepository.Verify(x => x.AddUserTokenInfo(It.IsAny<UserTokenInfo>()), Times.Never);
        }

        /// <summary>
        /// Método para testar se ao registrar um ID já existente, uma exceção será disparada.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegisterUserAsync_WithAlreadyExistsCardNumber_ShouldThrowItemAlreadyExistsException()
        {
            // Arrange
            CustomerCardDTO dataToRegister = new CustomerCardDTO();
            CustomerCardEntity existing = new CustomerCardEntity();
            _mockCustomerCardRepository.Setup(x => x.GetCustomerCardByCardNumberAsync(It.IsAny<long>()))
                                        .ReturnsAsync(existing);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ItemsAlreadyExistsException>(
                async () =>
                {
                    await _userRegisterService.RegisterUserAsync(dataToRegister);
                }
            );

            Assert.Single(exception.Items);
            Assert.Equal(expected: $"The Card Number {existing.CardNumber} already exists.",
                            actual: exception.Items[0]);

            _mockUserTokenInfoRepository.Verify(x => x.AddUserTokenInfo(It.IsAny<UserTokenInfo>()), Times.Never);
        }

        /// <summary>
        /// Método para testar se ao registrar um novo usuário, o token será salvo para consultas futuras.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegisterUserAsync_WithNewCardRegister_ShouldSaveToken()
        {
            // Arrange
            CustomerCardDTO dataToRegister = new CustomerCardDTO();

            // Act
            await _userRegisterService.RegisterUserAsync(dataToRegister);

            // Assert
            _mockTokenCreatorService.Verify(x => x.CreateToken(It.IsAny<CustomerCardEntity>()), Times.Once);
            _mockUserTokenInfoRepository.Verify(x => x.AddUserTokenInfo(It.IsAny<UserTokenInfo>()), Times.Once);
        }

    }
}
