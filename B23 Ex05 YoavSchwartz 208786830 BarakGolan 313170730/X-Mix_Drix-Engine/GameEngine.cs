using System;
using System.Dynamic;
using System.Runtime;

namespace Engine
{
    public class GameEngine
    {
        private GameBoard m_GameBoard = null;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private Player m_CurrentTurnPlayer;
        private ComputerPlayer m_ComputerPlayer = null;

        public event Action<MoveData> ValidMoveTurnNotifier;

        public event Action<eSessionWinner> SessionOverNotifier;

        public event Action<eSessionWinner, int> PlayerScoreUpdater;

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

        public void Create2Players(ePlayerType i_FirstPlayerType, ePlayerType i_SecondPlayerType)
        {
            m_FirstPlayer = new Player(i_FirstPlayerType, eBoardCellValue.X);
            m_SecondPlayer = new Player(i_SecondPlayerType, eBoardCellValue.O);
            m_CurrentTurnPlayer = m_FirstPlayer;
        }


        public GameBoard.Cell[,] GetBoard()
        {
            return m_GameBoard.GetBoard();
        }

        public ePlayerType GetCurrentTurnPlayerName()
        {
            return m_CurrentTurnPlayer.Type;
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
            m_CurrentTurnPlayer = m_CurrentTurnPlayer == m_FirstPlayer ?
                                      m_SecondPlayer : m_FirstPlayer;
        }

        private void checkIfCurrentPlayerLoseInSession()
        {
            bool isPreviousPlayerLoseSession = m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(m_CurrentTurnPlayer.GameSymbol);

            switchCurrentPlayerToOtherPlayer();
            if (isPreviousPlayerLoseSession)
            {
                currentPlayerWonInTheGameSession();
            }

            if (IsSessionFinishInTie)
            {
                SessionOverNotifier?.Invoke(eSessionWinner.Tie);
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

            ValidMoveTurnNotifier?.Invoke(currentMoveData);
            checkIfCurrentPlayerLoseInSession();
            makeComputerMoveInHisTurn();
        }

        private void makeComputerMoveInHisTurn()
        {
            CellBoardCoordinate? selectedComputerPlayerCell;
            MoveData computerTurnData;

            if (m_CurrentTurnPlayer.Type == ePlayerType.Computer && !IsSessionOver)
            {
                selectedComputerPlayerCell = m_ComputerPlayer.GetValidRandomEmptyCellBoardCoordinate();
                if (selectedComputerPlayerCell.HasValue)
                {
                    computerTurnData = new MoveData(selectedComputerPlayerCell.Value, m_CurrentTurnPlayer.GameSymbol);

                    m_GameBoard.ChangeValueIfEmptyCell(computerTurnData);
                    ValidMoveTurnNotifier?.Invoke(computerTurnData);
                }
                checkIfCurrentPlayerLoseInSession();
            }
        }

        private void currentPlayerWonInTheGameSession()
        {
            eSessionWinner sessionWinner;

            m_CurrentTurnPlayer.IncrementGameSessionsScore();
            IsSessionHaveWinner = true;
            if (m_CurrentTurnPlayer == m_FirstPlayer)
            {
                sessionWinner = eSessionWinner.FirstPlayer;
                PlayerScoreUpdater?.Invoke(sessionWinner, m_FirstPlayer.Score);
            }
            else
            {
                sessionWinner = eSessionWinner.SecondPlayer;
                PlayerScoreUpdater?.Invoke(sessionWinner, m_SecondPlayer.Score);
            }

            onSessionOver(sessionWinner);
        }

        private void onSessionOver(eSessionWinner i_SessionWinner)
        {
            SessionOverNotifier?.Invoke(i_SessionWinner);
        }

        public void StartNewGameSession()
        {
            IsSessionHaveWinner = false;
            m_GameBoard.InitializeEmptyBoard();
            if (m_FirstPlayer.Type == ePlayerType.Computer || m_SecondPlayer.Type == ePlayerType.Computer)
            {
                m_ComputerPlayer.MakeAllCellBoardUnselected();
            }
        }

        private void createGamePlayer(ushort i_BoardSize, ePlayerType i_PlayerType,out Player i_PlayerRef)
        {

            if (i_PlayerType == ePlayerType.Computer)
            {
                m_ComputerPlayer = new ComputerPlayer(i_BoardSize, eBoardCellValue.O, i_PlayerType);
                i_PlayerRef = m_ComputerPlayer;
            }
            else
            {
                i_PlayerRef = new Player(i_PlayerType, eBoardCellValue.X);
            }
        }
     
        public void SetInitialGameSettings(
            ushort i_BoardSize,
            ePlayerType i_FirstPlayerType,
            ePlayerType i_SecondPlayerType)
        {
            if (i_BoardSize < (ushort)eBoardSizeError.MinSize)
            {
                throw new ArgumentOutOfRangeException("i_BoardSize", $"board size is too small,min allow is {(ushort)eBoardSizeError.MinSize}");
            }

            if (i_BoardSize > (ushort)eBoardSizeError.MaxSize)
            {
                throw new ArgumentOutOfRangeException("i_BoardSize", $"board size is too big,max allow is {(ushort)eBoardSizeError.MaxSize}");
            }

            m_GameBoard = new GameBoard(i_BoardSize);
            createGamePlayer(i_BoardSize,i_FirstPlayerType,out m_FirstPlayer);
            createGamePlayer(i_BoardSize, i_SecondPlayerType, out m_SecondPlayer);
            //m_FirstPlayer = new Player(i_FirstPlayerType, eBoardCellValue.X);
            //m_SecondPlayer = new Player(i_SecondPlayerType, eBoardCellValue.O);
            m_CurrentTurnPlayer = m_FirstPlayer;
            IsSessionHaveWinner = false;
            //if (m_FirstPlayer.Type == ePlayerType.Computer)
            //{
            //    m_ComputerPlayer = new ComputerPlayer(i_BoardSize, eBoardCellValue.O, m_FirstPlayer.Type);
            //}
            //else
            //{

            //}
        }
    }
}
