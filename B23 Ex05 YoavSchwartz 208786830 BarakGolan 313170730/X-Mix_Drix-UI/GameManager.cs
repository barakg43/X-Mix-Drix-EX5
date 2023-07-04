using Engine;
using System.Windows.Forms;

namespace X_Mix_Drix_UI
{
    public class GameManager
    {
        private readonly GameEngine r_Engine;
        private GameDisplay m_GameBoardDisplay;
        private readonly GameSetting r_GameSetting;

        public GameManager()
        {
            r_Engine = new GameEngine();
            r_GameSetting = new GameSetting();
        }

        public void RunGame()
        {
            DialogResult gameSettingDialogResult = r_GameSetting.ShowDialog();

            if (gameSettingDialogResult == DialogResult.OK)
            {
                createGameDisplay();
            }
        }

        private void setupGameEngineSetting()
        {
            if (r_GameSetting.IsPlayingVsComputer)
            {
                r_Engine.SetInitialGameSettings(r_GameSetting.BoardSize, ePlayerType.Human, ePlayerType.Computer);
              
            }
            else
            {
                r_Engine.SetInitialGameSettings(r_GameSetting.BoardSize, ePlayerType.Human, ePlayerType.Human);
            }

            r_Engine.SessionOverNotifier += r_Engine_SessionOverNotifier; ;
            r_Engine.PlayerScoreUpdater += r_Engine_PlayerScoreUpdater;
            r_Engine.ValidMoveTurnNotifier += r_Engine_ValidMoveTurnNotifier; ;
        }

        private void r_Engine_ValidMoveTurnNotifier(MoveData i_TurnDataForPlayer)
        {
            markPlayerMoveInGameBoard(i_TurnDataForPlayer);
        }

        private void r_Engine_PlayerScoreUpdater(eSessionWinner i_PlayerName, int i_Score)
        {
            m_GameBoardDisplay.UpdateScore(i_PlayerName, i_Score);
        }

        private void r_Engine_SessionOverNotifier(eSessionWinner i_SessionWinner)
        {
            sessionOverHandler(i_SessionWinner);
        }

        private void createGameDisplay()
        {
            //new BoardPrinter(r_GameSetting.BoardSize);
            m_GameBoardDisplay = new GameDisplay(
                r_GameSetting.Player1Name,
                r_GameSetting.Player2Name,
                r_GameSetting.BoardSize);
            m_GameBoardDisplay.RegisterForCellBoardClickedEvent(r_Engine.MakeValidGameMoveForCurrentPlayer);
            setupGameEngineSetting();
            m_GameBoardDisplay.ShowDialog();
        }

        private void sessionOverHandler(eSessionWinner i_SessionWinner)
        {
            switch (i_SessionWinner)
            {
                case eSessionWinner.FirstPlayer:
                    checkIfWantPlayingAnotherSession(
                        m_GameBoardDisplay.AnnounceSessionWinnerAndAskForNewSession(r_GameSetting.Player1Name));
                    break;
                case eSessionWinner.SecondPlayer:
                    checkIfWantPlayingAnotherSession(
                        m_GameBoardDisplay.AnnounceSessionWinnerAndAskForNewSession(r_GameSetting.Player2Name));
                    break;
                case eSessionWinner.Tie:
                    checkIfWantPlayingAnotherSession(
                        m_GameBoardDisplay.AnnounceSessionTieAndAskForNewSession());
                    break;
            }
        }

        private void checkIfWantPlayingAnotherSession(DialogResult i_DialogResult)
        {
            if (i_DialogResult == DialogResult.Yes)
            {
                m_GameBoardDisplay.StartNewGameSession();
                r_Engine.StartNewGameSession();
            }
            else if (i_DialogResult == DialogResult.No)
            {
                m_GameBoardDisplay.Close();
            }
        }

        private void markPlayerMoveInGameBoard(MoveData i_TurnData)
        {
            m_GameBoardDisplay.ChangeCellBoardValue(i_TurnData);
        }
    }
}