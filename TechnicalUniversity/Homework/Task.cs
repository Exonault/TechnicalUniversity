using System;
using System.Linq;

namespace Homework
{
    public static class Task
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());

            Console.WriteLine(Task.NthElementSequenceIterative(m));
            Console.WriteLine(Task.NthElementSequenceRecursive(m));

            Console.WriteLine(Task.RecursiveSum(m));
            Console.WriteLine(Task.RecursiveSum(n, m));

            Console.WriteLine(Task.IsMatrixEquals(new int[,] {{1, 2}, {3, 4}, {5, 6}, {7, 8}},
                new int[,] {{2, 2}, {3, 4}, {5, 6}, {7, 8}}));

            Task.SumOfRowsAndCollumns(new int[,] {{5, 6, 10}, {7, 8, 20}});

            int[,] matrix = {{1, 3, 6, 8}, {3, 8, 0, 2}, {2, 5, 6, 9}, {1, 3, 4, 5}};
            Task.PrintElementsAboveMainDiagonal(matrix);

            int[] arr = {1, 2, 3, 4, 5, 6};
            Task.ReverseArray(arr, 0, arr.Length - 1);
            Console.WriteLine(string.Join(" ", arr));
        }

        //Домашно 30.11
        public static int NthElementSequenceIterative(int n)
        {
            int[] arr = new int[n];
            arr[0] = 2;
            arr[1] = 4;
            arr[2] = 6;

            for (int i = 3; i < n; i++)
            {
                arr[i] = (3 * arr[i - 3] + 4 * arr[i - 2] - 7 * arr[i - 1]);
            }

            return arr[n - 1];
        }

        public static int NthElementSequenceRecursive(int n)
        {
            if (n == 1)
            {
                return 2;
            }

            if (n == 2)
            {
                return 4;
            }

            if (n == 3)
            {
                return 6;
            }
            else
            {
                return (3 * NthElementSequenceRecursive(n - 3) +
                        4 * NthElementSequenceRecursive(n - 2) -
                        7 * NthElementSequenceRecursive(n - 1));
            }
        }

        public static bool IsMatrixEquals(int[,] a, int[,] b) //1
        {
            bool isEqual = false;
            if ((a.GetLength(0) == b.GetLength(0)) && (a.GetLength(1) == b.GetLength(1)))
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (a[i, j] == b[i, j])
                        {
                            isEqual = true;
                        }
                        else
                        {
                            isEqual = false;
                            break;
                        }
                    }

                    if (isEqual == false)
                    {
                        break;
                    }
                    else continue;
                }
            }
            else
            {
                isEqual = false;
            }

            return isEqual;
        }

        public static void SumOfRowsAndCollumns(int[,] matrix) //2
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[] sumOfRows = new int[rows];
            int[] sumOfColls = new int[cols];

            for (int i = 0; i < rows; i++)
            {
                sumOfRows[i] = 0;
                for (int j = 0; j < cols; j++)
                {
                    sumOfRows[i] = sumOfRows[i] + matrix[i, j];
                }
            }

            for (int i = 0; i < cols; i++)
            {
                sumOfColls[i] = 0;
                for (int j = 0; j < rows; j++)
                {
                    sumOfColls[i] = sumOfColls[i] + matrix[j, i];
                }
            }

            System.Console.WriteLine($"Sum of rows: {string.Join(" ", sumOfRows)}");
            System.Console.WriteLine($"Sum of collums: {string.Join(" ", sumOfColls)}");
        }

        public static void SumOfRowsAndCollumns(int[,] matrix, out int[] rowSum, out int[] colSum) //2
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            rowSum = new int[rows];
            colSum = new int[cols];

            for (int i = 0; i < rows; i++)
            {
                rowSum[i] = 0;
                for (int j = 0; j < cols; j++)
                {
                    rowSum[i] = rowSum[i] + matrix[i, j];
                }
            }

            for (int i = 0; i < cols; i++)
            {
                colSum[i] = 0;
                for (int j = 0; j < rows; j++)
                {
                    colSum[i] = colSum[i] + matrix[j, i];
                }
            }

            //   System.Console.WriteLine($"Sum of rows: {string.Join(" ", rowSum)}");
            //  System.Console.WriteLine($"Sum of collums: {string.Join(" ", colSum)}");
        }

        public static void PrintElementsAboveMainDiagonal(int[,] matrix) //3
        {
            int m = matrix.GetLength(0);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i < j)
                        System.Console.Write(matrix[i, j] + " \t");
                    else
                        System.Console.Write(" \t");
                }

                System.Console.WriteLine();
            }
        }

        public static void ReverseArray(int[] arr, int start,
            int end)
        {
            int temp;
            if (start >= end)
                return;

            temp = arr[start];
            arr[start] = arr[end];
            arr[end] = temp;

            ReverseArray(arr, start + 1, end - 1);
        }

        public static void bubbleSort(int[] arr, int n)
        {
            // Base case
            if (n == 1)
                return;

            // One pass of bubble 
            // sort. After this pass,
            // the largest element
            // is moved (or bubbled) 
            // to end.
            for (int i = 0; i < n - 1; i++)
                if (arr[i] > arr[i + 1])
                {
                    // swap arr[i], arr[i+1]
                    int temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }

            // Largest element is fixed,
            // recur for remaining array
            bubbleSort(arr, n - 1);
        }

        public static void RecursiveSwap(int[] array) //4
        {
        }

        public static int RecursiveSum(int n) //5
        {
            int sum = 0;
            if (n == 1)
            {
                return 1;
            }
            else sum = n + RecursiveSum(n - 1);

            return sum;
        }

        public static int RecursiveSum(int n, int m) //6
        {
            int sum = 0;
            if (m == n)
            {
                return n;
            }
            else sum = m + RecursiveSum(n, m - 1);

            return sum;
        }
    }
}