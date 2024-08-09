namespace bayes
{
    public class Data : IData
    {
        #region Public properties
        /// <summary>
        /// The name of the value
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The actual value
        /// </summary>
        public int Value { get; set; }
        #endregion // Public properties

        #region Contructor
        /// <summary>
        /// Default contructor
        /// </summary>
        public Data() 
            : this(dataLine: string.Empty)
        { }
        /// <summary>
        /// Contructors
        /// </summary>
        /// <param name="dataLine"></param>
        public Data(string dataLine)
        {
            string[] strings = dataLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Name = strings[0];
            Value = int.Parse(strings[1]);
        }

        #endregion // Constructors

        #region IEquatable implementation
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other">The data we compare to</param>
        /// <returns>Whether the two are equal</returns>
        public bool Equals(IData? other)
        {
            if (this.Name == other?.Name)
            {
                return (this.Value == other?.Value);
            }
            return false;
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">The left side of the operation</param>
        /// <param name="right">The right side of the operation</param>
        /// <returns>Whether the two are equal</returns>
        public static bool operator == (Data left, IData right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">The left side of the operation</param>
        /// <param name="right">The right side of the operation</param>
        /// <returns>Whether the two are equal</returns>
        public static bool operator != (Data left, IData right)
        {
            return !left.Equals(right);
        }
        #endregion // IEquatable implementation
    }
}
