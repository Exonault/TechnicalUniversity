using System;

namespace Exam
{
    public static class ProgramTwo
    {
        //Ni = -N(i-1) + (2N(i-2))^2 + (N(i-3))^3, i > 3
        //N1 = -1, N2 = 2, N3 = 3
        public static void Run() // ProgramTwo.Run() in main() method to work
        {
            Console.WriteLine("Enter n:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine($"Nth number of the sequence: {FindNthIterative(n)}");
            Console.WriteLine($"Nth number of the sequence: {FindNthRecursive(n)}");
        }

        /*Ni = -N(i-1) + (2N(i-2))^2 + (N(i-3))^3, i > 3
        N1 = -1, N2 = 2, N3 = 3
        a = N(i-1)
        b = N(i-2)
        c = N(i-3)
        */
        private static int
            FindNthIterative(int n) //Iterative with array. I know its not optimized, but its greedy algo that works.
        {
            int result = 0;
            int[] arr = new int[n];

            arr[0] = -1;
            arr[1] = 2;
            arr[2] = 3;

            for (int i = 3; i < n; i++)
            {
                int a = arr[i - 1];
                int b = arr[i - 2];
                int c = arr[i - 3];

                arr[i] = -a + (2 * (b * b)) + (c * c * c);
            }

            result = arr[n - 1];
            return result;
        }

        /*
          Ni = -N(i-1) + (2N(i-2))^2 + (N(i-3))^3, i > 3
          N1 = -1, N2 = 2, N3 = 3
         *
         *
         * a = -N(i-1);
         * b = N(i-2);
         * c = N(i-3);
         *
         * Multiplication because Math.Pow() doesnt exist;
         */
        private static int FindNthRecursive(int n)
        {
            if (n == 1)
            {
                return -1;
            }

            if (n == 2)
            {
                return 2;
            }

            if (n == 3)
            {
                return 3;
            }

            else
            {
                int a = FindNthRecursive(n - 1);
                int b = FindNthRecursive(n - 2);
                int c = FindNthRecursive(n - 3);

                int result = -a + (2 * (b * b)) + (c * c * c);

                return result;
            }
        }
    }
}