namespace Library.Graph.Generators
{
    public interface IGraphViewGenerator<TValue>
        where TValue : notnull
    {
        ViewGeneratingResult<TValue> Generate();
    }
}
