using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public struct CellBoardCoordinate
    {
        public CellBoardCoordinate(ushort i_SelectedRow, ushort i_SelectedColumn)
        {
            SelectedRow = i_SelectedRow;
            SelectedColumn = i_SelectedColumn;
        }

        public ushort SelectedColumn { get; }

        public ushort SelectedRow { get; }
    }
}
