namespace Engine
{
    public struct MoveData
    {
        public MoveData(ushort i_SelectedColumn, ushort i_SelectedRow, eBoardCellValue i_CellValue)
        {
            CellCoordinate = new CellBoardCoordinate(i_SelectedColumn, i_SelectedRow);
            CellValue = i_CellValue;
        }

        public MoveData(CellBoardCoordinate i_CellCoordinate, eBoardCellValue i_CellValue)
        {
            CellCoordinate = i_CellCoordinate;
            CellValue = i_CellValue;
        }

        public CellBoardCoordinate CellCoordinate { get; }

        public eBoardCellValue CellValue { get; }
    }
}