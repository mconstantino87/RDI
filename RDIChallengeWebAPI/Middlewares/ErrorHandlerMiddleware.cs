using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RDIChallengeWebAPI.Middlewares
{
    /// <summary>
    /// Classe responsável pelo tratamento de exceptions nas requisições.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        /// <summary>
        /// Proxima requisição do middleware.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="next">Proxima requisição do middleware.</param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Método responsável pelo tratamento de erros nas requisições.
        /// </summary>
        /// <param name="context">Contexto http para resposta das requisições.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                List<string> errors;
                var httpResponse = context.Response;

                switch (exception)
                {
                    case ItemsAlreadyExistsException e:
                        errors = new List<string>(e.Items);
                        httpResponse.StatusCode = (int)HttpStatusCode.Conflict;
                        break;

                    default:
                        errors = new List<string>() { exception.Message };
                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonConvert.SerializeObject(new { Errors = errors });
                context.Response.ContentType = "application/json";
                await httpResponse.WriteAsync(result);
            }
        }
    }
}
