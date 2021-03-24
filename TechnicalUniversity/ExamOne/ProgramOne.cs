using System;
using System.IO;

namespace ExamOne
{
    public static class ProgramOne
    {
        private static int size;
        private static int[,] matrix;

        public static void Run()// ProgramOne.Run() in main() method to work
        {
            InitMatrix();
           // PrintMatrix();
            Console.WriteLine($"The numbers, who are negative and divisible by 3 or 7 are: {CountNegDivisibleByThreeOrSeven()}");
            Console.WriteLine($"Is the matrix triangular: {IsTriangularMatrix()}");
            NormalizePositiveRows();
            //PrintMatrix();
        }


        private static void InitMatrix()
        {
            string fileLocation =
                @"C:\Users\k.krachmarov\source\repos\Git\TechnicalUniversity-Semester1\TechnicalUniversity\ExamOne\Files\test.txt";

            if (File.Exists(fileLocation))
            {
                using (StreamReader sr = File.OpenText(fileLocation))
                {
                    size = int.Parse(sr.ReadLine());
                    matrix = new int[size, size];

                    string[] line;

                    for (int i = 0; i < size; i++)
                    {
                        line = sr.ReadLine().Split("|");

                        for (int j = 0; j < size; j++)
                        {
                            matrix[i, j] = int.Parse(line[j]);
                        }
                    }
                }
            }
            else Console.WriteLine("No file found");
        }


        private static int CountNegDivisibleByThreeOrSeven()
        {
            int count = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int element = matrix[i, j];
                    if (element < 0 && (element % 3 == 0 || element % 7 == 0))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static bool IsTriangularMatrix()
        {
            bool isTriangular = false;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i > j && matrix[i, j] == 0)
                    {
                        isTriangular = true;
                    }
                }
            }
            return isTriangular;
        }

        private static void NormalizePositiveRows()
        {
            for (int i = 0; i < size; i++)
            {
                int[] row = GetRow(i);

                int maxValue = FindMaxPositive(row);
                int countNegative = CountNegative(row);

                for (int j = 0; j < row.Length; j++)
                {
                    if (maxValue == 0 || countNegative >= 1)
                    {
                        row[j] = 0;
                    }
                    else
                    {
                        row[j] /= maxValue;
                    }
                }


                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = row[j];
                }
            }
        }

        private static int[] GetRow(int i)//Gets a row from the matrix
        {
            int[] result = new int[size];

            for (int j = 0; j < size; j++)
            {
                result[j] = matrix[i, j];
            }

            return result;
        }

        private static int FindMaxPositive(int[] arr)//Finds the max value of an array, which is positive
        {
            int result = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > result && arr[i] > 0)
                {
                    result = arr[i];
                }
            }
            
            return result;
        }

        private static int CountNegative(int[] arr)// Counts the negative numbers in an array
        {
            int counter = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                {
                    counter++;
                }
            }
            return counter;
        }

        private static void PrintMatrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }

                Console.WriteLine();
            }
        }
    }
}