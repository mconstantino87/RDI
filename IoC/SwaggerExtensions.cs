using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace IoC
{
    /// <summary>
    /// Classe responsável pelas extensões do Swagger.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Método para adicionar o Swagger em um container IoC.
        /// </summary>
        /// <param name="services">Serviço para adicionar o container.</param>
        /// <returns>Serviço com o container do AutoMapper adicionado.</returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RDI Challenge API",
                    Description = "Desafio proposto pela RDI",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "RDI Software",
                        Email = "contact@rdi.com",
                        Url = new Uri("https://www.rdisoftware.com/")
                    }
                });

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
