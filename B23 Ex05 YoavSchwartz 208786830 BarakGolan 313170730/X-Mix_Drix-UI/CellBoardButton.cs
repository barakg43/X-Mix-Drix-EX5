using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private const ushort k_MarginSize = 3;
        public CellBoardButton(CellBoardCoordinate i_ButtonBoardCoordinate)
        {
            r_ButtonBoardCoordinate = i_ButtonBoardCoordinate;
            initializeProperties();
        }

        public static ushort GetButtonSize()
        {
            return k_ButtonSize + k_MarginSize * 2;
        }
        private void initializeProperties()
        {
            Height = k_ButtonSize;
            Width = k_ButtonSize;
            Margin = new Padding(k_MarginSize);
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
