using Domain.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RDIChallengeWebAPI.IntegrationTests.Extensions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RDIChallengeWebAPI.IntegrationTests
{
    /// <summary>
    /// Classe de teste de integração das APIS 1 e 2 (registro de cartão + validação de Token)
    /// </summary>
    public class ValidateTokenTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        /// <summary>
        /// Cliente http usado nas requisições.
        /// </summary>
        private readonly HttpClient _client;


        /// <summary>
        /// Endereço para fazer requisições na API de registro.
        /// </summary>
        private const string API_REGISTER_ADDRESS = "api/register";

        /// <summary>
        /// Endereço para fazer requisições na API de registro.
        /// </summary>
        private const string API_VALIDATE_TOKEN_ADDRESS = "api/ValidateToken";

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="application">Aplicação WebAPI.</param>
        public ValidateTokenTests(WebApplicationFactory<Startup> application)
        {
            _client = application.CreateClient();
        }

        /// <summary>
        /// Método para verificar se ao enviar uma requisição do token com dados na validade, retorna OK.
        /// Faz o cadastro na API e na sequencia verifica se o token é válido.
        /// </summary>
        /// <returns></returns>
        [Theory]
        [InlineData(1, 123456789011, 1)]
        [InlineData(2, 123456789012, 12)]
        [InlineData(3, 123456789013, 123)]
        [InlineData(4, 123456789014, 1234)]
        [InlineData(5, 123456789015, 12345)]
        [InlineData(6, 123, 12345)]
        [InlineData(7, 0, 12345)]
        [InlineData(8, 12345, 12345)]
        public async Task ValidateToken_WithAllDataValid_ShouldReturnOK(int customerId, long CardNumber, int CVV)
        {
            // Arrange
            CustomerCardDTO customerCardDTO = new CustomerCardDTO();
            customerCardDTO.CustomerId = customerId;
            customerCardDTO.CardNumber = CardNumber;
            customerCardDTO.CVV = CVV;

            // Act
            var httpResponse = await _client.PostAsJson(API_REGISTER_ADDRESS, customerCardDTO);

            var token = JsonConvert.DeserializeObject<CardRegistrationResultDTO>
                                (await httpResponse.Content.ReadAsStringAsync());

            TokenValidationDTO tokenValidation = new TokenValidationDTO();
            tokenValidation.Token = token.Token;
            tokenValidation.CardId = token.CardId;
            tokenValidation.CustomerId = customerCardDTO.CustomerId;
            tokenValidation.CVV = customerCardDTO.CVV;

            httpResponse = await _client.PostAsJson(API_VALIDATE_TOKEN_ADDRESS, tokenValidation);

            bool isValid = bool.Parse(await httpResponse.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(expected: System.Net.HttpStatusCode.OK, actual: httpResponse.StatusCode);
            Assert.True(isValid);
        }
    }
}
