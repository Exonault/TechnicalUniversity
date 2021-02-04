using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Text;

namespace Exercise
{
    public class Tasks // 04.01.21
    {
        public int SumRecursive(int n)
        {
            if (n <= 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }
            else return n + SumRecursive(n - 1);
        }

        public void ElementCount()
        {
            //my impl
            Console.WriteLine("Enter an array");
            //int[] arr = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int[] arr = {1, 2, 1, 2, 1, 2, 4, 5, 5};
            int[] values = arr.Distinct().ToArray();


            for (int i = 0; i < values.Length; i++)
            {
                int value = values[i];
                int count = 0;
                for (int j = 0; j < arr.Length; j++)
                {
                    if (value == arr[j])
                    {
                        count++;
                    }
                }

                Console.WriteLine($"Element {value} has been counted {count} times");
            }
        }

        public void ElementCounting()
        {
            //lectors impl
            Console.WriteLine("Enter lenght of array");
            int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n];
            int[] countArr = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter value for index {i}");
                arr[i] = int.Parse(Console.ReadLine());
                countArr[i] = 0;
            }

            for (int i = 0; i < n; i++)
            {
                int counter = 1;
                if (countArr[i] == 0)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (arr[i] == arr[j])
                        {
                            counter++;
                            countArr[j] = -1;
                        }
                    }

                    countArr[i] = counter;
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (countArr[i] != -1)
                {
                    Console.WriteLine($"Element{arr[i]} has been counted {countArr[i]} times");
                }
            }
        }

        public int LongestSequenceWithoutSpaces(string s)
        {
            int counter = 1;
            int longestSequence = 1;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == s[i - 1])
                {
                    counter++;
                }
                else
                {
                    counter = 1;
                }

                if (counter > longestSequence)
                {
                    longestSequence = counter;
                }
            }

            return longestSequence;
        }

        public int LongestSequenceWithSpaces(string s)
        {
            int counter = 1;
            int longestSequence = 1;
            char lastChar = s[0];

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    if (s[i] == lastChar)
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 1;
                    }

                    lastChar = s[i];

                    if (counter > longestSequence)
                    {
                        longestSequence = counter;
                    }
                }
            }

            return longestSequence;
        }
    }
}