using System;
using System.Linq;

namespace DynamicOptimization
{
    public class BranchAndBound
    {
        public void Run()
        {
            int[,] values = new int[,]
            {
                {9, 2, 7, 5},
                {10, 4, 3, 7},
                {5, 8, 1, 8},
                {7, 6, 9, 4}
            };

            int max = 0;
            for (int i = 0; i <  values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    if (max <  values[i, j])
                    {
                        max =  values[i, j];
                    }
                }
            }

            for (int i = 0; i <  values.GetLength(0); i++)
            {
                for (int j = 0; j <  values.GetLength(1); j++)
                {
                    values[i, j] = max -  values[i, j];
                }
            }
            int upperBound = Int32.MaxValue;
            int[] bestSolution = SolutionMinBB(values, 0, new int[] {-1, -1, -1, -1}, 0, ref upperBound);

            Console.WriteLine(String.Join(", ", bestSolution));
        }

        private void SolutionMaxBB(int[,] jobs)
        {
        }

        private int[] SolutionMinBB(int[,] jobs, int worker, int[] assignment, int timeAccumulated,
            ref int timeUpperBound)
        {
            if (worker == assignment.Length)
            {
                if (timeAccumulated < timeUpperBound)
                {
                    timeUpperBound = timeAccumulated;
                    return assignment.ToArray();
                }
                else
                {
                    return null;
                }
            }

            int[] bestSolution = null;
            for (int i = 0; i < jobs.GetLength(0); i++)
            {
                bool assigned = false;
                for (int j = 0; j < assignment.Length && !assigned; j++)
                {
                    if (assignment[j] == i)
                    {
                        assigned = true;
                    }
                }

                if (!assigned)
                {
                    assignment[worker] = i;

                    int timeNew = timeAccumulated + jobs[i, worker];

                    if (timeNew >= timeUpperBound)
                    {
                        continue;
                    }

                    int[] solution = SolutionMinBB(jobs, worker + 1, assignment, timeNew, ref timeUpperBound);

                    if (solution != null)
                    {
                        bestSolution = solution;
                    }

                    assignment[worker] = -1;
                }
            }

            return bestSolution;
        }
    }
}