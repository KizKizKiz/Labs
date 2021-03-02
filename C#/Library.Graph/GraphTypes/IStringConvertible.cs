namespace Library.GraphTypes
{
    public interface IStringConvertible<T>
    {
        T ConvertFromString(string entity);
    }
}