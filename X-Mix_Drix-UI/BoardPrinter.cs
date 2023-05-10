using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex02;
namespace X_Mix_Drix_UI
{
    public class BoardPrinter
    {
        private readonly ushort m_BoardSize;

        private GameBoard m_GameBoard;

        public BoardPrinter(ushort boardSize)
        {
            m_BoardSize = boardSize;
            m_GameBoard = new GameBoard(boardSize);
        }

        public void PrintGameBoard()
        {
            StringBuilder boardAsString = new StringBuilder(getFirstRow());

            for(int i = 0; i < m_BoardSize; i++)
            {
                boardAsString.Append(i + 1).Append('|');
                for(int j = 0; j < m_BoardSize; j++)
                {
                    boardAsString.Append(string.Format(" {0} |", m_GameBoard.GetBoardCellValue(i, j)));
                }
                boardAsString.Append('\n');
                boardAsString.Append(getRowSeperator());

            }

            Console.WriteLine(boardAsString.ToString());
        }

        private string getRowSeperator()
        {
            string res = " ";
            for(int i = 0; i < 4*m_BoardSize + 1; i++)
            {
                res += "=";
            }
            return res + "\n";
        }

        private string getFirstRow()
        {
            string res = "  ";
            for (int i = 0; i < m_BoardSize; i++)
            {
                res += i + 1;
                res += "   ";
            }
            return res + "\n";
        }
    }
}
