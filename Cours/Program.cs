using System;

namespace Cours
{
    internal class Program
    {
        private const string Version = "0.1.0";

        static readonly BullsCowsGame BcGame = new BullsCowsGame();

        // Entry Point
        public static void Main(string[] args)
        {
            bool bPlayAgain;

            PrintIntro();

            do
            {
                PlayGame();
                bPlayAgain = AskToPlayAgain();
            } while (bPlayAgain);
        }

        // Print Game Title and Intro.
        static void PrintIntro()
        {
            Console.WriteLine("Welcome to Bulls and Cows");
            Console.WriteLine("                            ");
            Console.WriteLine("          }   {         ___ ");
            Console.WriteLine("          (o o)        (o o) ");
            Console.WriteLine("   /-------\\ /          \\ /-------\\ ");
            Console.WriteLine("  / | BULL |O            O| COW  | \\ ");
            Console.WriteLine(" *  |-,--- |              |------|  * ");
            Console.WriteLine("    ^      ^              ^      ^ ");
            Console.WriteLine("C# version: " + Version);
            Console.WriteLine("");
        }

        // Main Loop of the game.
        static void PlayGame()
        {
            Console.WriteLine("Can you guess the "+ BcGame.GetHiddenWordLenght + " letters isogram I'm thinking of?");
            while (BcGame.GetCurrentTry <= BcGame.GetMaxTries && !BcGame.IsGameWon)
            {
                BcGame.SubmitGuess(GetGuess());
                Console.WriteLine("Bulls: " + BcGame.GetBullsCount + " - Cows: " + BcGame.GetConwsCount);
                Console.WriteLine("");
            }

            Console.WriteLine(BcGame.IsGameWon
                ? "Congratulation, you won!"
                : "Bad luck, maybe you'll do better next time.");
        }

        //Ask the player if he wants to play again.
        private static bool AskToPlayAgain()
        {
            Console.WriteLine("Do you want to play again? (y/n)");
            string playAgain = Console.ReadLine();
            if (playAgain != null)
            {
                Console.WriteLine(playAgain[0]);

                if (playAgain[0] == 'y')
                {
                    BcGame.Reset();
                    return true;
                }
            }

            Console.WriteLine("Goodbye!");
            return false;
        }


        // Ask the player for a Guess and validate it.
        private static string GetGuess()
        {
            string guess;
            BullsCowsGame.EGuessStatus valid;

            do
            {
                Console.WriteLine("Try " + BcGame.GetCurrentTry + " of " + BcGame.GetMaxTries + " - Please enter your guess:");
                guess = Console.ReadLine();

                valid = BcGame.ValidateGuess(guess);

                switch (valid)
                {
                    case BullsCowsGame.EGuessStatus.WrongLengh:
                        Console.WriteLine("Please enter a word that is " + BcGame.GetHiddenWordLenght + " letters long.");
                        break;

                    case BullsCowsGame.EGuessStatus.NotLowerCase:
                        Console.WriteLine("Please only use lowercase.");
                        break;

                    case BullsCowsGame.EGuessStatus.NotIsogram:
                        Console.WriteLine("Please enter an isogram.");
                        break;
                }
            } while (valid != BullsCowsGame.EGuessStatus.Ok);

            return guess;
        }
    }
}