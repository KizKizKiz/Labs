namespace ConsoleApp.Graph
{
    public readonly ref struct EdgeWithWeight<T>
    {
        public T First { get; }
        public T Second { get; }
        public double Weight { get; }
        public EdgeWithWeight(T first, T second, double weight)
        {
            First = first;
            Second = second;
            Weight = weight;
        }
    }
}