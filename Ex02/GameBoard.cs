using Ex02;

namespace Engine
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

        public struct Cell
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

        private bool isBoardHaveRowFilledWithValue(eBoardCellValue i_ValueToCheck)
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
        private bool isBoardHaveColumnFilledWithValue(eBoardCellValue i_ValueToCheck)
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

        private bool isBoardHaveDiagonalFilledWithValue(eBoardCellValue i_ValueToCheck)
        {
            ushort countValueInDiagonal = 0, countValueInAntiDiagonal=0;
             
            for (ushort i = 0; i < r_BoardSize ; i++)
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
            bool cellIsEmpty = m_BoardMatrixCells[i_MoveData.CellCoordinate.SelectedRow - 1, i_MoveData.CellCoordinate.SelectedColumn - 1].Value == eBoardCellValue.Empty;

            if(cellIsEmpty)
            {
                m_BoardMatrixCells[i_MoveData.CellCoordinate.SelectedRow - 1, i_MoveData.CellCoordinate.SelectedColumn - 1].Value=i_MoveData.CellValue;
            }

            return cellIsEmpty;
        }

        public bool IsValidAndEmptyCell(ushort i_Row, ushort i_Column, ref eCellError i_CellError)
        {
            bool cellIsValid = false;
            if (!(i_Row <= r_BoardSize && i_Column <= r_BoardSize && i_Row > 0 && i_Column > 0))
            {
                i_CellError = eCellError.CellOutOfRange;
            }
            else if(m_BoardMatrixCells[i_Row - 1, i_Column - 1].Value != eBoardCellValue.Empty)
            {
                i_CellError = eCellError.CellNotEmpty;
            }
            else
            {
                cellIsValid = true;
            }

            return cellIsValid;
        }

        public eBoardCellValue[,] GetCurrentBoardState()
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

        public void InitializeEmptyBoard()
        {
            throw new System.NotImplementedException();
        }
    }
}
