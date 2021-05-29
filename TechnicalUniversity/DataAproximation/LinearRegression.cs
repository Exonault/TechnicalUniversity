using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAproximation
{
    public class LinearRegression
    {
        public double[] RegressionParameters { get; set; }

        public double Hipotesys(double[] xs)
        {
            double h = 0;
            for (int i = 0; i < xs.Length; i++)
            {
                h += xs[i] * RegressionParameters[i];
            }

            return h;
        }

        public void GradientDescentTrain(double learningRate, int iteration, List<double> ys, List<double[]> xs)
        {
            if (xs.Count == 0 || xs[0].Length == 0)
            {
                return;
            }

            RegressionParameters = new double[xs[0].Length];

            do
            {
                for (int k = 0; k < 1000; k++)
                {
                    var newRegressionParameters = RegressionParameters.ToArray();

                    for (int j = 0; j < RegressionParameters.Length; j++)
                    {
                        double error = 0;
                        for (int i = 0; i < xs.Count; i++)
                        {
                            error += (ys[i] - Hipotesys(xs[i])) * xs[i][j];
                        }

                        newRegressionParameters[j] = learningRate * error;
                    }

                    RegressionParameters = newRegressionParameters;
                }

                var parameters = RegressionParameters
                    .Select(s => s.ToString("#0.0000"))
                    .Aggregate((f, s) => f + "\t" + s);

                var z = 0;
                var averageError = xs
                    .Average(s => Math.Pow(ys[z++] - Hipotesys(s), 2));

                Console.WriteLine($"{iteration}: {parameters} \t Error:{averageError}");
            } while (iteration-- > 0);
        }
    }
}