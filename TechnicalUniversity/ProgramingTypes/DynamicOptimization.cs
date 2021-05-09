using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace DynamicOptimization
{
    public class DynamicOptimization
    {
        private int[,] _costs = new int[,]
        {
            {-1, 1, 3, -1, 6},
            {1, -1, 2, -1, 8},
            {3, 2, -1, 8, 2},
            {-1, 1, 8, -1, 1},
            {3, 8, 2, 1, -1},
        };

        public void Run()
        {
            string a;
            this.SolutionRec(0, new List<int>(), out a);
            this.SolutionRec(0, 0, out a);
            Console.WriteLine(a);
        }

        private int SolutionRec(int from, List<int> visited, out string route)
        {
            route = "";

            if (visited.Count + 1 == _costs.GetLength(0))
            {
                route += from.ToString();
                return _costs[from, 0];
            }

            visited.Add(from);

            int bestSubSolution = int.MaxValue;
            for (int to = 0; to < _costs.GetLength(0); to++)
            {
                if (!visited.Contains(to) && _costs[from, to] != -1)
                {
                    int subSolution = _costs[from, to] + SolutionRec(to, visited, out string subRoute);

                    if (subSolution < _costs[from, to])
                    {
                        continue;
                    }

                    if (bestSubSolution > subSolution)
                    {
                        bestSubSolution = subSolution;
                        route = from + " " + subRoute;
                    }
                }
            }

            visited.Remove(from);

            return bestSubSolution == int.MaxValue ? -1 : bestSubSolution;
        }

        private int SolutionRec(int from, int visited, out string route)
        {
            route = "";

            visited |= 1 << from;

            if (visited == (1 << _costs.GetLength(1)) - 1)
            {
                route += from.ToString();
                return _costs[from, 0];
            }


            int bestSubSolution = int.MaxValue;
            for (int to = 0; to < _costs.GetLength(0); to++)
            {
                if ((visited &(1 << to)) == 0 && _costs[from, to] != -1)
                {
                    int subSolution = _costs[from, to] + SolutionRec(to, visited, out string subRoute);

                    if (subSolution < _costs[from, to])
                    {
                        continue;
                    }

                    if (bestSubSolution > subSolution)
                    {
                        bestSubSolution = subSolution;
                        route = from + " " + subRoute;
                    }
                }
            }


            return bestSubSolution == int.MaxValue ? -1 : bestSubSolution;
        }
    }
}