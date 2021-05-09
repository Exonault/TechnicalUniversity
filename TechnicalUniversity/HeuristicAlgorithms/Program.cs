using System;
using System.Linq;
using System.Threading;

namespace HeuristicAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            // псевдо рандом генератор
            var random = new Random();

            var n = 1;
            Console.WriteLine("Machines count:" + n);

            var m = random.Next(100);
            Console.WriteLine("Tasks count:" + m);

            // инициализира времената за всяка задача w и наградите за решените задачи p със случайни числа
            int[] w = new int[m];
            int[] p = new int[m];
            for (int i = 0; i < m; i++)
            {
                w[i] = random.Next(100);
                p[i] = random.Next(100);
                Console.WriteLine("Task '" + i + "'\tneeded time:" + w[i] + "\tprice:" + p[i]);
            }

            // инициализира максималното време с по-малко от нужното за всички задачи
            var T = (int) (w.Sum() * 0.75);
            Console.WriteLine("Hiring time:" + T);

            // търси най-доброто решение за 10 сек.
            var result = HeuristicSearch(
                random,
                w,
                p,
                T,
                DateTime.Now.AddSeconds(10));

            // визуализира намереното решение
            Console.WriteLine(result
                .Select(s => s.ToString())
                .Aggregate((f, s) => f + s));

            // визуализира оценката на намереното решение
            Console.WriteLine(
                Evaluate(w, p, T, result));

            Console.ReadKey();
        }

        public static int Evaluate(int[] w, int[] p, int T, int[] solution)
        {
            int result = 0;
            for (int i = 0; i < w.Length; i++)
            {
                if (solution[i] == 1)
                {
                    T -= w[i];
                    if (T < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        result += p[i];
                    }
                }
            }

            return result;
        }

        public static int[] RandomSolution(Random random, int size)
        {
            int[] result = new int[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = random.Next(0, 2);
            }

            return result;
        }


        public static int[] NextSolution(int[] w, int[] p, int T, int[] solution)
        {
            int[] result = solution.ToArray();

            int best = Int32.MinValue;
            int bestIndex = 0;
            for (int i = 0; i < solution.Length; i++)
            {
                result[i] = Math.Abs(result[i] - 1);

                int value = Evaluate(w, p, T, result);

                if (best < value)
                {
                    best = value;
                    bestIndex = i;
                }

                result[i] = Math.Abs(result[i] - 1);
            }

            result[bestIndex] = Math.Abs(result[bestIndex] - 1);

            return result;
        }

        public static int[] HeuristicSearch(
            Random random, // ще се ползва за генериране на случайно решение
            int[] w, // времена на изпълнение на задачите
            int[] p, // награда за изпълнение на задачите
            int T, // максимално време
            DateTime deadLine) // до кога да се изпълнява метода
        {
            int[] bestSolution = RandomSolution(random, w.Length);

            int bestValue = Evaluate(w, p, T, bestSolution);

            int[] currentSoluton = bestSolution;

            int currentValue = Evaluate(w, p, T, bestSolution);

            while (DateTime.Now < deadLine)
            {
                int[] newSolution = NextSolution(w, p, T, currentSoluton);

                int newValue = Evaluate(w, p, T, newSolution);

                if (newValue > currentValue)
                {
                    currentValue = newValue;
                    currentSoluton = newSolution;
                }
                else
                {
                    currentSoluton = RandomSolution(random, w.Length);
                    currentValue = Evaluate(w, p, T, currentSoluton);
                }

                if (currentValue > bestValue)
                {
                    bestValue = currentValue;
                    bestSolution = currentSoluton;
                }
            }

            return bestSolution;
        }
    }
}