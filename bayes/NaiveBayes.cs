using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayes
{
    /// <summary>
    /// Naive Bayes binary classification model.
    /// </summary>
    internal class NaiveBayes
    {
        #region Private fields
        private double ProbOfYes = 0;
        private double ProbOfNo = 0;
        private int CountNo = 0;
        private int CountYes = 0;

        #endregion // Private fields

        #region Public properties
        /// <summary>
        /// List of <seealso cref="NaiveBayesData"/>, contains the training data.
        /// </summary>
        public List<NaiveBayesData> Data { get; private set; }
        #endregion // Public properties

        #region Contructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataset"></param>
        public NaiveBayes(List<string> dataset)
        {
            Data = new List<NaiveBayesData>();
            for (int i = 0; i < dataset.Count; i++)
            {
                Data.Add(new NaiveBayesData(dataset[i]) );
            }

            CountNo = Data.Count(x => x.IsDiabetes == false);
            CountYes = Data.Count(x => x.IsDiabetes == true);
            ProbOfNo = (double)CountNo / Data.Count;
            ProbOfYes = (double)CountYes / Data.Count;
        }
        #endregion // Constructor

        #region Public methods
        /// <summary>
        /// Method usedd to make pediction based on the training data.
        /// </summary>
        /// <param name="data">The data which we want to evaluate.</param>
        /// <returns>The probability of each return type.</returns>
        public string Predict(string data)
        {
            string[] helper = data.Split(',');
            int glucose = int.Parse(helper[0]);
            int bloodPressure = int.Parse(helper[1]);

            double givenGlucose = Data.Count(x => x.Glucose == glucose && x.IsDiabetes == true);
            double givenBloodPressure = Data.Count(x => x.BloodPressure == bloodPressure && x.IsDiabetes == true);
            double yes = ProbOfYes * (double)(givenGlucose / CountYes) * (double)(givenBloodPressure / CountYes);

            givenGlucose = Data.Count(x => x.Glucose == glucose && x.IsDiabetes == false);
            givenBloodPressure = Data.Count(x => x.BloodPressure == bloodPressure && x.IsDiabetes == false);
            double no = ProbOfNo * (double)(givenGlucose / CountNo) * (double)(givenBloodPressure / CountNo);

            double PercentYes = yes / (yes + no);
            double PercentNo = no / (yes + no);
            return $"Given the data: {data}, the probability of diabetes is {PercentYes:p2} yes, and {PercentNo:p2} no.";

        }
        #endregion // Public methods
    }
    /// <summary>
    /// Class representing an entry of data.
    /// </summary>
    public class NaiveBayesData
    {
        #region Public properties
        public int Glucose { get; private set; }
        public int BloodPressure { get; private set; }
        public bool IsDiabetes { get; private set; }
        #endregion // Public properties

        #region Contructor
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="line">The line of the data.</param>
        public NaiveBayesData(string line)
        {
            string[] helper = line.Split(',');
            Glucose = int.Parse(helper[0]);
            BloodPressure = int.Parse(helper[1]);
            IsDiabetes = int.Parse(helper[2]) == 0 ? false : true;
        }
        #endregion // Contructor
    }
}

