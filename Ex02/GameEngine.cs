using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class GameEngine
    {
        private GameBoard m_GameBoard=null;
        private bool m_IsStillPlaying ;
        private Player m_FirstPlayer = null, m_SecondPlayer = null, m_CurrentTurnPlayer;
        public GameEngine()
        {
            m_IsStillPlaying = true;
        }

        public void Create2Players(ePlayerName i_FirstPlayerName, ePlayerName i_SecondPlayerName)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName);
            m_SecondPlayer = new Player(i_SecondPlayerName);
        }
        public eBoardSizeError CreateNewEmptyGameBoard(ushort i_BoardSize)
        {
            eBoardSizeError sizeStatus;

            if(i_BoardSize < (ushort)eBoardSizeError.MinSize)
            {
                sizeStatus = eBoardSizeError.MinSize;

            }
            else if(i_BoardSize > (ushort)eBoardSizeError.MaxSize)
            {
                sizeStatus = eBoardSizeError.MaxSize;
            }
            else
            {
                sizeStatus = eBoardSizeError.Valid;
                m_GameBoard = new GameBoard(i_BoardSize);
            }

            return sizeStatus;
        }

        public ePlayerName GetCurrentTurnPlayerName()
        {
            return m_CurrentTurnPlayer.Name;
        }

        public bool IsValidMoveInTurn(MoveData i_Data)
        {
            return m_GameBoard.IsValidAndEmptyCell(i_Data.SelectedRow, i_Data.SelectedRow)
                   && i_Data.CellValue != eBoardCellValue.Empty;
        }


        public void Start()
        {
            if(m_FirstPlayer == null || m_SecondPlayer == null)
            {

            }


        }
    }
}
