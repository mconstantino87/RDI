using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RDIChallengeWebAPI.Controllers
{
    /// <summary>
    /// Constroller que representa o desafio 2, responsável pela validação do token.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateTokenController : ControllerBase
    {
        /// <summary>
        /// Serviço para validação do token.
        /// </summary>
        private readonly ITokenInfoValidatorService _tokenValidator;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="tokenInfoValidator">Serviço para validação do token.</param>
        public ValidateTokenController(ITokenInfoValidatorService tokenInfoValidator)
        {
            _tokenValidator = tokenInfoValidator;
        }

        /// <summary>
        /// Método para validar um token com as informações enviadas pelo usuário. 
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// 
        ///     POST /api/ValidateToken
        ///     {
        ///         "customerId": 12345,
        ///         "cardId": 123456789012,
        ///         "token": 4781
        ///         "cvv": 1234
        ///     }  
        ///     
        /// Os seguintes valores são permitidos.
        /// 
        ///     customerId - valor inteiro.
        ///     cardId - valor inteiro, identificador retornado na requisição /api/Register
        ///     token - valor inteiro, identificador retornado na requisição /api/Register
        ///     CVV - valor inteiro entre 1 e 5 dígitos.
        /// </remarks>
        /// <param name="token">Token a ser validado.</param>
        /// <returns>Resultado da validação do token.</returns>
        /// <response code="200">Chamada a API OK, observar o retorno true ou false.</response>
        /// <response code="400">Falha na requisição.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TokenValidationDTO token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            bool isValid = await _tokenValidator.IsValidAsync(token);
            return Ok(isValid);
        }
    }
}
