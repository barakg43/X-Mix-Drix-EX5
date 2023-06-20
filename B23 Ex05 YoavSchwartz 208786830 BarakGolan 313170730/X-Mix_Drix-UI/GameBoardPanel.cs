using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{
   internal class GameBoardPanel:FlowLayoutPanel
   {
       public event Action<CellBoardCoordinate> CellBoardClicked;
       private readonly ushort r_BoardSize;
        private List<CellBoardButton> CellBoardButtons;
        public GameBoardPanel(ushort i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            initializeEmptyBoard();
            initializeProperties();
            CellBoardButtons = new List<CellBoardButton>(i_BoardSize * i_BoardSize);
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
                    this.Controls.Add(currentCellBoardButton);
                }
            }
        }
        private void initializeProperties()
        {
            ushort buttonSize = CellBoardButton.GetButtonSize();

            Height = buttonSize * r_BoardSize;
            Width = buttonSize * r_BoardSize;
        }

        public void ClearAllBoardCell()
        {
            foreach(CellBoardButton cellBoardButton in CellBoardButtons)
            {
                cellBoardButton.ChangeCellValue(eBoardCellValue.Empty);
            }
        }
        protected virtual void OnCellBoardClicked(CellBoardCoordinate i_CellCoordinate)
        {
            CellBoardClicked?.Invoke(i_CellCoordinate);
        }


    }
}
