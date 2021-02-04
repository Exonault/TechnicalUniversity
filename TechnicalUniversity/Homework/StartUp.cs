using System;

namespace Homework
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[,] {{5, 6, 10}, {7, 8, 20}};
            Task.SumOfRowsAndCollumns(matrix);
        }
    }
}