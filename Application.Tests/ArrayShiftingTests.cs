using RDIChallengeWebAPI.Utils;
using Xunit;

namespace Application.Tests
{
    /// <summary>
    /// Classe para testes de deslocamento do array, usado no algoritmo do token.
    /// </summary>
    public class ArrayShiftingTests
    {
        /// <summary>
        /// Teste que valida o vetor, após resultar o número de deslocamentos desejados.
        /// </summary>
        /// <param name="dataToShift">Vetor com os valores para serem deslocados.</param>
        /// <param name="result">Resultado esperado após o deslocamento</param>
        /// <param name="shiftTimes">Quantidade de vezes para deslocar o vetor.</param>
        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, 0)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3, 1, 2 }, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 2, 3, 1 }, 2)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, 3)]

        [InlineData(new int[] { 3, 4, 5, 6 }, new int[] { 3, 4, 5, 6 }, 0)]
        [InlineData(new int[] { 3, 4, 5, 6 }, new int[] { 6, 3, 4, 5 }, 1)]
        [InlineData(new int[] { 3, 4, 5, 6 }, new int[] { 5, 6, 3, 4 }, 2)]
        [InlineData(new int[] { 3, 4, 5, 6 }, new int[] { 4, 5, 6, 3 }, 3)]
        [InlineData(new int[] { 3, 4, 5, 6 }, new int[] { 3, 4, 5, 6 }, 4)]
        public void ArrayRotate_RotatingMultipleTimes_ShouldRotateCorrectly(int[] dataToShift, int[] result, int shiftTimes)
        {
            ArrayUtils.ArrayRotate(dataToShift, shiftTimes);
            Assert.Equal(expected: result, actual:dataToShift);
        }
    }
}
