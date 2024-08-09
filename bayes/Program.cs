namespace bayes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Reading and splitting dataset.
            List<string> input = File.ReadAllLines("C:\\Users\\deakd\\Documents\\GitHub\\Naive_Bayes_Classifier\\bayes\\dataset.csv").Select(x => x).ToList();
            List<string> train = new List<string>();
            for (int i = 0; i < (int)(input.Count); i++)
            {
                train.Add(input[i]);
            }
            #endregion

            NaiveBayes bayes = new NaiveBayes(train);
            for (int i = 0; i < bayes.Test.Count; i++)
            {
                var result = bayes.Predict(bayes.Test[i].ValueToString());
                Console.WriteLine($"The probability of yes: {result.Item1:p2}, the probability of no: {result.Item2:p2}");
            }

        }
    }
}
