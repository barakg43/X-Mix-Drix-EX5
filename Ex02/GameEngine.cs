using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class GameEngine
    {
        private GameBoard m_GameBoard=null;
        private bool m_IsStillPlaying;
        private Player m_FirstPlayer = null, m_SecondPlayer = null, m_CurrentTurnPlayer;
        private Random m_RandomNumberGenerator;
        private bool m_IsGameStarted;
        public GameEngine()
        {
            m_IsStillPlaying = true;
            m_RandomNumberGenerator = new Random();
        }

        public void Create2Players(ePlayerName i_FirstPlayerName, ePlayerName i_SecondPlayerName)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName,eBoardCellValue.X);
            m_SecondPlayer = new Player(i_SecondPlayerName,eBoardCellValue.O);
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
                m_IsGameStarted = false;
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

        public eBoardCellValue[,] GetCurrentBoardState()
        {
            return m_GameBoard.GetCurrentBoardState();
        }
        public eStartingGameStatus Start()
        {
            eStartingGameStatus gameInitilaztionStatus;
     
            if (m_FirstPlayer == null || m_SecondPlayer == null)
            {
                gameInitilaztionStatus = eStartingGameStatus.NotChooseTwoPlayerForTheGame;
            }
            else if(m_GameBoard == null)
            {
                gameInitilaztionStatus = eStartingGameStatus.NotChooseGameBoard;
            }
            else
            {
                gameInitilaztionStatus = eStartingGameStatus.StartSuccessfully;
            }
            m_CurrentTurnPlayer = m_FirstPlayer;

            return gameInitilaztionStatus;
        }

    }
}
