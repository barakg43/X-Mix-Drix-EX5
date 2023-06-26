﻿using System;
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
        private ScoreDisplay m_ScoreDisplay;
        private GameBoardPanel m_GameBoardPanel;

        public GameDisplay(string i_Player1Name, string i_Player2Name, ushort i_BoardSize)
        {
            InitializeComponent();
            initializeGameSetting(i_Player1Name, i_Player2Name, i_BoardSize);
        }

        //// TODO :need to be fix the arrage on the board and score location
        private void initializeGameSetting(string i_Player1Name, string i_Player2Name, ushort i_BoardSize)
        {
            m_ScoreDisplay = new ScoreDisplay(i_Player1Name, i_Player2Name);
            m_GameBoardPanel = new GameBoardPanel(i_BoardSize);
            this.ClientSize = new Size(
                Math.Max(m_GameBoardPanel.Width, m_ScoreDisplay.Width),
                m_GameBoardPanel.Height + m_ScoreDisplay.Height + 60);


            m_GameBoardPanel.Location = caluateCenterPositionInForm(
                m_GameBoardPanel,
                5);
            m_ScoreDisplay.Location = caluateCenterPositionInForm(
                m_ScoreDisplay,
                120);
            Controls.Add(m_GameBoardPanel);
            Controls.Add(m_ScoreDisplay);
        }

        public DialogResult AnnounceSessionWinnerAndAskForNewSession(string i_WinnerName)
        {
            StringBuilder massage = new StringBuilder(2);

            massage.AppendLine(String.Format(k_WinnerSessionStringFormat, i_WinnerName));
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

        private Point caluateCenterPositionInForm(Control i_Control, int i_TopOffset)
        {
            int topPosition = i_TopOffset + (ClientSize.Height - i_Control.Height) / 2;
            int leftPosition = (ClientSize.Width - i_Control.Width) / 2;

            return new Point(leftPosition, topPosition);
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
