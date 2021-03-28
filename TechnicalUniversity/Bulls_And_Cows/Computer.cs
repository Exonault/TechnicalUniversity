using System;
using System.Collections.Generic;

namespace Bulls_And_Cows
{
    public class Computer
    {
        private List<String> _possibleNumbers;
        private Random _random;

        public Computer()
        {
            _random = new Random();
            _possibleNumbers = AllPossibleCombinations();
        }

        public bool Play()
        {
            int initialIndex = _random.Next(_possibleNumbers.Count);
            String initialNumber = _possibleNumbers[initialIndex];
            Console.WriteLine($"Computer's guess is {initialNumber}");
            Console.WriteLine("Enter the number of bulls:");
            int bullsCount = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the number of cows:");
            int cowsCount = int.Parse(Console.ReadLine());

            if (bullsCount == 4)
            {
                Console.WriteLine("Computer wins!");
                return true;
            }

            _possibleNumbers.RemoveAt(initialIndex);
            NumbersPruning(_possibleNumbers, initialNumber, bullsCount, cowsCount);

            if (_possibleNumbers.Count < 1 && bullsCount != 4)
            {
                throw new Exception();
            }

            Console.WriteLine("------------------------------------");

            return false;
        }

        private void NumbersPruning(List<string> possibleNumbers, String initialNumber, int bullsCount, int cowsCount)
        {
            for (int i = 0; i < possibleNumbers.Count; i++)
            {
                string currentNumber = possibleNumbers[i];

                int currentBullsCount = 0;
                int currentCowsCount = 0;

                for (int j = 0; j < initialNumber.Length; j++)
                {
                    if (currentNumber.Contains(initialNumber[j]))
                    {
                        currentCowsCount++;

                        if (currentNumber[j] == initialNumber[j])
                        {
                            currentBullsCount++;
                            currentCowsCount--;
                        }
                    }
                }

                if (currentBullsCount != bullsCount || currentCowsCount != cowsCount)
                {
                    possibleNumbers.RemoveAt(i);
                    i--;
                }
            }
        }

        private List<string> AllPossibleCombinations()
        {
            List<string> result = new List<string>();

            for (int firstDigit = 1; firstDigit <= 9; firstDigit++)
            {
                for (int secondDigit = 0; secondDigit <= 9; secondDigit++)
                {
                    if (firstDigit != secondDigit)
                    {
                        for (int thirdDigit = 0; thirdDigit <= 9; thirdDigit++)
                        {
                            if (thirdDigit != firstDigit && thirdDigit != secondDigit)
                            {
                                for (int fourthDigit = 0; fourthDigit <= 9; fourthDigit++)
                                {
                                    if (fourthDigit != firstDigit && fourthDigit != secondDigit &&
                                        fourthDigit != thirdDigit)
                                    {
                                        string number = firstDigit + "" + secondDigit + "" + thirdDigit + "" +
                                                        fourthDigit + "";
                                        result.Add(number);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}