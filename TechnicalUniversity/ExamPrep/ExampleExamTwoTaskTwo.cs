using System;
using System.IO;
using System.Linq;

namespace ExamPrep
{
    public static class ExampleExamTwoTaskTwo
    {
        private static string[] words;
        private static string input;
        public static void Run()
        {
            Read();
            WordCount();
            Numbers();
            Letters();
        }

        private static void Read()
        {
            input = Console.ReadLine();
            words = input.Split(new char[] {' ', ',', '.'}, StringSplitOptions.RemoveEmptyEntries);
        }

        private static void WordCount() => Console.WriteLine($"Words count: {words.Length}");

        private static void Numbers()
        {
            int numCounts = 0;
            bool isNumbers = false;
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                for (int j = 0; j < word.Length; j++)
                {
                    if (word[j] >= '0' && word[j] <= '9')
                    {
                        isNumbers = true;
                        numCounts++;
                    }
                }
            }

            Console.WriteLine($"Is there numbers in the message: {isNumbers}");
            Console.WriteLine($"Number count: {numCounts}");
        }

        private static void Letters()
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string inputUpper =  input.ToUpper();

            for (int i = 0; i < letters.Length; i++)
            {
                char letter = letters[i];
                int letterOcurences = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    if (letter == inputUpper[j])
                    {
                        letterOcurences++;
                    }
                }
                
                Console.WriteLine($"Letter {letters[i]} has ocurred {letterOcurences} times");
            }
        }
    }
}