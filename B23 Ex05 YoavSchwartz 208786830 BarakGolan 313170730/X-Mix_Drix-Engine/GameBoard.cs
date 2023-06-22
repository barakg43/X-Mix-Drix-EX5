using System;

namespace Engine
{
    public class GameBoard
    {
        private readonly ushort r_BoardSize;
        private Cell[,] m_BoardMatrixCells;
        private int m_FilledCellAmount;

        public GameBoard(ushort i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            InitializeEmptyBoard();
        }

        public struct Cell
        {
            public eBoardCellValue Value { get; set; }
        }

        public void InitializeEmptyBoard()
        {
            m_BoardMatrixCells = new Cell[r_BoardSize, r_BoardSize];
            m_FilledCellAmount = 0;
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    m_BoardMatrixCells[row, col].Value = eBoardCellValue.Empty;
                }
            }
        }

        private bool isBoardHaveRowFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInRow;
            bool isOneRowFilledWithSingleValue = false;

            for (ushort col = 0; col < r_BoardSize && !isOneRowFilledWithSingleValue; col++)
            {
                countValueInRow = 0;
                for (ushort row = 0; row < r_BoardSize; row++)
                {
                    increaseCounterIfCellContainValue(row, col, i_ValueToCheck, ref countValueInRow);
                }

                isOneRowFilledWithSingleValue = countValueInRow == r_BoardSize;
            }

            return isOneRowFilledWithSingleValue;
        }

        private bool isBoardHaveColumnFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInColumn;
            bool isOneColFilledWithSingleValue = false;

            for (ushort row = 0; row < r_BoardSize && !isOneColFilledWithSingleValue; row++)
            {
                countValueInColumn = 0;
                for (ushort col = 0; col < r_BoardSize; col++)
                {
                    increaseCounterIfCellContainValue(row, col, i_ValueToCheck, ref countValueInColumn);
                }

                isOneColFilledWithSingleValue = countValueInColumn == r_BoardSize;
            }

            return isOneColFilledWithSingleValue;
        }

        private bool isBoardHaveDiagonalFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInDiagonal = 0, countValueInAntiDiagonal = 0;

            for (ushort i = 0; i < r_BoardSize; i++)
            {
                increaseCounterIfCellContainValue(i, i, i_ValueToCheck, ref countValueInDiagonal);
                increaseCounterIfCellContainValue(i, (ushort)(r_BoardSize - i - 1), i_ValueToCheck, ref countValueInAntiDiagonal);
            }

            return countValueInDiagonal == r_BoardSize || countValueInAntiDiagonal == r_BoardSize;
        }

        public bool IsBoardHaveAnyRowColumnDiagonalFilled(eBoardCellValue i_ValueToCheck)
        {
            return isBoardHaveRowFilledWithValue(i_ValueToCheck)
                   || isBoardHaveColumnFilledWithValue(i_ValueToCheck)
                   || isBoardHaveDiagonalFilledWithValue(i_ValueToCheck);
        }

        private void increaseCounterIfCellContainValue(
            ushort i_Row,
            ushort i_Column,
            eBoardCellValue i_ValueToCheck,
            ref ushort i_ValueCounter)
        {
            if (m_BoardMatrixCells[i_Row, i_Column].Value == i_ValueToCheck)
            {
                i_ValueCounter++;
            }
        }

        public bool IsAllBoardFilled()
        {
            return m_FilledCellAmount == r_BoardSize * r_BoardSize;
        }

        public void ChangeValueIfEmptyCell(MoveData i_MoveData)
        {
            int row = i_MoveData.CellCoordinate.SelectedRow - 1;
            int col = i_MoveData.CellCoordinate.SelectedColumn - 1;
            bool cellIsEmpty =
                m_BoardMatrixCells[row, col].Value == eBoardCellValue.Empty;

            if (cellIsEmpty)
            {
                m_BoardMatrixCells[row, col].Value = i_MoveData.CellValue;
                m_FilledCellAmount++;
            }
            else
            {
                throw new InvalidOperationException($"Cell({row + 1},{col + 1}) is not empty");
            }

        }

        public void CheckIfValidAndEmptyCell(MoveData i_Data)
        {
            if (!(i_Data.CellCoordinate.SelectedRow <= r_BoardSize && i_Data.CellCoordinate.SelectedColumn <= r_BoardSize && i_Data.CellCoordinate.SelectedRow > 0 && i_Data.CellCoordinate.SelectedColumn > 0))
            {
                throw new IndexOutOfRangeException("Selected coordinate is out of board range");
            }

            if (m_BoardMatrixCells[i_Data.CellCoordinate.SelectedRow - 1, i_Data.CellCoordinate.SelectedColumn - 1].Value != eBoardCellValue.Empty)
            {
                throw new InvalidOperationException("Cell is not empty");
            }

            if (i_Data.CellValue == eBoardCellValue.Empty)
            {
                throw new InvalidOperationException("cannot erase not empty Cell");
            }
        }

        public eBoardCellValue[,] GetCurrentBoardState()
        {
            eBoardCellValue[,] currentBoard = new eBoardCellValue[r_BoardSize, r_BoardSize];

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    currentBoard[row, col] = m_BoardMatrixCells[row, col].Value;
                }
            }

            return currentBoard;
        }

        public Cell[,] GetBoard()
        {
            return m_BoardMatrixCells;
        }

        public ushort BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }
    }
}
