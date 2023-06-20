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
        private FlowLayoutPanel flowLayoutPanel1;
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // GameBoardPanel
            // 
            this.Name = "panel1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.ResumeLayout(false);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
