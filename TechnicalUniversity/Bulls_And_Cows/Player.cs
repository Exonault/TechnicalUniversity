using System;
using System.Text;

namespace Bulls_And_Cows
{
    public class Player
    {
        private Random _random;
        private StringBuilder _stringBuilder;
        private bool _firstRound;
        private string _computerNumber;

        public Player()
        {
            _random = new Random();
            _stringBuilder = new StringBuilder();
            _stringBuilder.Append("Previous guesses: Number Bulls Cows\n");
            _firstRound = true;
            _computerNumber = GenerateNumber();
        }

        public bool Play()
        {
            if (!_firstRound)
            {
                Console.WriteLine(_stringBuilder.ToString());
            }
            else
            {
                _firstRound = false;
            }

            int bullsCount = 0;
            int cowsCount = 0;

            string playerGuess = ValidInput();

            for (int i = 0; i < _computerNumber.Length; i++)
            {
                if (_computerNumber.Contains(playerGuess[i] + ""))
                {
                    cowsCount++;
                    if (_computerNumber[i] == playerGuess[i])
                    {
                        bullsCount++;
                        cowsCount--;
                    }
                }
            }

            Console.WriteLine($"Bulls: {bullsCount} Cows: {cowsCount} \n");
            _stringBuilder.Append($"                  {playerGuess}   {bullsCount}     {cowsCount} \n");
            if (bullsCount == 4)
            {
                Console.WriteLine("Player wins");
                return true;
            }

            Console.WriteLine("------------------------------------");
            return false;
        }


        private string GenerateNumber()
        {
            int[] digits = new int[4];
            string number = "";

            number += digits[0] = _random.Next(9) + 1;
            number += digits[1] = GenerateUniqueDigits(digits[0], -1, -1);
            number += digits[2] = GenerateUniqueDigits(digits[0], digits[1], -1);
            number += digits[3] = GenerateUniqueDigits(digits[0], digits[1], digits[2]);
            Console.WriteLine(number);
            return number;
        }

        private int GenerateUniqueDigits(int firstDigit, int secondDigit, int thirdDigit)
        {
            int number = -1;

            do
            {
                number = _random.Next(9) + 0;
            } while (number == firstDigit || number == secondDigit || number == thirdDigit);

            return number;
        }

        private string ValidInput()
        {
            string result = "";
            bool flag = false;
            do
            {
                result = Console.ReadLine();

                if (result.Length != 4 ||
                    result[0] == result[1] ||
                    result[0] == result[2] ||
                    result[0] == result[3] ||
                    result[1] == result[2] ||
                    result[1] == result[3] ||
                    result[2] == result[3])
                {
                    Console.WriteLine("Invalid input");
                }
                else
                {
                    flag = true;
                }
            } while (!flag);

            return result;
        }
    }
}