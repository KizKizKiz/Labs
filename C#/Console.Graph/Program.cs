using System;
using System.Linq;
using System.Threading.Tasks;
using Library.GraphTypes;
using Library.Operations;

namespace Console.Graph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //UnorientedEdgeWithWeightGraph<IntConvertible> graph = await GenerateAndExportGraph();
            UnorientedEdgeWithWeightGraph<IntConvertible> graph = new UnorientedEdgeWithWeightGraph<IntConvertible>();
            await graph.ImportAsync("graph-dump-11-23-05.xlsx");
            var aa = new MinimumSpanningTreeIterator<UnorientedEdgeWithWeightGraph<IntConvertible>, IntConvertible>(graph);
            foreach (var item in aa)
            {
                System.Console.WriteLine(item);
            }
        }

        private static async Task<UnorientedEdgeWithWeightGraph<IntConvertible>> GenerateAndExportGraph()
        {
            var graph = UnorientedEdgeWithWeightGraph<IntConvertible>.GenerateWithWeakCohesion(10, 4, () => new IntConvertible(_rnd.Next(0, 30)));
            await graph.ExportAsync();
            return graph;
        }

        private static Random _rnd = new Random();
    }
    public struct StringConvertible : IStringConvertible<StringConvertible>, IEquatable<StringConvertible>, IComparable<StringConvertible>
    {
        public string Entity { get; private set; }

        public StringConvertible(string entity)
        {
            Entity = entity;
        }
        public StringConvertible ConvertFromString(string entity)
        {
            Entity = entity;
            return this;
        }

        public bool Equals(StringConvertible other)
        {
            return Entity.Equals(other.Entity);
        }

        public int CompareTo(StringConvertible other)
        {
            return Entity.CompareTo(other.Entity);
        }
        public override string ToString()
        {
            return Entity;
        }
    }
}