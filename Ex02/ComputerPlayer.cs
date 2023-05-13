using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    class ComputerPlayer
    {
        private readonly ushort r_BoardSize;
        private List<CellBoardCoordinate> m_EmptyCellsList;
        private Random m_RandomIndexGenerator;
        //private Dictionary<int, int> m_MapIndex;
        public ComputerPlayer(ushort i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            m_EmptyCellsList = new List<CellBoardCoordinate>(r_BoardSize* r_BoardSize);
            m_RandomIndexGenerator = new Random();
            MakeAllCellBoardUnselected();
        }

        public void MakeAllCellBoardUnselected()
        {
            CellBoardCoordinate currentBoardCoordinate;

            for (ushort row = 0; row < r_BoardSize; row++)
            {
                for (ushort col = 0; col < r_BoardSize; col++)
                {
                    currentBoardCoordinate = new CellBoardCoordinate(row, col);
                    m_EmptyCellsList.Add(currentBoardCoordinate);
                }
            }
        }

        public void RemoveCoordinateFromAvailableList(CellBoardCoordinate i_CellCoordinate)
        {
            m_EmptyCellsList.Remove(i_CellCoordinate);
        }

        private int calculateMapKey(CellBoardCoordinate i_CellCoordinate)
        {
            return i_CellCoordinate.SelectedRow * r_BoardSize + i_CellCoordinate.SelectedColumn;
        }

        public CellBoardCoordinate? GetValidRandomEmptyCellBoardCoordinate()
        {
            CellBoardCoordinate? boardCoordinate=null;
            int randomIndex;

            if(m_EmptyCellsList.Count > 0)
            {
                randomIndex = m_RandomIndexGenerator.Next(m_EmptyCellsList.Count);
                boardCoordinate= m_EmptyCellsList[randomIndex];
                RemoveCoordinateFromAvailableList(boardCoordinate.Value);
            }

            return boardCoordinate;
        }
    }


}
