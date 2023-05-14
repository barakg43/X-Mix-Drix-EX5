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
        private bool m_IsGameOver = false;
        private ushort m_BoardSize;
        public GameEngine()
        {
            m_IsStillPlaying = true;
            m_RandomNumberGenerator = new Random();
        }
        public bool GameIsOver
        {
            get { return m_IsGameOver; }
        }
        public void Create2Players(ePlayerName i_FirstPlayerName = ePlayerName.Player1, ePlayerName i_SecondPlayerName = ePlayerName.Player2)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName,eBoardCellValue.X);
            m_SecondPlayer = new Player(i_SecondPlayerName,eBoardCellValue.O);
            m_CurrentTurnPlayer = m_FirstPlayer;
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
                m_IsGameOver = false;
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

        public int GetMaxBoardSize()
        {
            return (int)eBoardSizeError.MaxSize;
        }

        public int GetMinBoardSize()
        {
            return (int)eBoardSizeError.MinSize;
        }

        public ushort GetCurrentBoardSize()
        {
            return m_GameBoard.BoardSize;
        }

        private bool isValidMoveInTurn(MoveData i_MoveData, ref eCellError i_CellError)
        {
            return m_GameBoard.IsValidAndEmptyCell(i_MoveData.CellCoordinate.SelectedRow, i_MoveData.CellCoordinate.SelectedColumn, ref i_CellError)
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
            //bool isPlayerWinSession= m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(m_CurrentTurnPlayer.GameSymbol);
            //switchCurrentPlayerToOtherPlayer();
            if(m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(getOtherPlayerSymbol()))
            {
                /*CreateNewEmptyGameBoard(m_BoardSize);// should be in different place
                m_CurrentTurnPlayer.incrementGameSessionsScore();*/
                otherPlayerWon();
            }
        }

        private eBoardCellValue getOtherPlayerSymbol()
        {
            eBoardCellValue currentPlayerSymbol = m_CurrentTurnPlayer.GameSymbol;
            return (eBoardCellValue)(2 * (int)currentPlayerSymbol % 3);
        }
        public bool MakeValidGameMoveForCurrentPlayer(CellBoardCoordinate i_BoardCoordinate, bool i_CurrentPlayerWantsToQuit, ref eCellError i_CellError)
        {
            MoveData currentMoveData = new MoveData(i_BoardCoordinate, m_CurrentTurnPlayer.GameSymbol);
            bool isValidMove = isValidMoveInTurn(currentMoveData, ref i_CellError);
            if (i_CurrentPlayerWantsToQuit || isValidMove)
            {
                switchCurrentPlayerToOtherPlayer();
                if (i_CurrentPlayerWantsToQuit)
                {
                    otherPlayerWon();
                }
                else
                {
                    m_GameBoard.ChangeValueIfEmptyCell(currentMoveData);
                    checkIfaPlayerWinSession();
                }

            }
            
            return i_CurrentPlayerWantsToQuit || isValidMove;
        }

        private void otherPlayerWon()
        {
            m_CurrentTurnPlayer.incrementGameSessionsScore();
            m_IsGameOver = true;
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

        public Player[] GetPlayers()
        {
            return new Player[2] { m_FirstPlayer, m_SecondPlayer};
        }

    }
}
