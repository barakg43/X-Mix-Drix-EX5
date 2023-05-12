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
                /*if (Enum.IsDefined(typeof(eMenuOptions), userInput))
                {
                    inputIsInvalid = false;
                }
                else
                {
                    Console.WriteLine(k_InvalidInputMsg);
                }*/
                if (inputIsInvalid = !Enum.IsDefined(typeof(eMenuOptions), userInput))
                {
                    Console.WriteLine(k_InvalidInputMsg);
                }
            }
            return (eMenuOptions)userInput;
        }
        public MoveData GetAndCheckUserInputForTurnDataMove(ref bool r_PlayerWantsToQuit)// either return move data or q
        {
            bool inputIsInvalid;
            string[] rowColRawData;
            string inputData;
            char singleLetterInput;
            ushort selectRow=0;
            ushort selectedColumn=0;
            

            const string k_InvalidInputMsg = "The input you entered is invalid. Please try again.";//set as member?
            const string k_InputMsg = @"please enter board coordination to place your mark. 
enter two number in <row> <column> format with space between the numbers ";

            string currentMsgToUser= k_InputMsg;
            do
            {
                Console.WriteLine(currentMsgToUser);
                inputData = Console.ReadLine();
                rowColRawData = inputData.Split();
                if(rowColRawData.Length == 1)
                {
                    char.TryParse(rowColRawData[0], out singleLetterInput);
                    inputIsInvalid = char.ToUpper(singleLetterInput) != k_Quit;
                    r_PlayerWantsToQuit = !inputIsInvalid;
                }
                else
                {
                    inputIsInvalid = rowColRawData.Length != 2 || !ushort.TryParse(rowColRawData[0], out selectRow)
                                                           || !ushort.TryParse(rowColRawData[1], out selectedColumn);
                }

                if(inputIsInvalid)
                {

                    currentMsgToUser = k_InvalidInputMsg;
                }
            }
            while(inputIsInvalid);

            return new MoveData(selectRow,selectedColumn,eBoardCellValue.Empty);
        }
    

    public int GetAndCheckUserInputForBoardSize()
        {
            bool inputIsInvalid = true;
            const string k_InvalidInputMsg = "The input you entered is invalid. Please try again.";//set as member?
            int userInput = 0;
            while (inputIsInvalid)
            {
                int.TryParse(Console.ReadLine(), out userInput);
                /*if (userInput >= 3 && userInput <= 9)
                {
                    inputIsInvalid = false;
                }
                else
                {
                    Console.WriteLine(k_InvalidInputMsg);
                }*/
                if(inputIsInvalid = !(userInput >= 3 && userInput <= 9))
                {
                    Console.WriteLine(k_InvalidInputMsg);
                }
                
            }
            return userInput;
        }
    }
}
