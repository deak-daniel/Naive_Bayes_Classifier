namespace bayes
{
    /// <summary>
    /// Public interface representing a data
    /// </summary>
    public interface IData : IEquatable<IData>
    {
        /// <summary>
        /// The name of the data
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The actual value of the data
        /// </summary>
        int Value { get; }
    }
}
