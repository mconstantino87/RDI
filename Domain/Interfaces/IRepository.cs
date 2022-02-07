using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Repositório genérico de dados.
    /// </summary>
    /// <typeparam name="T">Tipo da informação a ser manipulada pelo repositório.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Método para criar um novo registro no repositório.
        /// </summary>
        /// <param name="data">Dado a ser criado.</param>
        /// <returns></returns>
        Task CreateAsync(T data);

        /// <summary>
        /// Método para retornar todos os registros.
        /// </summary>
        /// <returns>Coleção com todos os registros do repositório.</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
