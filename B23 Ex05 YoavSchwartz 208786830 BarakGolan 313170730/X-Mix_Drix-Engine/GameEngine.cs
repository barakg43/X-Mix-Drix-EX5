using System;

namespace Engine
{
    public class GameEngine
    {
        private ComputerPlayer m_ComputerPlayer;
        private Player m_CurrentTurnPlayer;
        private Player m_FirstPlayer;
        private GameBoard m_GameBoard;
        private Player m_SecondPlayer;

        public bool IsSessionFinishInTie =>
            !IsSessionHaveWinner && m_GameBoard != null && m_GameBoard.IsAllBoardFilled();

        public bool IsSessionOver => IsSessionFinishInTie || IsSessionHaveWinner;

        public bool IsSessionHaveWinner { get; private set; }

        public event Action<MoveData> ValidMoveTurnNotifier;

        public event Action<eSessionWinner> SessionOverNotifier;

        public event Action<eSessionWinner, int> PlayerScoreUpdater;

        private void create2Players(ePlayerType i_FirstPlayerType, ePlayerType i_SecondPlayerType, ushort i_BoardSize)
        {
            m_FirstPlayer = createGamePlayer(i_FirstPlayerType, eBoardCellValue.X, i_BoardSize);
            m_SecondPlayer = createGamePlayer(i_SecondPlayerType, eBoardCellValue.O, i_BoardSize);
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
            m_CurrentTurnPlayer = m_CurrentTurnPlayer == m_FirstPlayer ? m_SecondPlayer : m_FirstPlayer;
        }

        private void checkIfCurrentPlayerLoseInSession()
        {
            bool isPreviousPlayerLoseSession =
                m_GameBoard.IsBoardHaveAnyRowColumnDiagonalFilled(m_CurrentTurnPlayer.GameSymbol);

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

        public void MakeValidGameMoveForCurrentPlayer(CellBoardCoordinate i_BoardCoordinate)
        {
            MoveData currentMoveData = new MoveData(i_BoardCoordinate, m_CurrentTurnPlayer.GameSymbol);

            checkIfValidMoveInTurn(currentMoveData);
            m_GameBoard.ChangeValueIfEmptyCell(currentMoveData);
            if(m_ComputerPlayer != null)
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

            if(m_CurrentTurnPlayer.Type == ePlayerType.Computer && !IsSessionOver)
            {
                selectedComputerPlayerCell = m_ComputerPlayer.GetValidRandomEmptyCellBoardCoordinate();
                if(selectedComputerPlayerCell.HasValue)
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
            if(m_FirstPlayer.Type == ePlayerType.Computer || m_SecondPlayer.Type == ePlayerType.Computer)
            {
                m_ComputerPlayer.MakeAllCellBoardUnselected();
            }
        }

        private Player createGamePlayer(ePlayerType i_PlayerType, eBoardCellValue i_GameSymbol, ushort i_BoardSize)
        {
            Player gamePlayer;

            if(i_PlayerType == ePlayerType.Computer && m_ComputerPlayer != null)
            {
                throw new ArgumentException("Cannot play with 2 computer players!");
            }

            if(i_PlayerType == ePlayerType.Computer)
            {
                m_ComputerPlayer = new ComputerPlayer(i_BoardSize, i_GameSymbol);
                gamePlayer = m_ComputerPlayer;
            }
            else
            {
                gamePlayer = new Player(i_PlayerType, i_GameSymbol);
            }

            return gamePlayer;
        }

        public void SetInitialGameSettings(
            ushort i_BoardSize,
            ePlayerType i_FirstPlayerType,
            ePlayerType i_SecondPlayerType)
        {
            if(i_BoardSize < (ushort)eBoardSizeError.MinSize)
            {
                throw new ArgumentOutOfRangeException(
                    "i_BoardSize",
                    $"board size is too small,min allow is {(ushort)eBoardSizeError.MinSize}");
            }

            if(i_BoardSize > (ushort)eBoardSizeError.MaxSize)
            {
                throw new ArgumentOutOfRangeException(
                    "i_BoardSize",
                    $"board size is too big,max allow is {(ushort)eBoardSizeError.MaxSize}");
            }

            m_GameBoard = new GameBoard(i_BoardSize);
            create2Players(i_FirstPlayerType, i_SecondPlayerType, i_BoardSize);
            m_CurrentTurnPlayer = m_FirstPlayer;
            IsSessionHaveWinner = false;
        }
    }
}