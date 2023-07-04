using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{
    public partial class GameDisplay : Form
    {
        private const string k_WinnerSessionStringFormat = @"The winner is {0}!";
        private const string k_TieMessage = "Tie!";
        private const string k_PlayAgainMessage = "Would you like to play another round?";
        private const int k_GameDisplaySizeOffset = 60;
        private const int k_GamePanelTopOffset = 10;
        private const int k_ScoreDisplayTopOffsetFromBoard = 10;
        private GameBoardPanel m_GameBoardPanel;
        private ScoreDisplay m_ScoreDisplay;

        public GameDisplay(string i_Player1Name, string i_Player2Name, ushort i_BoardSize)
        {
            InitializeComponent();
            initializeGameSetting(i_Player1Name, i_Player2Name, i_BoardSize);
        }

        private void initializeGameSetting(string i_Player1Name, string i_Player2Name, ushort i_BoardSize)
        {
            int scoreTopPostion;
            int fromClientWidth;

            m_ScoreDisplay = new ScoreDisplay(i_Player1Name, i_Player2Name);
            m_GameBoardPanel = new GameBoardPanel(i_BoardSize);
            fromClientWidth = Math.Max(m_GameBoardPanel.Width, m_ScoreDisplay.Width) + k_GameDisplaySizeOffset;
            m_GameBoardPanel.Location = calculateCenterPositionInForm(
                m_GameBoardPanel,
                fromClientWidth,
                k_GamePanelTopOffset);
            scoreTopPostion = m_GameBoardPanel.Height + m_GameBoardPanel.Top + k_ScoreDisplayTopOffsetFromBoard;
            m_ScoreDisplay.Location = calculateCenterPositionInForm(m_ScoreDisplay, fromClientWidth, scoreTopPostion);
            ClientSize = new Size(
                fromClientWidth,
                m_ScoreDisplay.Height + m_ScoreDisplay.Top + k_ScoreDisplayTopOffsetFromBoard);
            Controls.Add(m_GameBoardPanel);
            Controls.Add(m_ScoreDisplay);
        }

        public DialogResult AnnounceSessionWinnerAndAskForNewSession(string i_WinnerName)
        {
            StringBuilder massage = new StringBuilder(2);

            massage.AppendLine(string.Format(k_WinnerSessionStringFormat, i_WinnerName));
            massage.AppendLine(k_PlayAgainMessage);

            return MessageBox.Show(massage.ToString(), "A Win!", MessageBoxButtons.YesNo);
        }

        public DialogResult AnnounceSessionTieAndAskForNewSession()
        {
            StringBuilder massage = new StringBuilder(2);

            massage.AppendLine(k_TieMessage);
            massage.AppendLine(k_PlayAgainMessage);

            return MessageBox.Show(massage.ToString(), "A Tie!", MessageBoxButtons.YesNo);
        }

        public void ChangeCellBoardValue(MoveData i_CellToChangeData)
        {
            m_GameBoardPanel.ChangeCellBoardValue(i_CellToChangeData);
        }

        private Point calculateCenterPositionInForm(Control i_Control, int i_FormWidth, int i_TopPosition)
        {
            int leftPosition = (i_FormWidth - i_Control.Width) / 2;

            return new Point(leftPosition, i_TopPosition);
        }

        public void RegisterForCellBoardClickedEvent(Action<CellBoardCoordinate> i_EventHandlerOnClicked)
        {
            m_GameBoardPanel.CellBoardClicked += i_EventHandlerOnClicked;
        }

        public void UnregisterForCellBoardClickedEvent(Action<CellBoardCoordinate> i_EventHandlerToRemove)
        {
            m_GameBoardPanel.CellBoardClicked += i_EventHandlerToRemove;
        }

        public void StartNewGameSession()
        {
            m_GameBoardPanel.ClearAllBoardCell();
        }

        public void UpdateScore(eSessionWinner i_PlayerName, int i_ScorePlayer)
        {
            m_ScoreDisplay.SetScorePlayer(i_PlayerName, i_ScorePlayer);
        }
    }
}