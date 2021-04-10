
using MathNet.Numerics.Distributions;

namespace Library.Graph.Generators
{
    public class DefaultDistributionCalculator : IDistributionCalculator
    {
        public DefaultDistributionCalculator(double lambda)
        {
            _lambda = lambda;
        }

        public double GetDistribution() => Poisson.Sample(_lambda);

        private readonly double _lambda;
    }
}
