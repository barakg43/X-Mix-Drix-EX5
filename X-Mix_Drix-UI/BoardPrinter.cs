using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Ex02;
namespace X_Mix_Drix_UI
{
    public class BoardPrinter
    {
        private readonly ushort m_BoardSize;


        public BoardPrinter(ushort boardSize)
        {
            m_BoardSize = boardSize;
        }

        public void PrintGameBoard(GameBoard.Cell[,] i_Board)
        {
            StringBuilder boardAsString = new StringBuilder(getFirstRow());
            //int boardSize = i_Board.GetLength(0);

            for(int i = 0; i < m_BoardSize; i++)
            {
                boardAsString.Append(i + 1).Append('|');
                for(int j = 0; j < m_BoardSize; j++)
                {
                    boardAsString.Append(string.Format(" {0} |", readCellValue(i_Board[i, j].Value)));
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

        private string readCellValue(eBoardCellValue i_Value)
        {
            string value;
            if (i_Value == eBoardCellValue.Empty)
            {
                value = " ";
            }
            else
            {
                value = i_Value.ToString();
            }

            return value;
        }
    }
}
