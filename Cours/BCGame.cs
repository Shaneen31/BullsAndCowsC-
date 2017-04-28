using System;
using System.Collections.Generic;
using System.IO;

namespace CowLevel.CoreGame
{
	public class BullsCowsGame
	{
		private readonly string m_hiddenWord;
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
		public BullsCowsGame(string _initWord)
		{
			m_hiddenWord = _initWord;
			m_currentTry = 1;
			m_isWon = false;
		}

		/************************************************************
			Getters
		************************************************************/
		public int HiddenWordLenght => m_hiddenWord.Length;

		public int CurrentTry => m_currentTry;

		public int GetMaxTries => 4*m_hiddenWord.Length - 8;

		public int BullsCount => m_bulls;

		public int CowsCount => m_cows;

		public bool IsWon => m_isWon;

		/************************************************************
			Methods
		************************************************************/
		/// <summary>
		/// Receive a player guess and test it for validation.
		/// </summary>
		public EGuessStatus ValidateGuess(string _guess)
		{
			if (_guess.Length != m_hiddenWord.Length)
			{
				return EGuessStatus.WrongLengh;
			}
			else if (!IsLowerCase(_guess))
			{
				return EGuessStatus.NotLowerCase;
			}
			else if (!IsIsogram(_guess))
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
		bool IsLowerCase(string _guess)
		{
			foreach (char _letter in _guess)
			{
				if (!char.IsLower(_letter))
				{
					return false;
  				}
 			}
			return true;
		}

		/// <summary>
		/// Check if player guess is an isogram
		/// </summary>
		bool IsIsogram(string _guess)
		{
			if (_guess.Length == 1)
			{
				return true;
			}

			List<char>_letterList = new List<char>();

			foreach (char _letter in _guess)
			{
				if (_letterList.Contains(_letter))
				{
					return false;
				}
				else
				{
					_letterList.Add(_letter);
				}
			}
			return true;
		}

		/// <summary>
		/// Count Bulls & Cows in player's Guess
		/// </summary>
		public bool EndGame(string _guess)
		{
			m_bulls = 0;
			m_cows = 0;

			for (int i = 0; i < HiddenWordLenght; i++)
			{
				for (int j = 0; j < HiddenWordLenght; j++)
				{
					if (m_hiddenWord[i].Equals(_guess[j]))
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