namespace Library.Graph.ConvertibleTypes
{
    public interface IStringConvertible<T>
    {
        T ConvertFromString(string entity);
    }
}