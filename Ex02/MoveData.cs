namespace Engine
{
        public struct MoveData
    {
        private CellBoardCoordinate m_CellBoardCoordinate;
        private eBoardCellValue m_CellValue;
        public MoveData(ushort i_SelectedColumn, ushort i_SelectedRow, eBoardCellValue i_CellValue)
        {
            m_CellBoardCoordinate = new CellBoardCoordinate(i_SelectedColumn, i_SelectedRow);
            m_CellValue = i_CellValue;
        }

        public MoveData(CellBoardCoordinate i_CellCoordinate, eBoardCellValue i_CellValue)
        {
            m_CellBoardCoordinate = i_CellCoordinate;
            m_CellValue = i_CellValue;
        }

        public CellBoardCoordinate CellCoordinate
        {
            get { return m_CellBoardCoordinate; }
        }

        public eBoardCellValue CellValue 
        { 
            get { return m_CellValue; }
        }
    }
}
