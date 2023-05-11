using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace X_Mix_Drix_UI
{
    class Menu
    {
        private void printGameMenu()
        {
            StringBuilder menuStringBuilder = new StringBuilder();

            menuStringBuilder.AppendLine("####################################");
            menuStringBuilder.AppendLine("# 1.  Play against other player    #");
            menuStringBuilder.AppendLine("# 2.  Play against other Computer  #");
            menuStringBuilder.AppendLine("# 3.  Exit                         #");
            menuStringBuilder.AppendLine("####################################");
            Console.Write(menuStringBuilder.ToString());
        }

        public ushort GetGameBoardSizeFromUser(bool toPrintInputError)
        {
            string stringNumberRaw;
            bool isSuccessParseString;
            int minBoardSizeAllow = (int)eBoardSizeError.MinSize;
            int maxBoardSizeAllow = (int)eBoardSizeError.MaxSize;

            if (toPrintInputError)
            {
                Console.Write($"invalid size-must between {minBoardSizeAllow} to {maxBoardSizeAllow} ,try again and press enter:");
            }
            else
            {
                Console.Write(
                    $"Please enter the desire game board size (between {minBoardSizeAllow} to {maxBoardSizeAllow}) and press enter:");
            }
            stringNumberRaw = Console.ReadLine();
            isSuccessParseString = ushort.TryParse(stringNumberRaw, out ushort o_boardSize);
            while (!isSuccessParseString || o_boardSize < 0)
            {
                Console.Write("invalid size-must be non negative integer ,try again and press enter:");
                stringNumberRaw = Console.ReadLine();
                isSuccessParseString = ushort.TryParse(stringNumberRaw, out o_boardSize);
            }

            return o_boardSize;
        }
        public eMenuOptions getValidMenuOptionFromUser()
        {
            eMenuOptions selectedOption;
            String stringNumberRaw;
            bool isInputIsNumber;
            int optionNumber;
            int minOptionNumber = (int)eMenuOptions.StartGameAgainstPc + 1;
            int maxOptionNumber = (int)eMenuOptions.Quit + 1;

            Console.WriteLine(
                String.Format("please choose one of the options below(between %d to %d)",
                    minOptionNumber, maxOptionNumber)
                );
            printGameMenu();
            stringNumberRaw = Console.ReadLine();

            isInputIsNumber = int.TryParse(stringNumberRaw, out optionNumber);
            while(!isInputIsNumber || optionNumber < minOptionNumber || optionNumber > maxOptionNumber)
            {
                Console.Write("invalid option number,try again and press enter:");
                stringNumberRaw = Console.ReadLine();
                isInputIsNumber = int.TryParse(stringNumberRaw, out optionNumber);
            }

            selectedOption = (eMenuOptions)optionNumber;

            return selectedOption;
        }

    
    }
}
