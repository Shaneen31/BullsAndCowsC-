using System.Linq;

namespace CowLevel
{
	public class BullsCowsGame
	{
		private readonly string m_hiddenWord;
		public int HiddenWordLength { get { return m_hiddenWord.Length; } }
		public int CurrentTry { get; private set; } = 1;
		public int MaxTries { get { return 4*HiddenWordLength - 8; } }
		public bool IsWon { get; private set; }
		public int BullsCount { get; private set;}
		public int CowsCount { get; private set; }

		public enum GuessStatus
		{
			WrongLength,
			NotIsogram,
			Ok
		}

		/************************************************************
			Constructor
		************************************************************/
		public BullsCowsGame(string _initWord)
		{
			m_hiddenWord = _initWord;
		}

		/************************************************************
			Methods
		************************************************************/
		/// <summary>
		/// Receive a player guess and test it for validation.
		/// </summary>
		public GuessStatus SetGuessStatus(string _guess)
		{
			return _guess.Length != HiddenWordLength ? GuessStatus.WrongLength
				: IsIsogram(_guess) == false ? GuessStatus.NotIsogram
				: GuessStatus.Ok;
		}

		/// <summary>
		/// Check if player guess is an isogram
		/// </summary>
		bool IsIsogram(string _guess)
		{
			bool _isIsogram;

			if (_guess.Length == 1)
			{
				_isIsogram = true;
			}
			else
			{
				_isIsogram = _guess.Length == _guess.Distinct().Count();
			}

			return _isIsogram;
		}

		/// <summary>
		/// Count Bulls & Cows in player's Guess
		/// </summary>
		public bool EndGame(string _guess)
		{
			BullsCount = 0;
			CowsCount = 0;

			for (int i = 0; i < HiddenWordLength; i++)
			{
				for (int j = 0; j < HiddenWordLength; j++)
				{
					if (m_hiddenWord[i].Equals(_guess[j]))
					{
						if (i == j)
						{
							BullsCount++;
						}
						else
						{
							CowsCount++;
						}
					}
				}
			}

			CurrentTry++;
			return IsWon = BullsCount == HiddenWordLength;
		}
	}
}