namespace Library.Graph.ImportersExporters
{
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

        bool IsNotDefaultValue { get; }
    }
}