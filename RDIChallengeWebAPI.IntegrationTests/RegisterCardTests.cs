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
    /// Classe de teste de integração da API 1: registro de cartões.
    /// </summary>
    public partial class RegisterCardTests : IClassFixture<WebApplicationFactory<Startup>>
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
        /// Construtor da classe.
        /// </summary>
        /// <param name="application">Aplicação WebAPI.</param>
        public RegisterCardTests(WebApplicationFactory<Startup> application)
        {
            _client = application.CreateClient();
        }

        /// <summary>
        /// Método para verificar se ao criar objetos com sucesso, o número gerado como card id será sequencial. 
        /// </summary>
        /// <param name="numberOfTests">Quantidade de vezes para fazer o teste.</param>
        [Theory]
        [InlineData(10)]
        public async Task RegisterAPI_NewCardNumber_DifferentCustomerCards_ShouldCreateSequentialCardId(int numberOfTests)
        {
            // Arrange
            CustomerCardDTO customerCardDTO = new CustomerCardDTO();
            long defaultCardNumber = 123456789012;
            bool first = true; // Flag para verificar se é a primeira vez que armazena o cardID.
            int expectedCardId = 0; // Armazena qual o proximo CardID esperado. 

            // Act & assert
            for (int cardId = 1; cardId <= numberOfTests; cardId++)
            {
                customerCardDTO.CustomerId = cardId;
                customerCardDTO.CardNumber = defaultCardNumber + cardId;

                var httpResponse = await _client.PostAsJson(API_REGISTER_ADDRESS, customerCardDTO);

                var token = JsonConvert.DeserializeObject<CardRegistrationResultDTO>
                                    (await httpResponse.Content.ReadAsStringAsync());

                expectedCardId = first ? token.CardId : ++expectedCardId;
                first = false;

                // Assert
                Assert.Equal(expected: System.Net.HttpStatusCode.OK, actual: httpResponse.StatusCode);
                Assert.Equal(expected: expectedCardId, token.CardId);
                Assert.NotNull(token);
            }
        }

        /// <summary>
        /// Método para verificar se ao criar o mesmo objeto na requisição, irá retornar uma mensagem de conflito (409).
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegisterAPI_NewCardNumber_SameRegistry_ShouldReturnError409Conflict()
        {
            // Arrange
            CustomerCardDTO customerCardDTO = new CustomerCardDTO() { CustomerId = 9999, CardNumber = 9999 };

            // Act
            var httpResponse1 = await _client.PostAsJson(API_REGISTER_ADDRESS, customerCardDTO);
            var httpResponse2 = await _client.PostAsJson(API_REGISTER_ADDRESS, customerCardDTO);

            // Assert
            Assert.Equal(expected: System.Net.HttpStatusCode.OK, actual: httpResponse1.StatusCode);
            Assert.Equal(expected: System.Net.HttpStatusCode.Conflict, actual: httpResponse2.StatusCode);
        }

        /// <summary>
        /// Método para verificar se ao enviar um objeto inválido, irá retornar o error 400 (Bad Request).
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegisterAPI_NewCardNumber_InvalidJson_ShouldRetorn400BadRequest()
        { 
            // Act
            var httpResponse = await _client.PostAsJson(API_REGISTER_ADDRESS, "invalid data");

            // Assert
            Assert.Equal(expected: System.Net.HttpStatusCode.BadRequest, actual: httpResponse.StatusCode);
        }

    }
}
