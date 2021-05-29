using System;

namespace Bulls_And_Cows
{
    public static class GameEngine
    {
        public static void StartGame()
        { 
            Computer computer = new Computer();
            Player player = new Player(computer.Number);
            
            string nextToPlay = "player";
            bool winner = false;

            while (!winner)
            {
                switch (nextToPlay)
                {
                    case "player":
                        Console.WriteLine("Player's turn (try to guess the number of the computer)");
                        winner = player.Play();
                        nextToPlay = "computer";
                        break;
                    case "computer":
                        try
                        {
                            winner = computer.Play();
                            nextToPlay = "player";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("You have lied");
                            winner = true;
                        }
                        break;
                }
            }
        }
    }
}