using System;
using System.Collections.Generic;
using System.Linq;

namespace HeuristicAlgorithms
{
    public class GeneticalAlgo
    {
        const int MUTATION_PERCENTAGE = 30;
        const string GENES = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890, .-;:_!\"#%&/()=?@${[]}";

        static char[] targetSolution;

        private static List<char[]> RandomPopulation(
            Random r,
            int count)
        {
            var result = new List<char[]>();

            for (int i = 0; i < count; i++)
            {
                char[] newSolution = new char[targetSolution.Length];

                for (int j = 0; j < newSolution.Length; j++)
                {
                    newSolution[j] = GENES[r.Next(GENES.Length)];
                }

                result.Add(newSolution);
            }

            return result;
        }

        private static char[] Mutate(
            Random r,
            char[] solution,
            int mutationPercentage)
        {
            var newSolution = new char[targetSolution.Length];

            for (int i = 0; i < newSolution.Length; i++)
            {
                newSolution[i] = r.Next(100) < mutationPercentage
                    ? GENES[r.Next(GENES.Length)]
                    : solution[i];
            }


            return newSolution;
        }

        private static char[] Crossover(
            Random r,
            char[] solution1,
            char[] solution2)
        {
            var newSolution = new char[targetSolution.Length];

            int crosses = r.Next(newSolution.Length);
            bool firstParent = true;

            for (int i = 0; i < newSolution.Length; i++)
            {
                newSolution[i] = firstParent
                    ? solution1[i]
                    : solution2[i];

                if (crosses-- <= 0)
                {
                    firstParent = !firstParent;
                }
            }

            return newSolution;
        }

        private static int Evaluate(
            char[] solution)
        {
            var result = 0;
            for (int i = 0; i < solution.Length; i++)
                if (solution[i] == targetSolution[i])
                    result++;

            return result;
        }

        private static char[] GeneticSearch(
            Random r,
            DateTime deadLine,
            int parentsCount,
            int populationSize)
        {
            char[] bestSolution = null;

            var population = RandomPopulation(r, populationSize);

            while (DateTime.Now < deadLine)
            {
                var selectedPopulation = population
                    .OrderByDescending(solution => Evaluate(solution))
                    .Take(parentsCount)
                    .ToList();

                while (selectedPopulation.Count < populationSize)
                {
                    var parent1 = selectedPopulation[r.Next(parentsCount)];
                    var parent2 = selectedPopulation[r.Next(parentsCount)];

                    var child = Crossover(r, parent1, parent2);

                    child = Mutate(r, child, MUTATION_PERCENTAGE);

                    selectedPopulation.Add(child);
                }

                population = selectedPopulation;

                Console.WriteLine();
                Console.WriteLine(population
                    .Select(solution => new string(solution))
                    .Aggregate((f, s) => f + Environment.NewLine + s));

                bestSolution = population
                    .OrderByDescending(solution => Evaluate(solution))
                    .First();

                if (Evaluate(bestSolution) == targetSolution.Length)
                {
                    break;
                }
            }

            return bestSolution;
        }

        public static void Run()
        {
            targetSolution = "Hello, GA!".ToArray();

            var solution = GeneticSearch(
                new Random(),
                DateTime.Now.AddSeconds(30),
                2,
                4);

            Console.WriteLine();
            Console.WriteLine($"Solution found: {new string(solution)}");
            Console.ReadLine();
        }
    }
}