using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace X_Mix_Drix_UI
{
    class Menu
    {
        const char k_Quit = 'Q';
        private const string k_InvalidInputMsg = "The input you entered is invalid. Please try again.";
        public enum eMenuOptions
        {
            StartGameAgainstPC = 1,
            StartGameAgaintsPlayer,
            Quit,
        }

        public enum eEndOfGameOptions
        {
            Continue = 1,
            Finish

        }

        public void PrintMainMenu()
        {
            Ex02.ConsoleUtils.Screen.Clear();
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

        public void PrintCurrentPlayerTurn(string i_PlayerName)
        {
            Console.WriteLine(string.Format("It's {0} turn", i_PlayerName));
        }

        public eMenuOptions GetAndCheckUserInputForMenuItem()
        {
            bool inputIsInvalid = true;
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
        public CellBoardCoordinate GetAndCheckUserInputForTurnDataMove(ref bool i_CurrentPlayerWantsToQuit, eCellError cellError)
        {
            string[] rowColRawData;
            string inputData;
            ushort selectRow=0;
            ushort selectedColumn=0;
            char singleLetterInput;//might cause runtime error

            const string k_InputMsg = @"please enter board coordination to place your mark. 
enter two number in <row> <column> format with space between the number ";

            string currentMsgToUser= k_InputMsg;
            if(cellError != eCellError.NoError)
            {
                currentMsgToUser = checkCellError(cellError);
            }

            bool inputIsInvalid;
            do
            {
                Console.WriteLine(currentMsgToUser);
                inputData = Console.ReadLine();
                rowColRawData = inputData.Split();
                if (rowColRawData.Length == 1)
                {
                    char.TryParse(rowColRawData[0], out singleLetterInput);
                    inputIsInvalid = char.ToUpper(singleLetterInput) != k_Quit;
                    i_CurrentPlayerWantsToQuit = !inputIsInvalid;
                }
                else
                {
                    inputIsInvalid = rowColRawData.Length != 2 || !ushort.TryParse(rowColRawData[0], out selectRow)
                                                           || !ushort.TryParse(rowColRawData[1], out selectedColumn);
                }

                if (inputIsInvalid)
                {
                    currentMsgToUser = k_InvalidInputMsg;
                }
            }
            while (inputIsInvalid);

            return new CellBoardCoordinate(selectRow,selectedColumn);
        }
    

        public int GetAndCheckUserInputForBoardSize(int i_MinSize, int i_MaxSize)
        {
            bool inputIsInvalid = true;
            int userInput = 0;
            Console.WriteLine(string.Format("Please select a board size between {0} and {1}", i_MinSize, i_MaxSize));
            while (inputIsInvalid)
            {
                int.TryParse(Console.ReadLine(), out userInput);// check if number
                if (userInput >= i_MinSize && userInput <= i_MaxSize)
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
        public bool GetEndOfGameInput()
        {
            bool inputIsInvalid = true;
            int userInput = 0;
            Console.WriteLine(string.Format(
@"1. Start another match
2. Finish game

Select an option by entering its number"));
            while (inputIsInvalid)
            {
                int.TryParse(Console.ReadLine(), out userInput);// check if number
                if (Enum.IsDefined(typeof(eEndOfGameOptions), userInput))
                {
                    inputIsInvalid = false;
                }
                else
                {
                    Console.WriteLine(k_InvalidInputMsg);
                }
            }
            return (eEndOfGameOptions)userInput == eEndOfGameOptions.Finish;

        }

        private string checkCellError(eCellError cellError)
        {
            string res;
            if(cellError == eCellError.CellNotEmpty)
            {
                res = "Cell is not empty";
            }
            else if(cellError == eCellError.CellOutOfRange)
            {
                res = "Cell is out of range";
            }
            else
            {
                res = "Cant erase used cell";
            }
            return res;
        }
    }
}
