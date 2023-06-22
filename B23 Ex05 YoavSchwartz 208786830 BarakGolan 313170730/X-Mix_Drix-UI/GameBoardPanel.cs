using Engine;
using System;
using System.Windows.Forms;

namespace X_Mix_Drix_UI
{
    internal class GameBoardPanel : FlowLayoutPanel
    {
        public event Action<CellBoardCoordinate> CellBoardClicked;

        private readonly ushort r_BoardSize;

        private readonly CellBoardButton[,] r_CellBoardButtons;
        public GameBoardPanel(ushort i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_CellBoardButtons = new CellBoardButton[i_BoardSize, i_BoardSize];
            initializeEmptyBoard();
            initializeProperties();

        }

        private void initializeEmptyBoard()
        {
            CellBoardButton currentCellBoardButton;
            for (ushort row = 1; row <= r_BoardSize; row++)
            {
                for (ushort col = 1; col <= r_BoardSize; col++)
                {
                    currentCellBoardButton = new CellBoardButton(new CellBoardCoordinate(row, col));
                    currentCellBoardButton.CellClicked += OnCellBoardClicked;
                    r_CellBoardButtons[row - 1, col - 1] = currentCellBoardButton;
                    this.Controls.Add(currentCellBoardButton);
                }
            }
        }

        public void ChangeCellBoardValue(MoveData i_CellData)
        {
            int row = i_CellData.CellCoordinate.SelectedRow - 1;
            int col = i_CellData.CellCoordinate.SelectedColumn - 1;

            r_CellBoardButtons[row, col].ChangeCellValue(i_CellData.CellValue);
        }
        private void initializeProperties()
        {
            ushort buttonSize = CellBoardButton.GetButtonSize();

            Height = buttonSize * r_BoardSize;
            Width = buttonSize * r_BoardSize;
            Cursor = Cursors.Hand;

        }

        public void ClearAllBoardCell()
        {
            foreach (CellBoardButton cellBoardButton in r_CellBoardButtons)
            {
                cellBoardButton.ChangeCellValue(eBoardCellValue.Empty);
                cellBoardButton.Enabled = true;
            }
        }
        protected virtual void OnCellBoardClicked(CellBoardCoordinate i_CellCoordinate)
        {
            CellBoardClicked?.Invoke(i_CellCoordinate);

        }


    }
}
