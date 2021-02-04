using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Homework
{
    public static class HomeworkFiles
    {
        private static string path = @"C:\Users\k.krachmarov\source\repos\TechnicalUniversity\Homework\Files\";

        public static void MatrixSum()
        {
            string filePath = path + "matrix.txt";

            var allRows = File.ReadAllLines(filePath)
                .Select(l =>
                    l.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList())
                .ToList();

            if (allRows.Count == 0)
            {
                Console.WriteLine("Empty file");
            }

            var matrix = new int[allRows.Count, allRows[0].Count];

            int oddRowsEvenElementSum = 0;
            int evenColumnOddElementSum = 0;

            for (int i = 0; i < allRows.Count; i++)
            {
                for (int j = 0; j < allRows[0].Count; j++)
                {
                    matrix[i, j] = allRows[i][j];
                    if ((i + 1) % 2 == 1 && matrix[i, j] % 2 == 0)
                    {
                        oddRowsEvenElementSum += matrix[i, j];
                    }
                    else if ((j + 1) % 2 == 0 && matrix[i, j] % 2 == 1)
                    {
                        evenColumnOddElementSum += matrix[i, j];
                    }
                }
            }

            Console.WriteLine($@"Odd row and even element sum = {oddRowsEvenElementSum}
Even column and odd element sum = {evenColumnOddElementSum}");
        }

        public static void Statistic()
        {
            Console.WriteLine("Write the path to the file you want to use");
            string path = Console.ReadLine();
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    int dotCount = 0;
                    int commaCount = 0;
                    int numCount = 0;
                    while (!sr.EndOfStream)
                    {
                        char[] line = sr.ReadLine().ToCharArray();
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] == '.')
                            {
                                dotCount++;
                            }
                            else if (line[i] == ',')
                            {
                                commaCount++;
                            }

                            if (line[i] >= '0' && line[i] <= '9')
                            {
                                i++;
                                if (i >= line.Length)
                                {
                                    numCount++;
                                    break;
                                }

                                if (line[i] < '0' || line[i] > '9')
                                {
                                    numCount++;
                                    i--;
                                }
                                else i--;
                            }
                        }
                    }

                    Console.WriteLine($@"Dot counted: {dotCount}
Comma count: {commaCount}
Number count: {numCount}");
                }
            }
            else Console.WriteLine("No file found");
        }

        public static void SquareMatrix() // \t wont split
        {
            Console.WriteLine("Enter n:");

            int n = int.Parse(Console.ReadLine());

            int[,] matrix = new int[n, n];

            string filePath = path + "test.txt";

            if (File.Exists(filePath))
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] info = sr.ReadLine()
                            .Split('\t', StringSplitOptions.RemoveEmptyEntries);

                        int row = int.Parse(info[0]) - 1;

                        int col = int.Parse(info[1]) - 1;

                        if (info.Length == 2)
                        {
                            matrix[row, col] = -1;
                        }

                        else if (info.Length == 3)
                        {
                            int val = int.Parse(info[2]);
                            matrix[row, col] = val;
                        }
                    }

                    int evenElementsSum = 0;

                    int oddElementsSum = 0;

                    int evenRowsSum = 0;

                    int oddColumnsSum = 0;

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write($"{matrix[i, j]} \t");
                            if (matrix[i, j] % 2 == 0)
                            {
                                evenElementsSum += matrix[i, j];
                            }
                            else
                            {
                                oddElementsSum += matrix[i, j];
                            }

                            if (i % 2 == 0)
                            {
                                evenRowsSum += matrix[i, j];
                            }

                            if (j % 2 == 1)
                            {
                                oddColumnsSum += matrix[i, j];
                            }
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine($@"Even elements sum = {evenElementsSum}
Odd elements sum = {oddElementsSum}
Even rows sum = {evenRowsSum}
Odd columns sum = {oddColumnsSum}");
                }
            }
            else Console.WriteLine("No file found");
        }

        public static void Words()
        {
            string filePath = path + "large_sample.txt";

            int[] letters = new int[26];

            using (StreamReader sr = File.OpenText(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    int[] wordCharacters = CountLetters(line);

                    for (int i = 0; i < wordCharacters.Length; i++)
                    {
                        if (wordCharacters[i] > letters[i])
                        {
                            letters[i] = wordCharacters[i];
                        }
                    }
                }
            }

            PrintLetters(letters);
        }

        private static void PrintLetters(int[] letters)
        {
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] != 0)
                {
                    char letter = (char) ('a' + i);
                    for (int k = 0; k < letters[i]; k++)
                    {
                        Console.Write($"{letter},");
                    }
                }
            }
        }

        private static int[] CountLetters(string word)
        {
            int[] Counter = new int[26];
            for (int k = 0; k < word.Length; k++)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (word[k] == ('a' + i))
                    {
                        Counter[i]++;
                    }
                }
            }

            return Counter;
        }
