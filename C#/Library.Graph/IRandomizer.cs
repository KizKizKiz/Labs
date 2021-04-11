namespace Library.Graph
{
    /// <summary>
    /// Представляет контракт вычисления случайного числа.
    /// </summary>
    public interface IRandomizer
    {
        /// <summary>
        /// Возвращает число в диапазоне от <paramref name="min"/> до <paramref name="max"/>.
        /// </summary>
        int FromRange(int min, int max);

        /// <summary>
        /// Возвращает число в диапазоне от 0 до <paramref name="max"/>.
        /// </summary>
        int FromRange(int max);
    }
}