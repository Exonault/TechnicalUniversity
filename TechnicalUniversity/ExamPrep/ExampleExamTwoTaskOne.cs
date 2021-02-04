using System;
using System.IO;
using System.Linq;

namespace ExamPrep
{
    public static class ExampleExamTwoTaskOne
    {
        private static int[] values;

        public static void Run()
        {
            ReadFile();
            if (IsDescendingSorted())
            {
                Console.WriteLine("Array is descending sorted");
            }
            else Console.WriteLine("Array is NOT descending sorted");
            FindTriples();
        }

        private static void ReadFile()
        {
            Console.WriteLine("Enter file name");
            string fileName = Console.ReadLine();

            string filePath = @"C:\Users\k.krachmarov\source\repos\TechnicalUniversity\ExamPrep\Files\" + fileName;

            string[] array;
            using (StreamReader sr = File.OpenText(filePath))
            {
                //values = sr.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                array = sr.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

            values = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                values[i] = int.Parse(array[i]);
            }
        }

        private static bool IsDescendingSorted()
        {
            int n = values.Length;
            if (n == 0 || n == 1)
            {
                return true;
            }

            for (int i = 1; i < n; i++)
            {
                if (values[i - 1] < values[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static void FindTriples()
        {
            if (!IsDescendingSorted())
            {
                //Array.Sort(values);
                BubbleSort(values);
            }

            int count = 0;
            
            for (int i = 0; i < values.Length; i++)
            {
                int tripleCounter = 1;
                for (int j = i; j < values.Length - 1; j++)
                {
                    if (values[i] == values[j+1])
                    {
                        tripleCounter++;
                    }
                }

                if (tripleCounter == 3)
                {
                    count++;
                }
            }

            Console.WriteLine($"Count of triplets: {count}");
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
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
    }
}