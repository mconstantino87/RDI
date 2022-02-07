using System;

namespace RDIChallengeWebAPI.Utils
{
    /// <summary>
    /// Classe utilitária para operações em array.
    /// </summary>
    public static class ArrayUtils
    {
        /// <summary>
        /// Método para rotacionar o array de forma circular a direita.
        /// </summary>
        /// <typeparam name="T">Tipo do array a ser rotacionado.</typeparam>
        /// <param name="array">Array a ser rotacionado.</param>
        /// <returns>Array modificado após realizar as rotações necessárias.</returns>
        public static void ArrayRotate<T>(T[] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
            {
                var previous = array[0];
                for (var i = 0; i < array.Length - 1; i++)
                {
                    var next = array[i + 1];
                    array[i + 1] = previous;
                    previous = next;
                }
                array[0] = previous;
            }
        }

        /// <summary>
        /// Método para rotacionar o array de forma circular a direita um determinado número de vezes.
        /// </summary>
        /// <typeparam name="T">Tipo do array a ser rotacionado.</typeparam>
        /// <param name="array">Array a ser rotacionado.</param>
        /// <param name="k">Número de vezes para realizar a rotação.</param>
        /// <returns>Array modificado após realizar as rotações necessárias.</returns>
        public static void ArrayRotate<T>(T[] array, int k)
        {
            for (int i = 0; i < k; i++)
            {
                ArrayRotate(array);
            }
        }
    }
}
