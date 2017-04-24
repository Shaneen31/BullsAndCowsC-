using System.Collections.Generic;

namespace Cours
{
    public class BullsCowsGame
    {
        private string _hiddenWord;
        private int _currentTry;
        private bool _isWon;
        private int _bulls;
        private int _cows;
        
        public enum EGuessStatus
        {
            Invalid,
            Ok,
            WrongLengh,
            NotLowerCase,
            NotIsogram
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
        public int GetHiddenWordLenght => _hiddenWord.Length;

        public int GetCurrentTry => _currentTry;

        // TODO Add multiple value according to HiddenWord Lenght
        public int GetMaxTries => _hiddenWord.Length + 1;

        public int GetBullsCount => _bulls;

        public int GetConwsCount => _cows;

        public bool IsGameWon => _isWon;

        /************************************************************
            Private Methods
        ************************************************************/
        public void Reset()
        {
            _hiddenWord = "planet";
            _currentTry = 1;
            _isWon = false;
        }

        public EGuessStatus ValidateGuess(string guess)
        {
            if (guess.Length != _hiddenWord.Length)
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

        // TODO Count Bulls & Cows in player's Guess
        public void SubmitGuess(string guess)
        {
            _bulls = 0;
            _cows = 0;

            for (int i = 0; i < GetHiddenWordLenght; i++)
            {
                for (int j = 0; j < GetHiddenWordLenght; j++)
                {
                    if (_hiddenWord[i] == guess[j])
                    {
                        if (i == j)
                        {
                            _bulls++;
                        }
                        else
                        {
                            _cows++;
                        }
                    }
                }
            }

            if (_bulls == GetHiddenWordLenght)
            {
                _isWon = true;
            }

            _currentTry++;
        }
    }
}