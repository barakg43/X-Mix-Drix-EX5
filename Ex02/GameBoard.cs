using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{

   
    public class GameBoard
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
                for(int col = 0; col < r_BoardSize; col++)
                {
                    m_BoardMatrixCells[row, col].Value = eBoardCellValue.Empty;
                }
            }
        }

        public bool IsBoardHaveRowFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInRow = 0;
            bool isOneRowFilledWithSingleValue = false;

            for (ushort col = 0; col < r_BoardSize && !isOneRowFilledWithSingleValue; col++)
            {
                for (ushort row = 0; row < r_BoardSize; row++)
                {
                    increaseCounterIfCellContainValue(row, col, i_ValueToCheck, ref countValueInRow);

                }
                isOneRowFilledWithSingleValue = countValueInRow == r_BoardSize;
            }

            return isOneRowFilledWithSingleValue;
        }
        public bool IsBoardHaveColumnFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInColumn = 0;
            bool isOneColFilledWithSingleValue = false;

            for (ushort row = 0; row < r_BoardSize && !isOneColFilledWithSingleValue; row++)
            {
                for (ushort col = 0; col < r_BoardSize; col++)
                {
                    increaseCounterIfCellContainValue(row, col, i_ValueToCheck, ref countValueInColumn);
                }
                isOneColFilledWithSingleValue = countValueInColumn == r_BoardSize;
            }

            return isOneColFilledWithSingleValue;
        }

        public bool IsBoardHaveDiagonalFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInDiagonal = 0, countValueInAntiDiagonal=0;
             
            for (ushort i = 0; i < r_BoardSize ; i++)
            {
                increaseCounterIfCellContainValue(i, i, i_ValueToCheck, ref countValueInDiagonal);
                increaseCounterIfCellContainValue(i, (ushort)(r_BoardSize - i), i_ValueToCheck, ref countValueInAntiDiagonal);
            }

            return countValueInDiagonal == r_BoardSize || countValueInAntiDiagonal == r_BoardSize;
        }

        private void increaseCounterIfCellContainValue(ushort i_Row, ushort i_Column, eBoardCellValue i_ValueToCheck, ref ushort i_ValueCounter)
        {
            if (m_BoardMatrixCells[i_Row, i_Column].Value == i_ValueToCheck)
            {
                i_ValueCounter++;
            }
        }
        public bool IsAllBoardFilled()
        {
            bool isBoardFilled = true;

            for (int row = 0; row < m_BoardMatrixCells.GetLength(0)  && isBoardFilled; row++)
            {
                for (int col = 0; col < m_BoardMatrixCells.GetLength(1) && isBoardFilled; col++)
                {
                    if(m_BoardMatrixCells[row, col].Value == eBoardCellValue.Empty)
                        isBoardFilled = false;
                }
            }

            return isBoardFilled;
        }


        public bool ChangeValueIfEmptyCell(MoveData i_MoveData)
        {
            bool cellIsEmpty = m_BoardMatrixCells[i_MoveData.SelectedRow, i_MoveData.SelectedRow].Value == eBoardCellValue.Empty;

            if(cellIsEmpty)
            {
                m_BoardMatrixCells[i_MoveData.SelectedRow, i_MoveData.SelectedRow].Value=i_MoveData.CellValue;
            }

            return cellIsEmpty;
        }

        public bool IsValidAndEmptyCell(ushort i_Row, ushort i_Column)
        {
            return i_Row < r_BoardSize &&
                   i_Column < r_BoardSize &&
                   m_BoardMatrixCells[i_Row, i_Column].Value == eBoardCellValue.Empty;
        }

        public eBoardCellValue[,] GetCurrnetBoardState()
        {
            eBoardCellValue[,] currentBoard = new eBoardCellValue[r_BoardSize, r_BoardSize];

            for(int row = 0; row < r_BoardSize; row++)
            {
                for(int col = 0; col < r_BoardSize; col++)
                {
                    currentBoard[row, col] = m_BoardMatrixCells[row, col].Value;
                }

            }

            return currentBoard;
        }

        public string GetBoardCellValue(int i, int j)
        {
            string value;
            if(m_BoardMatrixCells[i, j].Value == eBoardCellValue.Empty)
            {
                value = " ";
            }
            else
            {
                value = m_BoardMatrixCells[i, j].Value.ToString();
            }

            return value;
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
