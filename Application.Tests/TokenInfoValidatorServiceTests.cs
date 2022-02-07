using Application.Services;
using Data.Repository;
using Domain.DTOs;
using Domain.Entities;
using Moq;
using RDIChallengeWebAPI.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    /// <summary>
    /// Classe para testar as validações dos tokens.
    /// </summary>
    public class TokenInfoValidatorServiceTests
    {
        /// <summary>
        /// Mock para simular o repositório de cadastro dos tokens.
        /// </summary>
        private readonly Mock<IUserTokenInfoRepository> _mockUserTokenInfoRepository;

        /// <summary>
        /// Mock para simular o repositório com os dados dos clientes.
        /// </summary>
        private readonly Mock<ICustomerCardRepository> _mockCustomerCardRepository;

        /// <summary>
        /// Moken para simular o serviço de geração de token.
        /// </summary>
        private readonly Mock<ITokenCreatorService<CustomerCardEntity>> _mockTokenCreator;

        /// <summary>
        /// Serviço para validação do token.
        /// </summary>
        private readonly TokenInfoValidatorService _tokenInfoValidatorService;

        /// <summary>
        /// Objeto enviado pelo usuário para validar o token.
        /// </summary>
        private readonly TokenValidationDTO _tokenValidationDTO;

        /// <summary>
        /// Card ID usado nos testes.
        /// </summary>
        private readonly int _cardID = 1;

        /// <summary>
        /// Customer ID usado nos testes.
        /// </summary>
        private readonly int _customerID = 1;

        /// <summary>
        /// CVV usado nos testes.
        /// </summary>
        private readonly int _CVV = 1234;

        /// <summary>
        /// Token usado para validação nos testes.
        /// </summary>
        private readonly int _token = 2512;

        /// <summary>
        /// Construtor que inicializa os dados de testes.
        /// </summary>
        public TokenInfoValidatorServiceTests()
        {
            _tokenValidationDTO = new TokenValidationDTO() 
            {
                CardId = _cardID,
                CustomerId = _customerID,
                CVV = _CVV,
                Token = _token
            };

            // Arrange
            _mockUserTokenInfoRepository = new Mock<IUserTokenInfoRepository>();
            _mockCustomerCardRepository = new Mock<ICustomerCardRepository>();
            _mockTokenCreator = new Mock<ITokenCreatorService<CustomerCardEntity>>();
            _tokenInfoValidatorService = new TokenInfoValidatorService(
                _mockUserTokenInfoRepository.Object, _mockCustomerCardRepository.Object, _mockTokenCreator.Object);
        }

        /// <summary>
        /// Método para testar se ao passar um token que não foi gerado no sistema, o mesmo é inválido.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IsValid_WithNonExistingToken_ShouldReturnFalse()
        {
            // Arrange
            UserTokenInfo mockResult = null;
            _mockUserTokenInfoRepository.Setup(x => x.GetUserTokenInfoById(It.IsAny<int>())).Returns(mockResult);

            // Act
            bool isValid = await _tokenInfoValidatorService.IsValidAsync(_tokenValidationDTO);

            // Assert
            Assert.False(isValid);
        }

        /// <summary>
        /// Método para testar se ao passar um token que tenha uma data expirada, o mesmo é inválido.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IsValid_WithExpiredDate_ShouldReturnFalse()
        {
            // Arrange
            var dateExpired = DateTime.UtcNow.AddMinutes(-1);


            _mockUserTokenInfoRepository.Setup(x => x.GetUserTokenInfoById(It.IsAny<int>()))
                                    .Returns(new UserTokenInfo(_customerID, _cardID, dateExpired, _token));

            // Act
            bool isValid = await _tokenInfoValidatorService.IsValidAsync(_tokenValidationDTO);

            // Assert
            Assert.False(isValid);
        }


        /// <summary>
        /// Método para testar se ao passar um token que tenha o Customer ID incorreto, o mesmo é inválido.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IsValid_WithWrongCustomerID_ShouldReturnFalse()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddMinutes(30);
            int wrongCustomerID = 5;

            UserTokenInfo tokenResult = new UserTokenInfo(_customerID, _cardID, futureDate, _token);

            _mockUserTokenInfoRepository.Setup(x => x.GetUserTokenInfoById(It.IsAny<int>())).Returns(tokenResult);
            _mockUserTokenInfoRepository.Setup(x => x.GeUserTokenInfoByCardId(It.IsAny<int>())).Returns(tokenResult);
            _mockCustomerCardRepository.Setup(x => x.GetCustomerCardByCustomerIdAsync(It.IsAny<int>()))
                                        .ReturnsAsync(new CustomerCardEntity() { CustomerId = wrongCustomerID });

            // Act
            bool isValid = await _tokenInfoValidatorService.IsValidAsync(_tokenValidationDTO);

            // Assert
            Assert.False(isValid);
        }

        /// <summary>
        /// Método para testar se ao passar um token que o Card ID não tenha sido gerado, o mesmo retorna false.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IsValid_WithCardIDNotFound_ShouldReturnFalse()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddMinutes(30);
            UserTokenInfo tokenResultByID = new UserTokenInfo(_customerID, _cardID, futureDate, _token); // Token cadastrado
            UserTokenInfo tokenResultByCardID = null; // Token com erro no cardID

            _mockUserTokenInfoRepository.Setup(x => x.GetUserTokenInfoById(It.IsAny<int>())).Returns(tokenResultByID);
            _mockUserTokenInfoRepository.Setup(x => x.GeUserTokenInfoByCardId(It.IsAny<int>())).Returns(tokenResultByCardID);

            // Act
            bool isValid = await _tokenInfoValidatorService.IsValidAsync(_tokenValidationDTO);

            // Assert
            Assert.False(isValid);
        }

        /// <summary>
        /// Método para testar se ao passar um token que o Customer Entity que não foi cadastrado, o mesmo retorna false. 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IsValid_WithCustomerIdNotFound_ShouldReturnFalse()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddMinutes(30);
            UserTokenInfo tokenResultByID = new UserTokenInfo(_customerID, _cardID, futureDate, _token); 
            CustomerCardEntity customerCardEntity = null;

            _mockUserTokenInfoRepository.Setup(x => x.GetUserTokenInfoById(It.IsAny<int>())).Returns(tokenResultByID);
            _mockUserTokenInfoRepository.Setup(x => x.GeUserTokenInfoByCardId(It.IsAny<int>())).Returns(tokenResultByID);
            _mockCustomerCardRepository.Setup(x => x.GetCustomerCardByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(customerCardEntity);

            // Act
            bool isValid = await _tokenInfoValidatorService.IsValidAsync(_tokenValidationDTO);

            // Assert
            Assert.False(isValid);
        }


        /// <summary>
        /// Método para testar se ao passar um código de token errado, o mesmo retorna false.
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task IsValid_WithInvalidToken_ShouldReturnFalse()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddMinutes(30);
            int wrongToken = 9715;

            UserTokenInfo tokenResult = new UserTokenInfo(_customerID, _cardID, futureDate, wrongToken);
            CustomerCardEntity cardEntity = new CustomerCardEntity() { CustomerId = _customerID };

            // Act
            _mockUserTokenInfoRepository.Setup(x => x.GetUserTokenInfoById(It.IsAny<int>())).Returns(tokenResult);
            _mockUserTokenInfoRepository.Setup(x => x.GeUserTokenInfoByCardId(It.IsAny<int>())).Returns(tokenResult);
            _mockCustomerCardRepository.Setup(x => x.GetCustomerCardByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(cardEntity);
            _mockTokenCreator.Setup(x => x.CreateToken(cardEntity)).Returns(wrongToken);

            bool isValid = await _tokenInfoValidatorService.IsValidAsync(_tokenValidationDTO);

            // Assert
            Assert.False(isValid);
        }

        /// <summary>
        /// Método para testar se qyabdi tidas as condições forem verdadeiras, o mesmo irá retornar true.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IsValid_WithCorrectData_ShouldReturnTrue()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddMinutes(30);
            UserTokenInfo tokenResult = new UserTokenInfo(_customerID, _cardID, futureDate, _token);
            CustomerCardEntity cardEntity = new CustomerCardEntity() { CustomerId = _customerID };

            // Act
            _mockUserTokenInfoRepository.Setup(x => x.GetUserTokenInfoById(It.IsAny<int>())).Returns(tokenResult);
            _mockUserTokenInfoRepository.Setup(x => x.GeUserTokenInfoByCardId(It.IsAny<int>())).Returns(tokenResult);
            _mockCustomerCardRepository.Setup(x => x.GetCustomerCardByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(cardEntity);
            _mockTokenCreator.Setup(x => x.CreateToken(cardEntity)).Returns(_token);

            bool isValid = await _tokenInfoValidatorService.IsValidAsync(_tokenValidationDTO);

            // Assert
            Assert.True(isValid);
        }
    }
}
