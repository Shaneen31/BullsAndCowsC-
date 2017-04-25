using System.Collections.Generic;

namespace Cours
{
	public class BullsCowsGame
	{
		private string m_hiddenWord;
		private int m_currentTry;
		private bool m_isWon;
		private int m_bulls;
		private int m_cows;
        
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
			Reset();
		}

		/************************************************************
			Getters
		************************************************************/
		public int HiddenWordLenght => m_hiddenWord.Length;

		public int CurrentTry => m_currentTry;

		// TODO Add multiple value according to HiddenWord Lenght
		public int GetMaxTries => m_hiddenWord.Length + 1;

		public int BullsCount => m_bulls;

		public int CowsCount => m_cows;

		public bool IsGameWon => m_isWon;

		/************************************************************
			Methods
		************************************************************/
		public void Reset()
		{
			m_hiddenWord = "planet";
			m_currentTry = 1;
			m_isWon = false;
		}

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

		// Check if player guess is lower case
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

		// Check if player guess is an isogram
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

		// Count Bulls & Cows in player's Guess
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