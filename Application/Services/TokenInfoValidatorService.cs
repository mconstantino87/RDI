using Data.Repository;
using Domain.DTOs;
using Domain.Entities;
using RDIChallengeWebAPI.Model;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Classe responsável por realizar a validação de um gerado pelo sistema da RDI. 
    /// </summary>
    public class TokenInfoValidatorService : ITokenInfoValidatorService
    {
        /// <summary>
        /// Tempo padrão de expiração do token.
        /// </summary>
        public const long DEFAULT_TIME_TO_EXPIRATE_TOKEN = 30;

        /// <summary>
        /// Tempo de validação do token.
        /// </summary>
        private long _tokenValidationTime;

        /// <summary>
        /// Repositório com as informações do token.
        /// </summary>
        private IUserTokenInfoRepository _userTokenInfoRepository;

        /// <summary>
        /// Repositório com informações dos usuários cadastrados.
        /// </summary>
        private ICustomerCardRepository _customerCardRepository;

        /// <summary>
        /// Gerador de token.
        /// </summary>
        private ITokenCreatorService<CustomerCardEntity> _tokenCreator;

        /// <summary>
        /// Propriedade para gerenciar o tempo de validação do token.
        /// </summary>
        public long TokenValidationTime
        {
            get
            {
                return _tokenValidationTime;
            }
            set
            {
                if (value < 1) { throw new ArgumentException("Invalid token validation time. Expected minimum of 1."); }
                _tokenValidationTime = value;
            }
        }

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        public TokenInfoValidatorService(IUserTokenInfoRepository tokenInfoRepository, 
                                         ICustomerCardRepository customerCardRepository,
                                         ITokenCreatorService<CustomerCardEntity> tokenCreator)
        {
            _tokenValidationTime = DEFAULT_TIME_TO_EXPIRATE_TOKEN;

            _userTokenInfoRepository = tokenInfoRepository;
            _customerCardRepository = customerCardRepository;
            _tokenCreator = tokenCreator;
        }

        /// <summary>
        /// Método para verificar se o token é válido.
        /// </summary>
        /// <param name="token">Token a ser verificado.</param>
        /// <returns>True se o token for válido, false se não for.</returns>
        public async Task<bool> IsValidAsync(TokenValidationDTO token)
        {
            bool result = false;

            UserTokenInfo tokenInfo = _userTokenInfoRepository.GetUserTokenInfoById(token.CustomerId);
            if (tokenInfo != null)
            {
                result = !IsExpired(tokenInfo) && 
                    await CheckCardNumber(token) && 
                    await CheckToken(token);
            }

            return result;
        }

        /// <summary>
        /// Método para checar se o token está expirado.
        /// </summary>
        /// <param name="tokenInfo">Informações sobre o token.</param>
        /// <returns>True se o token está expirado, false se não estiver.</returns>
        private bool IsExpired(UserTokenInfo tokenInfo)
        {
            return (DateTime.UtcNow - tokenInfo.ExpirationDate).TotalMinutes 
                                                    >= _tokenValidationTime;
        }

        /// <summary>
        /// Método para checar se o cliente é o dono do cartão.  
        /// </summary>
        /// <param name="token">Informações sobre o token.</param>
        /// <returns>True se o cliente for o dono do cartão, false se não for.</returns>
        private async Task<bool> CheckCardNumber(TokenValidationDTO token)
        {
            bool result = false;
            UserTokenInfo tokenInfo = _userTokenInfoRepository.GeUserTokenInfoByCardId(token.CardId);
            if(tokenInfo != null)
            {
                var customerCard = await _customerCardRepository.GetCustomerCardByCustomerIdAsync(tokenInfo.CustomerId); // obtem infos, (como cardnumber)
                if (customerCard != null)
                {
                    result = (customerCard.CustomerId == token.CustomerId);
                    if (result) Console.WriteLine("Card Number: " + customerCard.CardNumber); // Requisito - Imprimir numero do cartão no console.
                }
            }
            return result;
        }

        /// <summary>
        /// Método para checar se o cliente é o dono do cartão.  
        /// </summary>
        /// <param name="token">Informações sobre o token.</param>
        /// <returns>True se o token for válido, false se não for</returns>
        private async Task<bool> CheckToken(TokenValidationDTO token)
        {
            bool result = false;
            UserTokenInfo tokenInfo = _userTokenInfoRepository.GeUserTokenInfoByCardId(token.CardId);
            if (tokenInfo != null)
            {
                var customerCard = await _customerCardRepository.GetCustomerCardByCustomerIdAsync(tokenInfo.CustomerId); // obtem infos, (como cardnumber)
                if (customerCard != null)
                {
                    customerCard.CVV = token.CVV; // Ajusta CVV para calculo do token.
                    var calculatedToken = _tokenCreator.CreateToken(customerCard);
                    result = (calculatedToken == token.Token);
                }
            }
            return result;
        }

    }
}
