using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
        public struct MoveData
    {
        public MoveData(ushort i_SelectedColumn, ushort i_SelectedRow, eBoardCellValue i_CellValue)
        {
            SelectedColumn = i_SelectedColumn;
            SelectedRow = i_SelectedRow;
            CellValue = i_CellValue;
        }

        public ushort SelectedColumn { get; }

        public ushort SelectedRow { get; }

        public eBoardCellValue CellValue { get; }
    }
}