//-------------------------------------------------------------------------
        struct Point
        {
            public int x, y;
        }

        public static void Maze()
        {
            string filePath = path + "lab-no_exit.txt";

            List<string> lines = File
                .ReadLines(filePath)
                .ToList();

            int size = int.Parse(lines[0]);

            int[,] maze = new int[size, size];

            Point start = new Point {x = int.Parse(lines[1]), y = int.Parse(lines[2])};

            Point end = new Point {x = int.Parse(lines[3]), y = int.Parse(lines[4])};

            for (int row = 5; row < lines.Count; row++)
            {
                int[] rowElements = lines[row]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < rowElements.Length; col++)
                {
                    maze[col, row - 5] = rowElements[col];
                }
            }

            var solutions = PathFinding(start, end, maze, size);

            if (solutions.Count != 0)
            {
                foreach (var solution in solutions)
                {
                    foreach (var points in solution)
                    {
                        Console.Write($"({points.x}, {points.y}) -> ");
                    }

                    Console.WriteLine("Exit");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No path ");
            }
        }

        private static List<List<Point>> PathFinding(Point start, Point end, int[,] maze, in int size)
        {
            List<List<Point>> completePath = new List<List<Point>>();

            List<List<Point>> pathList = new List<List<Point>>();
            List<Point> path = new List<Point>
            {
                start
            };
            pathList.Add(path);
            int pathNumber = 0;

            while (pathList.Count > 0)
            {
                pathNumber = pathList.Count;
                while (pathNumber > 0)
                {
                    path = pathList[0];
                    int n = path.Count - 1;
                    int x = path[n].x;
                    int y = path[n].y;

                    if (x == end.x && y == end.y)
                    {
                        completePath.Add(path);

                        pathList.RemoveAt(0);
                    }
                    else
                    {
                        if (x < size - 1)
                        {
                            if (maze[x + 1, y] != 1)
                            {
                                {
                                    List<Point> p = new List<Point>(path);
                                    p.Add(new Point {x = x + 1, y = y});

                                    pathList.Add(p);
                                }
                            }
                        }

                        if (x > 0)
                        {
                            if (maze[x - 1, y] != 1)
                            {
                                if (!AlreadyVisited(path, new Point {x = x - 1, y = y}))
                                {
                                    List<Point> p = new List<Point>(path);
                                    p.Add(new Point {x = x - 1, y = y});
                                    pathList.Add(p);
                                }
                            }
                        }


                        if (y > 0)
                        {
                            if (maze[x, y - 1] != 1)
                            {
                                if (!AlreadyVisited(path, new Point {x = x, y = y - 1}))
                                {
                                    List<Point> p = new List<Point>(path);
                                    p.Add(new Point {x = x, y = y - 1});
                                    pathList.Add(p);
                                }
                            }
                        }

                        if (y < size - 1)
                        {
                            if (maze[x, y + 1] != 1)
                            {
                                if (!AlreadyVisited(path, new Point {x = x, y = y + 1}))
                                {
                                    List<Point> p = new List<Point>(path);
                                    p.Add(new Point {x = x, y = y + 1});
                                    pathList.Add(p);
                                }
                            }
                        }

                        pathList.RemoveAt(0);
                    }

                    pathNumber--;
                }
            }

            return completePath;
        }

        private static bool AlreadyVisited(List<Point> points, Point currentPosition)
        {
            bool notVisited = true;

            int n = points.Count - 1;
            while (n >= 0 && notVisited)
            {
                if (points[n].x == currentPosition.x)
                    if (points[n].y == currentPosition.y)
                    {
                        notVisited = false;
                    }

                n--;
            }

            return !notVisited;
        }
    }
}