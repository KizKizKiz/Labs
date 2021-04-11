namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет контракт вычисления распределения.
    /// </summary>
    public interface IDistributionCalculator
    {
        /// <summary>
        /// Возвращает распределение.
        /// </summary>
        double GetDistribution();
    }
}
