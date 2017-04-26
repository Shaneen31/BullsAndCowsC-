using System;
using System.Collections.Generic;
using System.IO;

namespace Cours
{
	public class BullsCowsGame
	{
		private string m_hiddenWord;
		private int m_currentTry;
		private bool m_isWon;
		private int m_bulls;
		private int m_cows;
		private readonly List<string>m_dictionary = new List<string>();

		public enum EGuessStatus
		{

			WrongLengh,
			NotLowerCase,
			NotIsogram,
			Ok
		}

		/************************************************************
			Constructor
		************************************************************/
		public BullsCowsGame()
		{
			LoadDictionary();
			Reset();
		}

		/************************************************************
			Getters
		************************************************************/
		public int HiddenWordLenght => m_hiddenWord.Length;

		public int CurrentTry => m_currentTry;

		public int GetMaxTries => 4*m_hiddenWord.Length - 8;

		public int BullsCount => m_bulls;

		public int CowsCount => m_cows;

		public bool IsGameWon => m_isWon;

		/************************************************************
			Methods
		************************************************************/
		void LoadDictionary()
		{
			string line;

			StreamReader file = new StreamReader("dictionary.txt");
			while ((line = file.ReadLine()) != null)
			{
				m_dictionary.Add(line);
			}
		}

		string SelectRandomWord()
		{
			int WordIndex;
			Random random = new Random();

			WordIndex = random.Next(1, m_dictionary.Count);

			return m_dictionary[WordIndex - 1];
		}


		public void Reset()
		{
			m_hiddenWord = SelectRandomWord();
			m_currentTry = 1;
			m_isWon = false;
		}

		/// <summary>
		/// Receive a player guess and test it for validation.
		/// </summary>
		/// <param name="guess">string comming from the player entry</param>
		/// <returns>Enum value according to status</returns>
		public EGuessStatus ValidateGuess(string guess)
		{
			if (guess.Length != m_hiddenWord.Length)
			{
				return EGuessStatus.WrongLengh;
			}
			else if (!IsLowerCase(guess))
			{
				return EGuessStatus.NotLowerCase;
			}
			else if (!IsIsogram(guess))
			{
				return EGuessStatus.NotIsogram;
			}
			else
			{
				return EGuessStatus.Ok;
			}
		}

		/// <summary>
		/// Check if player guess is lower case
		/// </summary>
		/// <param name="guess"></param>
		/// <returns></returns>
		bool IsLowerCase(string guess)
		{
			foreach (char letter in guess)
			{
				if (!char.IsLower(letter))
				{
					return false;
  				}
 			}
			return true;
		}

		/// <summary>
		/// Check if player guess is an isogram
		/// </summary>
		/// <param name="guess"></param>
		/// <returns></returns>
		bool IsIsogram(string guess)
		{
			if (guess.Length == 1)
			{
				return true;
			}

			List<char>letterList = new List<char>();

			foreach (char letter in guess)
			{
				if (letterList.Contains(letter))
				{
					return false;
				}
				else
				{
					letterList.Add(letter);
				}
			}
			return true;
		}

		/// <summary>
		/// Count Bulls & Cows in player's Guess
		/// </summary>
		/// <param name="guess"></param>
		/// <returns>bool</returns>
		public bool EndGame(string guess)
		{
			m_bulls = 0;
			m_cows = 0;

			for (int i = 0; i < HiddenWordLenght; i++)
			{
				for (int j = 0; j < HiddenWordLenght; j++)
				{
					if (m_hiddenWord[i].Equals(guess[j]))
					{
						if (i == j)
						{
							m_bulls++;
						}
						else
						{
							m_cows++;
						}
					}
				}
			}

			m_currentTry++;
			return m_isWon = m_bulls == HiddenWordLenght;
		}
	}
}