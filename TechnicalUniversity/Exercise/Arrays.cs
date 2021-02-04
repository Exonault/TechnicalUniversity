using System;
using System.Linq;
namespace Excercise
{
    public class Arrays
    {
        public void Run()
        {
            Console.WriteLine("1 dimention or 2 dimentions");
            int d = int.Parse(Console.ReadLine());
            if (d == 1)
            {
                Console.Write("n=");
                int n = int.Parse(Console.ReadLine());
                int[] arr;
                arr = ReadArray(n);
                WriteArr(arr);
                MinArr(arr);
            }
            else if (d == 2)
            {
                Console.WriteLine("n=");
                int n = int.Parse(Console.ReadLine());
                Console.WriteLine("m=");
                int m = int.Parse(Console.ReadLine());
                int[,] arr2;
                arr2 = ReadArray(n, m);
                Console.WriteLine();
                WriteArr(arr2);
                int[] minInEachRow = MinInEachRow(arr2);
                WriteMinInEachRow(minInEachRow);
                double avg = AverageInArray(arr2);
                Console.WriteLine($"Average is {avg}");
                double avgDiagonal = AverageMainDiagonalInArray(arr2);
                Console.WriteLine($"Average of the main diagonal is {avgDiagonal}");
                double avgSecondDiagonal = AverageSecondDiagonalInArray(arr2);
                Console.WriteLine($"Average of the second diagonal is {avgSecondDiagonal}");
                double avgAboveOfMainDiagonal = AverageAboveOfMainDiagonal(arr2);
                Console.WriteLine($"Average of the numbers ontop of the main diagonal is {avgAboveOfMainDiagonal}");
                double avgUnderOfMainDiagonal = AverageUnderOfMainDiagonal(arr2);
                Console.WriteLine($"Average of the numbers ontop of the main diagonal is {avgUnderOfMainDiagonal}");
            }

        }

        private double AverageAboveOfMainDiagonal(int[,] arr2)
        {
            int sum = 0;
            int count = 0;
            for (int i = 0; i < arr2.GetLength(0); i++)
            {
                for (int j = 0; j < arr2.GetLength(1); j++)
                {
                    if (i<j)
                    {
                        sum += arr2[i, j];
                        count++;
                    }
                }
            }

            double result = sum / count;
            return result;
        }

        private double AverageUnderOfMainDiagonal(int[,] arr2)
        {
            int sum = 0;
            int count = 0;
            for (int i = 0; i < arr2.GetLength(0); i++)
            {
                for (int j = 0; j < arr2.GetLength(1); j++)
                {
                    if (i > j)
                    {
                        sum += arr2[i, j];
                        count++;
                    }
                }
            }

            double result = sum / count;
            return result;
        }

        private double AverageSecondDiagonalInArray(int[,] arr2)
        {
            int sum = 0;
            int count = arr2.GetLength(0);

            for (int i = 0; i < count; i++)
            {
                sum += arr2[i, count - i - 1];
            }

            double result = sum / count;
            return result;
        }

        private double AverageMainDiagonalInArray(int[,] arr2)
        {
            int sum = 0;
            int count = Math.Min(arr2.GetLength(0), arr2.GetLength(1));
            for (int i = 0; i < count; i++)
            {
                sum += arr2[i, i];
            }
            double result = sum / count;
            return result;
        }

        private double AverageInArray(int[,] arr2)
        {
            int sum = 0;
            int rows = arr2.GetLength(0);
            int cols = arr2.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sum += arr2[i, j];
                }  
            }
            double result = sum / (rows * cols);
            return result;
        }

        private void WriteMinInEachRow(int[] minInEachRow)
        {
            for (int i = 0; i < minInEachRow.Length; i++)
            {
                Console.WriteLine($"For row {i} of arr the min element is {minInEachRow[i]}");
            }
        }

        private int[] MinInEachRow(int[,] arr)
        {
            int[] result = new int[arr.GetLength(0)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                int min = arr[i, 0];
                for (int j = 1; j < arr.GetLength(1); j++)
                {
                    if (min > arr[i, j])
                    {
                        min = arr[i, j];
                    }
                }
                result[i] = min;
            }

            return result;
        }

        private void MinArr(int[] arr)
        {
            Array.Sort(arr);
            Console.WriteLine(arr[0]);
        }


        private void WriteArr(int[,] arr2)
        {
            for (int i = 0; i < arr2.GetLength(0); i++)
            {
                for (int j = 0; j < arr2.GetLength(1); j++)
                {
                    Console.Write(arr2[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        private void WriteArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + "\t");
            }
        }

        private int[] ReadArray(int n)
        {
            int[] result = new int[n];
            Console.WriteLine("arr1");
            for (int i = 0; i < n; i++)
            {
                result[i] = int.Parse(Console.ReadLine());
            }
            return result;
        }

        private int[,] ReadArray(int n, int n1)
        {
            int[,] result = new int[n, n];

            Console.WriteLine("arr2");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n1; j++)
                {
                    result[i, j] = int.Parse(Console.ReadLine());
                }
            }
            return result;
        }
    }
}
