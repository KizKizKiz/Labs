namespace ConsoleApp.Graph
{
    public readonly struct Edge<T>
    {
        public T First { get; }
        public T Second { get; }
        public Edge(T first, T second)
        {
            First = first;
            Second = second;
        }
    }
}