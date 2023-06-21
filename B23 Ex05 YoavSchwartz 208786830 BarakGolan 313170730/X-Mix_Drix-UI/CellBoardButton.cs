using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{

    internal class CellBoardButton : Button
    {
        private readonly CellBoardCoordinate r_ButtonBoardCoordinate;
        public event Action<CellBoardCoordinate> CellClicked;
        private const ushort k_ButtonSize = 40;
        private const ushort k_MarginSize = 3;
        private readonly Color r_ColorX = Color.DarkSeaGreen;
        private readonly Color r_ColorO = Color.Orange;
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
            SetStyle(ControlStyles.Selectable, false);
        }
        public void ChangeCellValue(eBoardCellValue i_CellValue)
        {
            this.Text = readCellValue(i_CellValue);
            changeCellColorAcrodingValue(i_CellValue);
            this.Enabled = false;
        }

        private void changeCellColorAcrodingValue(eBoardCellValue i_CellValue)
        {
            switch(i_CellValue)
            {
                case eBoardCellValue.X:
                    BackColor = r_ColorX;
                    break;
                case eBoardCellValue.O:
                    BackColor = r_ColorO;
                    break;
                case eBoardCellValue.Empty:
                    BackColor = Button.DefaultBackColor;
                    break;

            }
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
