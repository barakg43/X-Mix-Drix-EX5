using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{
    internal class CellBoardButton : Button
    {
        private const ushort k_ButtonSize = 40;
        private const ushort k_MarginSize = 3;
        private readonly CellBoardCoordinate r_ButtonBoardCoordinate;
        private readonly Color r_ColorO = Color.Orange;
        private readonly Color r_ColorX = Color.DarkSeaGreen;

        public CellBoardButton(CellBoardCoordinate i_ButtonBoardCoordinate)
        {
            r_ButtonBoardCoordinate = i_ButtonBoardCoordinate;
            initializeProperties();
        }

        public event Action<CellBoardCoordinate> CellClicked;

        public static ushort GetButtonSize()
        {
            return k_ButtonSize + (k_MarginSize * 2);
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
            Text = readCellValue(i_CellValue);
            changeCellColorAcrodingValue(i_CellValue);
            Enabled = false;
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
                    BackColor = DefaultBackColor;
                    break;
            }
        }

        private string readCellValue(eBoardCellValue i_Value)
        {
            string value = i_Value == eBoardCellValue.Empty ? " " : i_Value.ToString();

            return value;
        }

        protected override void OnClick(EventArgs i_EventArgs)
        {
            base.OnClick(i_EventArgs);
            CellClicked?.Invoke(r_ButtonBoardCoordinate);
        }
    }
}