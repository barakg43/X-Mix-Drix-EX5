using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{

    class CellBoardButton:Button
    {
        private readonly CellBoardCoordinate r_ButtonBoardCoordinate;
        public event Action<CellBoardCoordinate> CellClicked;

        private const ushort k_ButtonSize = 30;
        public CellBoardButton(CellBoardCoordinate i_ButtonBoardCoordinate)
        {
            r_ButtonBoardCoordinate = i_ButtonBoardCoordinate;
            initializeProperties();
        }

        private void initializeProperties()
        {
            Height = k_ButtonSize;
            Width = k_ButtonSize;
            Text = readCellValue(eBoardCellValue.Empty);
        }
        public void ChangeCellValue(eBoardCellValue i_CellValue)
        {
            this.Text = readCellValue(i_CellValue);
            this.Enabled = false;
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

        protected override void OnClick(EventArgs e)
        {

            base.OnClick(e);
            CellClicked?.Invoke(r_ButtonBoardCoordinate);
        }

     
    }
}
