using System;
using System.Diagnostics.Eventing.Reader;

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

        public event Action<MoveData> ValidMoveTurnNotifier;

        public event Action<eSessionWinner> SessionOverNotifier;

        public event Action<eSessionWinner, int> PlayerScoreUpdater;

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
            m_CurrentTurnPlayer = m_CurrentTurnPlayer == m_FirstPlayer ?
                                      m_SecondPlayer : m_FirstPlayer;

            //if (m_CurrentTurnPlayer == m_FirstPlayer)
            //{
            //    m_CurrentTurnPlayer = m_SecondPlayer;
            //}
            //else
            //{
            //    m_CurrentTurnPlayer = m_FirstPlayer;
            //}
            //new
            //m_CurrentPlayerNew = m_CurrentPlayerNew == m_FirstPlayerNew ? m_SecondPlayerNew : m_FirstPlayerNew;
        }

        private void checkIfCurrentPlayerLoseInSession()
        {
            bool isPreviousPlayerLoseSession = m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(m_CurrentTurnPlayer.GameSymbol);

            switchCurrentPlayerToOtherPlayer();
            if(isPreviousPlayerLoseSession)
            {
                currentPlayerWonInTheGameSession();
            }

            if(IsSessionFinishInTie)
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
                Console.WriteLine(string.Format("player-removing ({0},{1}) #", currentMoveData.CellCoordinate.SelectedRow, currentMoveData.CellCoordinate.SelectedColumn));//TODO:REMOVE it
                m_ComputerPlayer.RemoveCoordinateFromAvailableList(currentMoveData.CellCoordinate);
            }

            //if(m_SecondPlayerNew is ComputerPlayer computer)//new
            //{
            //    computer.RemoveCoordinateFromAvailableList(currentMoveData.CellCoordinate);
            //}
            ValidMoveTurnNotifier?.Invoke(currentMoveData);
            checkIfCurrentPlayerLoseInSession();
            makeComputerMoveInHisTurn();
        }
    
        private void makeComputerMoveInHisTurn()
        {
            CellBoardCoordinate? selectedComputerPlayerCell;
            MoveData computerTurnData;

            if (m_CurrentTurnPlayer.Name == ePlayerName.Computer && !IsSessionOver)
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

            if(m_CurrentTurnPlayer == m_FirstPlayer)
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
            if (m_FirstPlayer.Name == ePlayerName.Computer || m_SecondPlayer.Name == ePlayerName.Computer)
            {
                m_ComputerPlayer.MakeAllCellBoardUnselected();
            }
        }
        public void CreateNewEmptyGameBoard(ushort i_BoardSize)
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
            IsSessionHaveWinner = false;
            if (m_FirstPlayer.Name == ePlayerName.Computer || m_SecondPlayer.Name == ePlayerName.Computer)
            {
                m_ComputerPlayer = new ComputerPlayer(i_BoardSize, eBoardCellValue.O);
            }
        }
    }
}
