namespace Application.Services
{
    /// <summary>
    /// Interface responsável pelo contrato para criação de Token.
    /// </summary>
    /// <typeparam name="T">Tipo de dado para gerar o token.</typeparam>
    public interface ITokenCreatorService<T>
    {
        /// <summary>
        /// Método para gerar o token.
        /// </summary>
        /// <param name="data">Dados do token a serem gerados.</param>
        /// <returns>Identificação do token.</returns>
        long CreateToken(T data);
    }
}
