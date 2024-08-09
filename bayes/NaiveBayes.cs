using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace bayes
{
    /// <summary>
    /// An enum representing the prediction value
    /// </summary>
    public enum PredictionValue
    {
        No = 0,
        Yes = 1
    }
    /// <summary>
    /// Naive Bayes binary classification model.
    /// </summary>
    public class NaiveBayes
    {
        #region Private fields
        private List<string> labels = new List<string>();
        private double ProbOfNo = 0;
        private double ProbOfYes = 0;
        private double CountYes = 0;
        private double CountNo = 0;
        List<NaiveBayesData> test = new List<NaiveBayesData>();
        List<NaiveBayesData> train = new List<NaiveBayesData>();
        #endregion // Private fields

        #region Public properties
        /// <summary>
        /// List of <seealso cref="NaiveBayesData"/>, contains the training data.
        /// </summary>
        public List<NaiveBayesData> Data { get; private set; }
        /// <summary>
        /// Readonly property for the test entries
        /// </summary>
        public List<NaiveBayesData> Test 
        {
            get { return test; } 
        }
        #endregion // Public properties

        #region Internal class
        /// <summary>
        /// Class representing an entry of data. The reason that this class is inside of <see cref="NaiveBayes"/> 
        /// is because this class cannot exist without the NaiveBayes class
        /// </summary>
        public class NaiveBayesData : List<IData>, IEquatable<NaiveBayesData>
        {
            #region Properties
            /// <summary>
            /// A property that gives back the labels
            /// </summary>
            public List<string> Labels { get; private set; }
            #endregion // Properties

            #region Contructors
            /// <summary>
            /// Default contructor
            /// </summary>
            public NaiveBayesData()
            {
                Labels = new List<string>();
            }
            /// <summary>
            /// Contructor.
            /// </summary>
            /// <param name="dataset">The path of the dataset.</param>
            public NaiveBayesData(List<string> labels, List<string> data)
            {
                string line = "";
                Labels = labels;
                // The last value should be the one that we want to predict
                for (int i = 0; i < data.Count; i++)
                {
                    line += $"{labels[i]},{data[i]}";
                    this.Add(new Data(line));
                    line = "";
                }
            }
            #endregion // Contructor

            #region IEquatable implementation
            /// <summary>
            /// Decides whether this object's <see cref="Data"/> is equal to any of the other object's <see cref="Data"/>
            /// </summary>
            /// <param name="other">The other object we are comparing to</param>
            /// <returns>True if they are equal, false if not</returns>
            public bool Equals(NaiveBayesData? other)
            {
                List<bool> bools = new List<bool>(); 
                for (int i = 0; i < this.Count - 1; i++)
                {
                    bools.Add(this[i].Equals(other?[i]));
                }
                return bools.Any(x => x == true);
            }
            #endregion // IEquatable implementation
        }
        #endregion // Internal class

        #region Contructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataset">The whole dataset</param>
        /// <param name="testSplit">The percent value of how much should the test split be compared to the whole dataset. (The test and train split should sum up to 1)</param>
        /// <param name="trainSplit">The percent value of how much should the train split be compared to the whole dataset. (The test and train split should sum up to 1)</param>
        public NaiveBayes(List<string> dataset, double trainSplit = 0.8, double testSplit = 0.2)
        {
            if (trainSplit + testSplit == 1)
            {
                string[] labels = dataset[0].Split(',' , StringSplitOptions.RemoveEmptyEntries);
                this.labels = labels.ToList();
                Data = new List<NaiveBayesData>();
                for (int i = 1; i < dataset.Count; i++)
                {
                    Data.Add(new NaiveBayesData(labels.ToList(), dataset[i].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ));
                }
                train = Data.GetRange(0, (int)(Data.Count * trainSplit));
                test = Data.GetRange((int)(Data.Count * trainSplit), (int)(Data.Count * testSplit));
                List<IData> endValues = train.Select(x => x.Last()).ToList();

                CountNo = endValues.Count(x => x.Value == 0);
                CountYes = endValues.Count(x => x.Value == 1);
                ProbOfNo = (double)CountNo / Data.Count;
                ProbOfYes = (double)CountYes / Data.Count;
            }
            else
            {
                throw new Exception("The train and test split percentages are not correct");
            }
        }
        #endregion // Constructors

        #region Public methods
        /// <summary>
        /// Method usedd to make pediction based on the training data.
        /// </summary>
        /// <param name="data">The data which we want to evaluate.</param>
        /// <returns>The probability of each return type.</returns>
        public (double, double) Predict(string data)
        {
            string[] helper = data.Split(',', StringSplitOptions.RemoveEmptyEntries);
            NaiveBayesData inputData = new NaiveBayesData(labels.ToList(), helper.ToList());
            List<NaiveBayesData> givenYes = new List<NaiveBayesData>();
            List<NaiveBayesData> givenNo = new List<NaiveBayesData>();
            
            List<NaiveBayesData> subList = new List<NaiveBayesData>();
            subList = train.Where(x => x.Equals(inputData)).ToList();
            
            givenYes = subList.Where(x => x.Last().Value == (int)PredictionValue.Yes).ToList();
            givenNo = subList.Where(x => x.Last().Value == (int)PredictionValue.No).ToList();
            double yes = ProbOfYes * givenYes.Count / CountYes;
            double no = ProbOfNo * givenNo.Count / CountNo;
            double PercentYes = yes / (yes + no);
            double PercentNo = no / (yes + no);
            return (PercentYes, PercentNo);
        }
        #endregion // Public methods
    }
}

