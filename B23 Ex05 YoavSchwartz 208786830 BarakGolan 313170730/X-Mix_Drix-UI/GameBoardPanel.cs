using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{
   internal class GameBoardPanel:Panel
   {
       public event Action<CellBoardCoordinate> CellBoardClicked;
       private readonly ushort r_BoardSize;
       private List<CellBoardButton> CellBoardButtons;
        public GameBoardPanel(ushort i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            initializeEmptyBoard();
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(200, 100);
            this.TabIndex = 0;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.ResumeLayout(false);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
