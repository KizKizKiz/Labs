namespace Library.Graph.ImportersExporters
{
    /// <summary>
    /// Представляет контракт для конвертации в тип <typeparamref name="T"/> из строкового представления.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStringConvertible<T>
    {
        /// <summary>
        /// Возвращает объект типа <typeparamref name="T"/>, полученный из <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="FormatException">
        /// Вызывается в случае неудачной попытки конвертации.
        /// </exception>
        T ConvertFromString(string entity);
    }
}