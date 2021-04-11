namespace Library.Graph.Generators.Options
{
    /// <summary>
    /// Тип связности графа.
    /// </summary>
    public enum ConnectivityType
    {
        /// <summary>
        /// Не связный.
        /// </summary>
        NotConnected,

        /// <summary>
        /// Слабосвязный для ориентированного графа и связный для неоринтированного.
        /// </summary>
        WeaklyOrJustConnected,

        /// <summary>
        /// Сильносвязный для ориентированного графа.
        /// </summary>
        StronglyConnected
    }
}