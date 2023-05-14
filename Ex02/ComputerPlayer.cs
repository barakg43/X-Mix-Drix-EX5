using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    class ComputerPlayer
    {
        private readonly ushort r_BoardSize;
        private readonly List<CellBoardCoordinate> r_EmptyCellsList;
        private readonly Random r_RandomIndexGenerator;
        public ComputerPlayer(ushort i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_EmptyCellsList = new List<CellBoardCoordinate>(r_BoardSize* r_BoardSize);
            r_RandomIndexGenerator = new Random();
            MakeAllCellBoardUnselected();
        }

        public void MakeAllCellBoardUnselected()
        {
            CellBoardCoordinate currentBoardCoordinate;

            for (ushort row = 1; row <= r_BoardSize; row++)
            {
                for (ushort col = 1; col <= r_BoardSize; col++)
                {
                    currentBoardCoordinate = new CellBoardCoordinate(row, col);
                    r_EmptyCellsList.Add(currentBoardCoordinate);
                }
            }
        }

        public void RemoveCoordinateFromAvailableList(CellBoardCoordinate i_CellCoordinate)
        {
            r_EmptyCellsList.Remove(i_CellCoordinate);
        }

   
        public CellBoardCoordinate? GetValidRandomEmptyCellBoardCoordinate()
        {
            CellBoardCoordinate? boardCoordinate=null;
            int randomIndex;

            if(r_EmptyCellsList.Count > 0)
            {
                randomIndex = r_RandomIndexGenerator.Next(r_EmptyCellsList.Count);
                boardCoordinate= r_EmptyCellsList[randomIndex];
                RemoveCoordinateFromAvailableList(boardCoordinate.Value);
            }

            if(boardCoordinate != null)
            {
                Console.WriteLine($@"({boardCoordinate.Value.SelectedRow},{boardCoordinate.Value.SelectedColumn}");
            }
            return boardCoordinate;
        }
    }


}
