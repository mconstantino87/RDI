using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RDIChallengeWebAPI.Controllers
{
    /// <summary>
    /// Constroller que representa o desafio 1, responsável pelo cadastro de novos cartões de usuário.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        /// <summary>
        /// Registro para o serviço de cartões.
        /// </summary>
        private readonly ICardRegisterService _userRegister;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="userRegister">Registro para o serviço de cartões.</param>
        public RegisterController(ICardRegisterService userRegister)
        {
            _userRegister = userRegister;
        }

        /// <summary>
        /// Método para realizar a inclusão de um novo cartão.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// 
        ///     POST /api/Register
        ///     {
        ///         "customerId": 12345,
        ///         "cardNumber": 123456789012,
        ///         "cvv": 1234
        ///     }  
        ///     
        /// Os seguintes valores são permitidos.
        /// 
        ///     customerId - valor inteiro.
        ///     cardNumber - valor inteiro entre 1 e 16 dígitos.
        ///     CVV - valor inteiro entre 1 e 5 dígitos.
        ///     
        /// Em caso de sucesso, será retornado um JSON semelhante a:
        /// 
        ///     {
        ///         "registrationDate": "2022-02-06T23:47:43.5182764Z",
        ///         "token": 1290,
        ///         "cardId": 1
        ///     }
        ///    
        /// Sendo:
        /// 
        ///     registrationDate - data de expiração do token
        ///     token - código do token a ser utilizado para validação na requisição em /api/ValidateToken
        ///     cardId - número do cartão gerado na requisição a ser utilizado para validação na requisição em /api/ValidateToken
        /// </remarks>
        /// <param name="customerCard">Novo cartão a ser incluído.</param>
        /// <returns>Resultado da criação de um novo cartão.</returns>
        /// <response code="200">Cartão cadastrado com sucesso.</response>
        /// <response code="400">Falha na requisição.</response>
        /// <response code="409">Conflito na inserção de dados.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerCardDTO customerCard)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userRegister.RegisterUserAsync(customerCard); // Erros são tratados na pipeline ErrorHandleMiddleware.
            return Ok(result);
        }
    }
}
