using Microsoft.AspNetCore.Builder;
using RDIChallengeWebAPI.Middlewares;

namespace RDIChallengeWebAPI.Extensions
{
    /// <summary>
    /// Classe responsável por fornecer métodos para tratamentos de erros nos serviços.
    /// </summary>
    public static class HandlingExceptionExtensions
    {
        /// <summary>
        /// Método que configura o tratamento de exceções no serviço da WebApi.
        /// </summary>
        /// <param name="app">Instância do objeto para configuração do serviço de erros na pipeline.</param>
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
