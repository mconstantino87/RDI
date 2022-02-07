using Domain.DTOs;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Inteface responsável pelo serviço de validação do token.
    /// </summary>
    public interface ITokenInfoValidatorService
    {
        /// <summary>
        /// Validade do Token, em minutos
        /// </summary>
        long TokenValidationTime { get; set; }

        /// <summary>
        /// Método responsável por validar um token com as informações recebidas do usuário.
        /// </summary>
        /// <param name="token">Informações recebidas do usuário.</param>
        /// <returns>True caso o token seja válido, false se for inválido.</returns>
        Task<bool> IsValidAsync(TokenValidationDTO token);
    }
}
