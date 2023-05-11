using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X_Mix_Drix_UI
{
    class Menu
    {
        public enum eMenuOptions
        {
            StartGameAgainstPC = 1,
            StartGameAgaintsPlayer,
            Quit,
        }

        public void PrintMainMenu()
        {
            Console.WriteLine(string.Format(
@"Revrse X-MIX_DRIX!!

1. Start game against PC

2. Start game against another player


3. Quit

Select an option by entering its number"));
        }

        public void PrintSizeSelect()
        {
            Console.WriteLine(string.Format(
@"Please select a board size from 5-9"));
        }

        public eMenuOptions GetAndCheckUserInputForMenuItem()
        {
            bool inputIsInvalid = true;
            const string k_InvalidInputMsg = "The input you entered is invalid. Please try again.";//set as member?
            int userInput = 0;
            while (inputIsInvalid)
            {
                int.TryParse(Console.ReadLine(), out userInput);// check if number
                if (Enum.IsDefined(typeof(eMenuOptions), userInput))
                {
                    inputIsInvalid = false;
                }
                else
                {
                    Console.WriteLine(k_InvalidInputMsg);
                }
            }
            return (eMenuOptions)userInput;
        }

        public int GetAndCheckUserInputForBoardSize()
        {
            bool inputIsInvalid = true;
            const string k_InvalidInputMsg = "The input you entered is invalid. Please try again.";//set as member?
            int userInput = 0;
            while (inputIsInvalid)
            {
                int.TryParse(Console.ReadLine(), out userInput);// check if number
                if (userInput >= 3 && userInput <= 9)
                {
                    inputIsInvalid = false;
                }
                else
                {
                    Console.WriteLine(k_InvalidInputMsg);
                }
            }
            return userInput;
        }
    }
}
