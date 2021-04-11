using Library.Graph.Types;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет контракт генерации графа.
    /// </summary>
    /// <typeparam name="TGraph">Тип графа.</typeparam>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public interface IGraphGenerator<TGraph, TValue>
        where TValue : notnull
        where TGraph : Graph<TValue>
    {
        /// <summary>
        /// Генерирует граф типа <typeparamref name="TGraph"/> и возвращает результат генерации.
        /// </summary>
        /// <returns>Результат генерации.</returns>
        GraphGeneratingResult<TGraph, TValue> Generate();
    }
}
