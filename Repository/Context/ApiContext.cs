using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    /// <summary>
    /// Contexto do Entity Framework para salvar as informações da API.
    /// </summary>
    public class ApiContext : DbContext
    {
        /// <summary>
        /// Construtro da classe.
        /// </summary>
        /// <param name="options">Informações de contexto do banco.</param>
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        /// <summary>
        /// Coleção de dados dos cartões dos clientes.
        /// </summary>
        public DbSet<CustomerCardEntity> CustomerCards { get; set; }
    }
}
