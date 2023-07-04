using System;

namespace Engine
{
    public class GameBoard
    {
        private readonly Cell[,] r_BoardMatrixCells;
        private int m_FilledCellAmount;

        public GameBoard(ushort i_BoardSize)
        {
            BoardSize = i_BoardSize;
            r_BoardMatrixCells = new Cell[BoardSize, BoardSize];
            InitializeEmptyBoard();
        }

        public ushort BoardSize { get; }

        public void InitializeEmptyBoard()
        {
            m_FilledCellAmount = 0;
            for(int row = 0; row < BoardSize; row++)
            {
                for(int col = 0; col < BoardSize; col++)
                {
                    r_BoardMatrixCells[row, col].Value = eBoardCellValue.Empty;
                }
            }
        }

        private bool isBoardHaveRowFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInRow;
            bool isOneRowFilledWithSingleValue = false;

            for(ushort col = 0; col < BoardSize && !isOneRowFilledWithSingleValue; col++)
            {
                countValueInRow = 0;
                for(ushort row = 0; row < BoardSize; row++)
                {
                    increaseCounterIfCellContainValue(row, col, i_ValueToCheck, ref countValueInRow);
                }

                isOneRowFilledWithSingleValue = countValueInRow == BoardSize;
            }

            return isOneRowFilledWithSingleValue;
        }

        private bool isBoardHaveColumnFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInColumn;
            bool isOneColFilledWithSingleValue = false;

            for(ushort row = 0; row < BoardSize && !isOneColFilledWithSingleValue; row++)
            {
                countValueInColumn = 0;
                for(ushort col = 0; col < BoardSize; col++)
                {
                    increaseCounterIfCellContainValue(row, col, i_ValueToCheck, ref countValueInColumn);
                }

                isOneColFilledWithSingleValue = countValueInColumn == BoardSize;
            }

            return isOneColFilledWithSingleValue;
        }

        private bool isBoardHaveDiagonalFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInDiagonal = 0;
            ushort countValueInAntiDiagonal = 0;

            for(ushort i = 0; i < BoardSize; i++)
            {
                increaseCounterIfCellContainValue(i, i, i_ValueToCheck, ref countValueInDiagonal);
                increaseCounterIfCellContainValue(
                    i,
                    (ushort)(BoardSize - i - 1),
                    i_ValueToCheck,
                    ref countValueInAntiDiagonal);
            }

            return countValueInDiagonal == BoardSize || countValueInAntiDiagonal == BoardSize;
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
            if(r_BoardMatrixCells[i_Row, i_Column].Value == i_ValueToCheck)
            {
                i_ValueCounter++;
            }
        }

        public bool IsAllBoardFilled()
        {
            return m_FilledCellAmount == BoardSize * BoardSize;
        }

        public void ChangeValueIfEmptyCell(MoveData i_MoveData)
        {
            int row = i_MoveData.CellCoordinate.SelectedRow - 1;
            int col = i_MoveData.CellCoordinate.SelectedColumn - 1;
            bool cellIsEmpty = r_BoardMatrixCells[row, col].Value == eBoardCellValue.Empty;

            if(cellIsEmpty)
            {
                r_BoardMatrixCells[row, col].Value = i_MoveData.CellValue;
                m_FilledCellAmount++;
            }
            else
            {
                throw new InvalidOperationException($"Cell({row + 1},{col + 1}) is not empty");
            }
        }

        public void CheckIfValidAndEmptyCell(MoveData i_Data)
        {
            if(!(i_Data.CellCoordinate.SelectedRow <= BoardSize && i_Data.CellCoordinate.SelectedColumn <= BoardSize
                                                                && i_Data.CellCoordinate.SelectedRow > 0
                                                                && i_Data.CellCoordinate.SelectedColumn > 0))
            {
                throw new IndexOutOfRangeException("Selected coordinate is out of board range");
            }

            if(r_BoardMatrixCells[i_Data.CellCoordinate.SelectedRow - 1, i_Data.CellCoordinate.SelectedColumn - 1].Value
               != eBoardCellValue.Empty)
            {
                throw new InvalidOperationException("Cell is not empty");
            }

            if(i_Data.CellValue == eBoardCellValue.Empty)
            {
                throw new InvalidOperationException("cannot erase not empty Cell");
            }
        }

        public eBoardCellValue[,] GetCurrentBoardState()
        {
            eBoardCellValue[,] currentBoard = new eBoardCellValue[BoardSize, BoardSize];

            for(int row = 0; row < BoardSize; row++)
            {
                for(int col = 0; col < BoardSize; col++)
                {
                    currentBoard[row, col] = r_BoardMatrixCells[row, col].Value;
                }
            }

            return currentBoard;
        }

        public Cell[,] GetBoard()
        {
            return r_BoardMatrixCells;
        }

        public struct Cell
        {
            public eBoardCellValue Value { get; set; }
        }
    }
}