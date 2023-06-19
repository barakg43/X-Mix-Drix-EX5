using System;

namespace Engine
{
    public class GameEngine
    {
        private GameBoard m_GameBoard = null;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private Player m_CurrentTurnPlayer;
        private ComputerPlayer m_ComputerPlayer = null;

        public bool IsSessionFinishInTie
        {
            get
            {
                return !IsSessionHaveWinner
                       && m_GameBoard != null
                       && m_GameBoard.IsAllBoardFilled();
            }
        }

        public bool IsSessionOver
        {
            get
            {
                return IsSessionFinishInTie || IsSessionHaveWinner;
            }
        }

        public bool IsSessionHaveWinner
        {
            get; private set;
        }

        public void Create2Players(ePlayerName i_FirstPlayerName, ePlayerName i_SecondPlayerName)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName, eBoardCellValue.X);
            m_SecondPlayer = new Player(i_SecondPlayerName, eBoardCellValue.O);
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
                 IsSessionHaveWinner = false;
                if (m_FirstPlayer.Name == ePlayerName.Computer || m_SecondPlayer.Name == ePlayerName.Computer)
                {
                    m_ComputerPlayer = new ComputerPlayer(i_BoardSize);
                }
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

        private bool isValidMoveInTurn(MoveData i_MoveData, out eCellError i_CellError)
        {
            return m_GameBoard.IsValidAndEmptyCell(i_MoveData, out i_CellError);
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

        private void checkIfCurrentPlayerLooseInSession()
        {
            bool isPreviousPlayerLooseSession = m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(m_CurrentTurnPlayer.GameSymbol);

            switchCurrentPlayerToOtherPlayer();
            if(isPreviousPlayerLooseSession)
            {
                currentPlayerWonInTheGameSession();
            }
        }

        public bool MakeValidGameMoveForCurrentPlayer(
            CellBoardCoordinate i_BoardCoordinate,
            bool i_CurrentPlayerWantsToQuit,
            out eCellError i_CellError)
        {
            MoveData currentMoveData = new MoveData(i_BoardCoordinate, m_CurrentTurnPlayer.GameSymbol);
            bool isValidMove = isValidMoveInTurn(currentMoveData, out i_CellError);

            if(isValidMove)
            {
                m_GameBoard.ChangeValueIfEmptyCell(currentMoveData);
                if(m_ComputerPlayer != null)
                {
                    m_ComputerPlayer.RemoveCoordinateFromAvailableList(currentMoveData.CellCoordinate);
                }

                checkIfCurrentPlayerLooseInSession();
            }
            else if(i_CurrentPlayerWantsToQuit)
            {
                switchCurrentPlayerToOtherPlayer();
                currentPlayerWonInTheGameSession();
            }
            
            return i_CurrentPlayerWantsToQuit || isValidMove;
        }

        public void MakeComputerMoveInHisTurn()
        {
            CellBoardCoordinate? selectedComputerPlayerCell;
            
            if (m_CurrentTurnPlayer.Name == ePlayerName.Computer && !IsSessionOver)
            {
                selectedComputerPlayerCell = m_ComputerPlayer.GetValidRandomEmptyCellBoardCoordinate();
                if (selectedComputerPlayerCell.HasValue)
                {
                    m_GameBoard.ChangeValueIfEmptyCell(
                        new MoveData(selectedComputerPlayerCell.Value, m_CurrentTurnPlayer.GameSymbol));
                }

                checkIfCurrentPlayerLooseInSession();
            }
        }

        private void currentPlayerWonInTheGameSession()
        {
            m_CurrentTurnPlayer.IncrementGameSessionsScore();
            IsSessionHaveWinner = true;
        }

        public void StartNewGameSession()
        {
            IsSessionHaveWinner = false;
            m_GameBoard.InitializeEmptyBoard();
            if (m_FirstPlayer.Name == ePlayerName.Computer || m_SecondPlayer.Name == ePlayerName.Computer)
            {
                m_ComputerPlayer.MakeAllCellBoardUnselected();
            }
        }

        public Player[] GetPlayers()
        {
            return new Player[2] { m_FirstPlayer, m_SecondPlayer };
        }
    }
}
