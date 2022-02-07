using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application.Exceptions
{
    /// <summary>
    /// Exception lançada quando itens de uma coleção já existirem, para informar por dados já registrados.
    /// </summary>
    public class ItemsAlreadyExistsException : Exception
    {
        /// <summary>
        /// Coleção de itens sem duplicação.
        /// </summary>
        private HashSet<string> _items;

        /// <summary>
        /// Coleção dos itens já registrado.
        /// </summary>
        public ReadOnlyCollection<string> Items => 
            new ReadOnlyCollection<string>(_items.ToList());

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        /// <param name="items">Itens duplicados na coleção.</param>
        public ItemsAlreadyExistsException(string message, params string[] items)
        {
            _items = new HashSet<string>();
            Array.ForEach(items, item => _items.Add(item));
        }

        /// <summary>
        /// Método para adicionar um erro caso uma condição específica seja satisfeita.
        /// </summary>
        /// <param name="condition">Condição para adicionar o item.</param>
        /// <param name="v">Item a ser adicionado.</param>
        public void AddErrorInCondition(bool condition, string v)
        {
            if(condition && !string.IsNullOrEmpty(v))
            {
                _items.Add(v);
            }
        }
    }
}
