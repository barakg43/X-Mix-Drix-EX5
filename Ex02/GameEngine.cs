using System;
using Ex02;

namespace Engine
{
    public class GameEngine
    {
        private GameBoard m_GameBoard=null;
        private bool m_IsStillPlaying;
        private Player m_FirstPlayer = null, m_SecondPlayer = null, m_CurrentTurnPlayer;
        private Random m_RandomNumberGenerator;
        private bool m_IsGameStarted;
        private ushort m_BoardSize;
        public GameEngine()
        {
            m_IsStillPlaying = true;
            m_RandomNumberGenerator = new Random();
        }

        public void Create2Players(ePlayerName i_FirstPlayerName = ePlayerName.Player1, ePlayerName i_SecondPlayerName = ePlayerName.Player2)
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

        public GameBoard.Cell[,] GetBoard()
        {
            return m_GameBoard.GetBoard();
        }

        public ePlayerName GetCurrentTurnPlayerName()
        {
            return m_CurrentTurnPlayer.Name;
        }

        private bool isValidMoveInTurn(MoveData i_MoveData)
        {
            return m_GameBoard.IsValidAndEmptyCell(i_MoveData.CellCoordinate.SelectedRow, i_MoveData.CellCoordinate.SelectedColumn)
                   && i_MoveData.CellValue != eBoardCellValue.Empty;
        }

        public eBoardCellValue[,] GetCurrentBoardState()
        {
            return m_GameBoard.GetCurrentBoardState();
        }

        private void switchCurrentPlayerToOtherPlayer()
        {
            if(m_CurrentTurnPlayer == m_FirstPlayer)
            {
                m_CurrentTurnPlayer = m_SecondPlayer;
            }
            else
            {
                m_CurrentTurnPlayer = m_FirstPlayer;
            }
        }

        private void checkIfaPlayerWinSession()
        {
            bool isPlayerWinSession= m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(m_CurrentTurnPlayer.GameSymbol);

            switchCurrentPlayerToOtherPlayer();
            if(isPlayerWinSession)
            {
                CreateNewEmptyGameBoard(m_BoardSize);// should be in different place
                m_CurrentTurnPlayer.incrementGameSessionsScore();
            }
        }
        public bool MakeValidGameMoveForCurrentPlayer(CellBoardCoordinate i_BoardCoordinate)
        {
            MoveData currentMoveData = new MoveData(i_BoardCoordinate, m_CurrentTurnPlayer.GameSymbol);
            bool isValidMove = isValidMoveInTurn(currentMoveData);

            if (isValidMove)
            {
                m_GameBoard.ChangeValueIfEmptyCell(currentMoveData);
                checkIfaPlayerWinSession();
            }

            return isValidMove;
        }

        
        public eStartingGameStatus ValidateInitializationGameParameters()
        {
            eStartingGameStatus gameInitializationStatus;
     
            if (m_FirstPlayer == null || m_SecondPlayer == null)
            {
                gameInitializationStatus = eStartingGameStatus.NotChooseTwoPlayerForTheGame;
            }
            else if(m_GameBoard == null)
            {
                gameInitializationStatus = eStartingGameStatus.NotChooseGameBoard;
            }
            else
            {
                gameInitializationStatus = eStartingGameStatus.StartSuccessfully;
            }
            m_CurrentTurnPlayer = m_FirstPlayer;

            return gameInitializationStatus;
        }

    }
}
