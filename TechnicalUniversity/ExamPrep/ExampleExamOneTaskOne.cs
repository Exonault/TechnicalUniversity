using System;
using System.Linq;

namespace ExamPrep
{
    public static class ExampleExamOneTaskOne
    {
        private static int[,] matrix;
        private static int matrixSize;

        public static void Run()
        {
            InitializeMatrix();
            PrintMatrix();
            MatrixMaxInEachColumn();
        }

        private static void InitializeMatrix()
        {
            Console.WriteLine("Enter n");
            int n = int.Parse(Console.ReadLine());

            if (!(n > 0 && n < 10))
            {
                Console.WriteLine("Wrong size");
            }
            else
            {
                matrix = new int[n, n];
                matrixSize = n;
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        if (i <= j)
                        {
                            Console.WriteLine($"Enter value for element in position[{i},{j}]");
                            int value = int.Parse(Console.ReadLine());
                            matrix[i, j] = value;
                        }
                        else matrix[i, j] = 0;
                    }
                }
            }
        }

        private static void MatrixMaxInEachColumn()
        {
            int[] maxValues = new int[matrixSize];

            /*  for (int i = 0; i < matrixSize; i++)
              {
                  int[] column = GetColumn(i);
                  for (int j = 0; j < column.Length; j++)
                  {
                      int maxValue = column[0];
                      if (column[j]>maxValue)
                      {
                          maxValue = column[j];
                      }
  
                      maxValues[i] = maxValue;
                  }
              }
              */

            for (int i = 0; i < matrixSize; i++)
            {
                int maxValueForColumn = matrix[0, i];
                for (int j = 0; j < matrixSize; j++)
                {
                    if (matrix[j, i] > maxValueForColumn)
                    {
                        maxValueForColumn = matrix[j, i];
                    }
                }

                maxValues[i] = maxValueForColumn;
            }

            //Array.Sort(maxValues);

            BubbleSort(maxValues);
            int min = maxValues[0];
            int max = maxValues[matrixSize - 1];
            Console.WriteLine($"min:{min} \nmax:{max}");
        }

        private static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // swap temp and arr[i] 
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        /* private static int[] GetColumn(int columnNumber)
         {
             return Enumerable.Range(0, matrix.GetLength(0))
                 .Select(x => matrix[x, columnNumber])
                 .ToArray();
         }
        */
        private static void PrintMatrix()
        {
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }

                Console.WriteLine();
            }
        }
    }
}