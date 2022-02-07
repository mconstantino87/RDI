using Application.Exceptions;
using AutoMapper;
using Data.Repository;
using Domain.DTOs;
using Domain.Entities;
using RDIChallengeWebAPI.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Classe para registar um novo cartão no sistema.
    /// </summary>
    public class CardRegisterService : ICardRegisterService
    {
        /// <summary>
        /// Identificador sequencial do cartão usado nas respostas.
        /// </summary>
        private static int _sequentialCardId = 0;

        /// <summary>
        /// Semáforo para sincronismo da criação do cartão.
        /// </summary>
        private static SemaphoreSlim _semaphore;

        /// <summary>
        /// Serviço responsável por criar o token.
        /// </summary>
        private ITokenCreatorService<CustomerCardEntity> _tokenCreator;

        /// <summary>
        /// Repositório para salvar as informações do cartão.
        /// </summary>
        private ICustomerCardRepository _customerCardRepository;

        /// <summary>
        /// Repositório para salvar as informações do token gerado.
        /// </summary>
        private IUserTokenInfoRepository _userTokenInfoRepository;

        /// <summary>
        /// Mapeador entre entidade e DTO de informações do cartão.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Construtor estático
        /// </summary>
        static CardRegisterService()
        {
            _semaphore = new SemaphoreSlim(1);
        }

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="customerCardRepository">Repositório para salvar as informações do cartão.</param>
        /// <param name="userTokenInfoRepository">IUserTokenInfoRepository</param>
        /// <param name="tokenCreator">Serviço responsável por criar o token.</param>
        /// <param name="mapper">Mapeador entre entidade e DTO de informações do cartão.</param>
        public CardRegisterService(ICustomerCardRepository customerCardRepository,
                                    IUserTokenInfoRepository userTokenInfoRepository,
                                    ITokenCreatorService<CustomerCardEntity> tokenCreator,
                                    IMapper mapper)
        {
            _mapper = mapper;
            _tokenCreator = tokenCreator;
            _customerCardRepository = customerCardRepository;
            _userTokenInfoRepository = userTokenInfoRepository;
        }
        /// <summary>
        /// Método para cadastrar um novo cartão no sistema.
        /// </summary>
        /// <param name="card">Informações do cartão cadastrado.</param>
        /// <returns>Dados de registro do cartão para se obter o token posteriormente.</returns>
        /// <exception cref=ItemsAlreadyExistsException">Uma ou mais informações únicas do cartão já estão sendo utilizadas.</exception>
        public async Task<CardRegistrationResultDTO> RegisterUserAsync(CustomerCardDTO card)
        {
            var entity = _mapper.Map<CustomerCardEntity>(card);

            try
            {
                await _semaphore.WaitAsync();
                await CheckInfoAlreadyExists(entity);
                await _customerCardRepository.CreateAsync(entity);

                CardRegistrationResultDTO result = new CardRegistrationResultDTO();
                result.RegistrationDate = DateTime.UtcNow;
                result.CardId = Interlocked.Increment(ref _sequentialCardId);
                result.Token = _tokenCreator.CreateToken(entity);

                UserTokenInfo tokenInfo = new UserTokenInfo(card.CustomerId, result.CardId, result.RegistrationDate, result.Token);
                _userTokenInfoRepository.AddUserTokenInfo(tokenInfo);
                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Método para checar se alguma informação única do cartão está em uso e lançar uma exceção informando os detalhes do erro.
        /// </summary>
        /// <param name="card">Informações do cartão cadastrado.</param>
        /// <returns></returns>
        private async Task CheckInfoAlreadyExists(CustomerCardEntity card)
        {
            bool userExists = await _customerCardRepository.GetCustomerCardByCustomerIdAsync(card.CustomerId) != null;
            bool cardNumberExists = await _customerCardRepository.GetCustomerCardByCardNumberAsync(card.CardNumber) != null;

            if (userExists || cardNumberExists)
            {
                ItemsAlreadyExistsException exception = new ItemsAlreadyExistsException("Some items already exists in database.");
                exception.AddErrorInCondition(userExists, $"The card with {nameof(card.CustomerId)} {card.CustomerId} already exists.");
                exception.AddErrorInCondition(cardNumberExists, $"The Card Number {card.CardNumber} already exists.");
                throw exception;
            }
        }
    }
}
