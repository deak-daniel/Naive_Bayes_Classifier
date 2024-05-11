namespace bayes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Reading and splitting dataset.
            List<string> input = File.ReadAllLines("Naive-Bayes-Classification-Data.csv").Skip(1).Select(x => x).ToList();
            List<string> train = new List<string>();
            for (int i = 0; i < (int)(input.Count * 0.8); i++)
            {
                train.Add(input[i]);
            }

            List<string> test = new List<string>();
            for (int i = (int)(input.Count * 0.8) + 1; i < input.Count; i++)
            {
                test.Add( $"{input[i].Split(',')[0]},{input[i].Split(',')[1]}");
            }
            #endregion

            NaiveBayes bayes = new NaiveBayes(train);
            for (int i = 0; i < test.Count; i++)
            {
                Console.WriteLine(bayes.Predict(test[i]));
            }
        }
    }
}
