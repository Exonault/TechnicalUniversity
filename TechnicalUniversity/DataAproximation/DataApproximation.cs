using System;
using System.IO;
using System.Linq;
using DataAproximation;

namespace Exercises
{
    public class DataApproximation
    {
        private const string filePath =
            @"C:\Users\k.krachmarov\source\repos\Git\TechnicalUniversity-Semester1\TechnicalUniversity\DataAproximation\Files\";

        public static void Run()
        {
            var data = File.ReadAllLines(filePath + "deals.csv")
                .Skip(1)
                .Select(line => line.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(num => double.Parse(num))
                    .ToArray())
                .ToArray();

            var i = 1;
            Console.WriteLine("Deals:");
            Console.WriteLine($"Area\tDeals age\tBuild age\tPrice");
            Console.WriteLine(
                data.Select(s => $"{i++}: {s[0]}\t{s[1]}\t{s[2]}\t{s[3]}")
                    .Aggregate((f, s) => f + Environment.NewLine + s));

            var xs = data
                .Select(s => new double[] {1, s[0], s[1], s[2]})
                .ToList();

            var ys = data
                .Select(s => s[3])
                .ToList();

            var r = new LinearRegression();
            r.GradientDescentTrain(0.00001, 2000000, ys, xs);

            i = 1;
            Console.WriteLine("Deals:");
            Console.WriteLine($"Area\tDeals age\tBuild age\tPrice");
            Console.WriteLine(
                data.Select(s =>
                        $"{i++}: {s[0]}\t{s[1]}\t{s[2]}\t{s[3]}\t{r.Hipotesys(new double[] {1, s[0], s[1], s[2]}):###0.0}")
                    .Aggregate((f, s) => f + Environment.NewLine + s));


            var offers = File.ReadAllLines(filePath + "offers.csv")
                .Skip(1)
                .Select(line => line.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(num => double.Parse(num))
                    .ToArray())
                .Select(s => new double[]
                {
                    s[0],
                    s[1],
                    s[2],
                    r.Hipotesys(new double[] {1, s[0], 0, s[2]}), //fair price
                    r.Hipotesys(new double[] {1, s[0], -1, s[2]}) //expected price 
                })
                .ToArray();

            i = 1;
            Console.WriteLine("Offers:");
            Console.WriteLine($"Area\tBuild age\tPrice\tFair Price\tExpected price");
            Console.WriteLine(
                offers.Select(s =>
                        $"{i++}: {s[0]}\t{s[1]}\t{s[2]}\t{s[3]:###0.0}\t{s[4]:###0.0}")
                    .Aggregate((f, s) => f + Environment.NewLine + s));

            Console.WriteLine();
            Console.WriteLine($"Offers total price: {data.Sum(p => p[2])}");

            string solution;
            Knapsack01.KnapsackRec(
                offers.Select(s => s[2]).ToArray(),
                offers.Select(s => s[4] - s[2]).ToArray(),
                offers.Length,
                500,
                out solution);

            Console.WriteLine(solution);
        }
    }
}