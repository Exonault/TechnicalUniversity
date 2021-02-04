using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ExamPrep
{
    public static class ExampleExamOneTaskThree
    {
        public static void Run()
        {
            SumOfElements();
        }

        private static void SumOfElements()
        {
            Console.WriteLine("Enter k");
            int k = int.Parse(Console.ReadLine());
            decimal[] values = new decimal[k + 1];
            for (int i = 0; i <= k; i++)
            {
                values[i] = GetElement(i);
            }

            decimal sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }

            Console.WriteLine(sum);
        }

        private static void SumOfElements(int k)
        {
            List<decimal> elements = new List<decimal>();

            for (int i = 0; i < k; i++)
            {
                elements.Add(GetElement(i));
            }
            Console.WriteLine(elements.Sum());
        }

        private static decimal GetElement(int i)
        {
            decimal result;
            int iSquared = i * i;
            result = 1 / (1 + iSquared);
            return result;
        }
    }
}