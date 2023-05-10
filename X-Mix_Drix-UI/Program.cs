using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ex02.ConsoleUtils;
namespace X_Mix_Drix_UI
{
    class Program
    {
        public static void Main()
        {
            char[,] board = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
            BoardPrinter printer = new BoardPrinter(3, board);
            printer.PrintGameBoard();
        }
    }
}
