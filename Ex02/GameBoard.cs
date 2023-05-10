using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{

   
    class GameBoard
    {
        private readonly ushort r_BoardSize;
        private Cell[,] m_BoardMatrixCells;

        public GameBoard(ushort i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            initializeEmptyBoard();
        }


        private struct Cell
        {
            public eBoardCellValue Value { get; set; }
        } 

        private void initializeEmptyBoard()
        {
            m_BoardMatrixCells = new Cell[r_BoardSize, r_BoardSize];

            for (int row = 0; row < r_BoardSize; row++)
            {
                for(int col = 0; col < r_BoardSize; row++)
                {
                    m_BoardMatrixCells[row, col].Value = eBoardCellValue.Empty;
                }
            }
        }

        public bool IsBoardHaveRow(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueStrike = 0;
            bool isHaveStrike = false;

            for (int row = 0; row < r_BoardSize && !isHaveStrike; row++)
            {
                for (int col = 0; col < r_BoardSize && !isHaveStrike; row++)
                {
                    if(m_BoardMatrixCells[row, col].Value == i_ValueToCheck)
                        countValueStrike++;
                }

                isHaveStrike = countValueStrike == r_BoardSize;
            }

            return isHaveStrike;
        }

        public bool isAllBoardFilled()
        {
            bool isBoardFilled = true;

            for (int row = 0; row < m_BoardMatrixCells.GetLength(0)  && isBoardFilled; row++)
            {
                for (int col = 0; col < m_BoardMatrixCells.GetLength(1) && isBoardFilled; row++)
                {
                    if(m_BoardMatrixCells[row, col].Value == eBoardCellValue.Empty)
                        isBoardFilled = false;
                }
            }

            return isBoardFilled;
        }


        public eBoardCellValue[,] GetCurrnetBoardState()
        {
            eBoardCellValue[,] currentBoard = new eBoardCellValue[r_BoardSize, r_BoardSize];

            for(int row = 0; row < r_BoardSize; row++)
            {
                for(int col = 0; col < r_BoardSize; row++)
                {
                    currentBoard[row, col] = m_BoardMatrixCells[row, col].Value;
                }

            }

            return currentBoard;
        }

        public int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }
    }
}
