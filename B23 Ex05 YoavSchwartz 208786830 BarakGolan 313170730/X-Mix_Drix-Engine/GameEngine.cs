using System;

namespace Engine
{
    public class GameEngine
    {
        private GameBoard m_GameBoard = null;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private Player m_CurrentTurnPlayer;
        private PlayerNew m_FirstPlayerNew;
        private PlayerNew m_SecondPlayerNew;
        private PlayerNew m_CurrentPlayerNew;
        private ComputerPlayer m_ComputerPlayer = null;

        public event Action<MoveData> ValidMoveTurnNotifer;

        public event Action GameOverNotifier;

        public ushort FirstPlayerScore => m_FirstPlayerNew.Score;

        public ushort SecondPlayerScore => m_SecondPlayerNew.Score;



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

        public void Create1PlayerGame(string i_PlayerName, ushort i_BoardSize)
        {
            m_FirstPlayerNew = new PlayerNew(i_PlayerName, eBoardCellValue.X);
            m_SecondPlayerNew = new ComputerPlayer(i_BoardSize, eBoardCellValue.O);

        }

        public void Create2PlayersGame(string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_FirstPlayerNew = new PlayerNew(i_FirstPlayerName, eBoardCellValue.X);
            m_SecondPlayerNew = new PlayerNew(i_SecondPlayerName, eBoardCellValue.O);
            m_CurrentPlayerNew = m_FirstPlayerNew;
        }

        public void Create2Players(ePlayerName i_FirstPlayerName, ePlayerName i_SecondPlayerName)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName, eBoardCellValue.X);
            m_SecondPlayer = new Player(i_SecondPlayerName, eBoardCellValue.O);
            m_CurrentTurnPlayer = m_FirstPlayer;
        }

        public void CreateNewEmptyGameBoard(ushort i_BoardSize)
        {
            eBoardSizeError sizeStatus;

            if(i_BoardSize < (ushort)eBoardSizeError.MinSize)
            {
                throw new ArgumentOutOfRangeException("i_BoardSize", $"board size is too small,min allow is {(ushort)eBoardSizeError.MinSize}");
            }
            else if(i_BoardSize > (ushort)eBoardSizeError.MaxSize)
            {
                throw new ArgumentOutOfRangeException("i_BoardSize", $"board size is too big,max allow is {(ushort)eBoardSizeError.MaxSize}");
            }
            else
            {
                m_GameBoard = new GameBoard(i_BoardSize);
                IsSessionHaveWinner = false;
                /*if (m_FirstPlayer.Name == ePlayerName.Computer || m_SecondPlayer.Name == ePlayerName.Computer)
                {
                    m_ComputerPlayer = new ComputerPlayer(i_BoardSize);
                }*/
            }
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

        private void checkIfValidMoveInTurn(MoveData i_MoveData)
        {
             m_GameBoard.CheckIfValidAndEmptyCell(i_MoveData);
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
            //new
            m_CurrentPlayerNew = m_CurrentPlayerNew == m_FirstPlayerNew ? m_SecondPlayerNew : m_FirstPlayerNew;
        }

        private void checkIfCurrentPlayerLoseInSession()
        {
            bool isPreviousPlayerLoseSession = m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(m_CurrentTurnPlayer.GameSymbol);

            switchCurrentPlayerToOtherPlayer();
            if(isPreviousPlayerLoseSession)
            {
                currentPlayerWonInTheGameSession();
            }
        }

        //public bool MakeValidGameMoveForCurrentPlayer(
        //    CellBoardCoordinate i_BoardCoordinate,
        //    bool i_CurrentPlayerWantsToQuit,
        //    out eCellError i_CellError)
        //{
        //    MoveData currentMoveData = new MoveData(i_BoardCoordinate, m_CurrentTurnPlayer.GameSymbol);
        //    bool isValidMove = true;
        //    i_CellError = eCellError.NoError;
        //    try
        //    {
        //        checkIfValidMoveInTurn(currentMoveData);
        //    }
        //    catch(Exception e)
        //    {
        //        isValidMove = false;
        //        i_CellError = eCellError.CellNotEmpty;
        //    }

        //    if (isValidMove)
        //    {
        //        m_GameBoard.ChangeValueIfEmptyCell(currentMoveData);
        //        if(m_ComputerPlayer != null)
        //        {
        //            m_ComputerPlayer.RemoveCoordinateFromAvailableList(currentMoveData.CellCoordinate);
        //        }

        //        checkIfCurrentPlayerLoseInSession();
        //    }
        //    else if(i_CurrentPlayerWantsToQuit)
        //    {
        //        switchCurrentPlayerToOtherPlayer();
        //        currentPlayerWonInTheGameSession();
        //    }
            
        //    return i_CurrentPlayerWantsToQuit || isValidMove;
        //}
        public void MakeValidGameMoveForCurrentPlayer(CellBoardCoordinate i_BoardCoordinate)
        {
            MoveData currentMoveData = new MoveData(i_BoardCoordinate, m_CurrentTurnPlayer.GameSymbol);
            checkIfValidMoveInTurn(currentMoveData);
            m_GameBoard.ChangeValueIfEmptyCell(currentMoveData);
            if (m_ComputerPlayer != null)
            {
                m_ComputerPlayer.RemoveCoordinateFromAvailableList(currentMoveData.CellCoordinate);
            }

            if(m_SecondPlayerNew is ComputerPlayer computer)//new
            {
                computer.RemoveCoordinateFromAvailableList(currentMoveData.CellCoordinate);
            }
            ValidMoveTurnNotifer?.Invoke(currentMoveData);
            checkIfCurrentPlayerLoseInSession();
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

                checkIfCurrentPlayerLoseInSession();
            }
        }

        private void currentPlayerWonInTheGameSession()
        {
            m_CurrentTurnPlayer.IncrementGameSessionsScore();
            m_CurrentPlayerNew.IncrementGameSessionsScore();//new
            IsSessionHaveWinner = true;
            onGameOver();
        }

        private void onGameOver()
        {
            GameOverNotifier?.Invoke();
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
