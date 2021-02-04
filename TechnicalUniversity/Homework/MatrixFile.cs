using System;
using System.IO;
using System.Linq;
using System.Net.Mail;


namespace Homework
{
    public static class MatrixFile
    {
        private static string path = @"C:\Users\k.krachmarov\source\repos\TechnicalUniversity\Homework\Files\";

        private static decimal[,] matrix;

        public static void ReadMatrix()
        {
            string filePath = path + "test.txt";

            int rows;
            int cols;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    rows = int.Parse(sr.ReadLine());
                    cols = int.Parse(sr.ReadLine());

                    matrix = new Decimal[rows, cols];

                    var allRows = File.ReadAllLines(filePath)
                        .Skip(2)
                        .Select(l =>
                            l.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(decimal.Parse)
                                .ToList())
                        .ToList();
                    if (allRows.Count == 0)
                    {
                        Console.WriteLine("Empty file");
                    }

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            matrix[i, j] = allRows[i][j];
                        }
                    }
                }
            }
            else Console.WriteLine("No file found");
        }

        public static void PrintMatrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }

                Console.WriteLine();
            }
        }

        public static bool CheckIdentity()
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((i == j && matrix[i, j] != 1) || (i != j && matrix[i, j] != 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static void SumNegativeOnAntiDiagonal()
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new Exception("Not defined for non square matrix");
            }

            int count = matrix.GetLength(0);
            decimal sum = 0;
            
            /* for (int i = 0; i < count; i++)
             {
                 if (matrix[i, count - i - 1] < 0)
                 {
                     sum += matrix[i, count - i - 1];
                 }
             }*/

            for (int i = 0, j = matrix.GetLength(1) - 1; i < matrix.GetLength(0); i++, j--)
            {
                if (matrix[i, j] < 0)
                {
                    sum += matrix[i, j];
                }
            }

            Console.WriteLine(sum);
        }

        public static void NormalizeRows()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                decimal formulaResult = 0;
                decimal[] row = GetRow(i);
                for (int j = 0; j < row.Length; j++)
                {
                    formulaResult += (row[j] * row[j]);
                }

                formulaResult = (decimal) Math.Sqrt((double) formulaResult);

                if (formulaResult != 0)
                {
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        matrix[i, k] /= formulaResult;
                    }
                }
            }
        }

        public static void SortMatrix()
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                var col = GetColumn(i);

                if (i % 2 == 0)
                {
                    Array.Sort(col);
                }

                else if (i % 2 == 1)
                {
                    Array.Sort(col);
                    Array.Reverse(col);
                }

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matrix[j, i] = col[j];
                }
            }
        }

        private static decimal[] GetColumn(int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        private static decimal[] GetRow(int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }
    }
}