namespace Library.Graph
{
    public interface IRandomizer
    {
        int FromRange(int min, int max);
        int FromRange(int max);
    }
}