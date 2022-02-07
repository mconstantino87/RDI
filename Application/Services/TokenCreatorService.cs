using Domain.Entities;
using RDIChallengeWebAPI.Utils;
using System;

namespace Application.Services
{
    /// <summary>
    /// Classe responsável pela criação de um token, através dos dados de cartão criados.
    /// </summary>
    public class TokenCreatorService: ITokenCreatorService<CustomerCardEntity>
    {
        /// <summary>
        /// Método para gerar o número do token.
        /// </summary>
        /// <param name="data">Dados do cartão para gerar o token.</param>
        /// <returns>Novo token.</returns>
        public long CreateToken(CustomerCardEntity data)
        {
            string strCardNumber = data.CardNumber.ToString().PadLeft(16, '0');

            int indexDigitsUsedToken = strCardNumber.Length - 4;
            char[] lastDigits = new char[4];

            strCardNumber.CopyTo(indexDigitsUsedToken, lastDigits, 0, 4);
            ArrayUtils.ArrayRotate(lastDigits, data.CVV);
            strCardNumber = new string(lastDigits);
            return Convert.ToInt64(strCardNumber.ToString());
        }
    }
}
