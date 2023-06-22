namespace Engine
{
    public readonly struct MoveData
    {
        private readonly CellBoardCoordinate r_CellBoardCoordinate;
        private readonly eBoardCellValue r_CellValue;

        public MoveData(ushort i_SelectedColumn, ushort i_SelectedRow, eBoardCellValue i_CellValue)
        {
            r_CellBoardCoordinate = new CellBoardCoordinate(i_SelectedColumn, i_SelectedRow);
            r_CellValue = i_CellValue;
        }

        public MoveData(CellBoardCoordinate i_CellCoordinate, eBoardCellValue i_CellValue)
        {
            r_CellBoardCoordinate = i_CellCoordinate;
            r_CellValue = i_CellValue;
        }

        public CellBoardCoordinate CellCoordinate
        {
            get
            {
                return r_CellBoardCoordinate;
            }
        }

        public eBoardCellValue CellValue
        {
            get
            {
                return r_CellValue;
            }
        }
    }
}
