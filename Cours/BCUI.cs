using System;
using System.Collections.Generic;
using System.Security.Policy;
using CowLevel.Data;
using CowLevel.CoreGame;

namespace CowLevel.Manager
{
	internal class Program
	{
		private static readonly Version s_gameVersion = new Version("0.3.0");
		private static List<string> s_dictionary;
		
		//Tools
		static void Line(object _value)
		{
			Console.WriteLine(_value);
		}

		// Entry Point
		public static void Main(string[] args)
		{
			LoadDictionary();
			PrintIntro(StartGame);
		}

		private static void LoadDictionary()
		{
			BCData _data = new BCData();
			s_dictionary = _data.LoadData("dictionary.txt");
		}

		// Print Game Title and Intro.
		static void PrintIntro(Action _callback)
		{
			string _welcomeUi =   "\n	Welcome to Bulls and Cows			\n"
			                    + "											\n"
			                    + "          }   {         ___				\n"
								+ "          (o o)        (o o)				\n"
								+ "   /-------\\ /          \\ /-------\\	\n"
								+ "  / | BULL |O            O| COW  | \\	\n"
								+ " *  |-,--- |              |------|  *	\n"
								+ "    ^      ^              ^      ^		\n"
								+ "C# version : " + string.Format("{0}.{1}.{2}", s_gameVersion.Major, s_gameVersion.Minor, s_gameVersion.Build)
								+ "\n\n";

			Line(_welcomeUi);
			_callback();
		}

		// Main Loop of the game.
		static void StartGame()
		{
			BullsCowsGame _game = new BullsCowsGame("planet"/*s_dictionary[new Random().Next(s_dictionary.Count)]*/);

			string _startGameLabel = String.Format("Can you guess the {0} letters isogram I'm thinking of?", _game.HiddenWordLenght);
			Line(_startGameLabel);

			while (_game.CurrentTry <= _game.GetMaxTries && !_game.IsWon)
			{
				_game.EndGame(GetGuess(_game));
				Line(String.Format("Bulls: {0} Cows: {1}", _game.BullsCount, _game.CowsCount));
			}
			Line(_game.IsWon ?"You won !":"You lose!");
			ReloadGame(StartGame);
		}

		// Ask the player for a Guess and validate it.
		static string GetGuess(BullsCowsGame _game)
		{
			string _guess="";
			BullsCowsGame.EGuessStatus _valid;
			do
			{
				Line(string.Format("Try {0} of {1} - Please enter your guess:", _game.CurrentTry, _game.GetMaxTries));
				_guess = Console.ReadLine();
				_valid = _game.ValidateGuess(_guess);

				switch (_valid)
				{
					case  BullsCowsGame.EGuessStatus.WrongLengh:
						Line("Bulls: " + _game.BullsCount + " - Cows: " + _game.CowsCount);
						break;
					case BullsCowsGame.EGuessStatus.NotIsogram:
						Line("Please enter a word witout repeating letters.");
						break;
				}
			} while (_valid != BullsCowsGame.EGuessStatus.Ok);

			return !string.IsNullOrEmpty(_guess) ? _guess.ToLower() : "";
		}

		//Ask the player if he wants to play again.
		private static bool ReloadGame(Action _onReloadMethod)
		{
			Line("Do you want to play again ? Y/N");
			string _answer = Console.ReadLine();

			while (!string.IsNullOrEmpty(_answer) && (_answer.ToLower() != "y" || _answer.ToLower() != "n"))
			{
				if (_answer.ToLower() == "y") _onReloadMethod();
				else if (_answer.ToLower() == "n") Environment.Exit(0);
				else
				{
					Line("Wrong answer !");
					Line("Do you want to play again ? Y/N");
					_answer = Console.ReadLine();
				}
			}

			return false;
		}
	}
}