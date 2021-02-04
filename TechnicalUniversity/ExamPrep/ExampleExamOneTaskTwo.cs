using System;
using System.IO;

namespace ExamPrep
{
    public static class ExampleExamOneTaskTwo
    {
        public static void Run()
        {
            Statistic();
        }

        private static void Statistic()
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
                    int letterCount = 0;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        //char[] line = sr.ReadLine().ToCharArray();
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
                            else if (line[i] >= '0' && line[i] <= '9')
                            {
                                numCount++;
                            }
                            else if ((line[i] >= 'A' && line[i] <= 'Z') || (line[i] >= 'a' && line[i] <= 'z'))
                            {
                                letterCount++;
                            }

                            /* else if (Char.IsNumber(line[i]))
                             {
                                 numCount++;
                             }
                             else if (Char.IsLetter(line[i]))
                             {
                                 letterCount++;
                             }
                             */
                        }
                    }

                    Console.WriteLine($@"Number count: {numCount}
Letter count: {letterCount}
Comma count: {commaCount}
Dot counted: {dotCount}
");
                }
            }
            else Console.WriteLine("No file found");
        }
    }
}