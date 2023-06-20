using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{
    public partial class GameDisplay : Form
    {
        private ScoreDisplay m_ScoreDisplay;
        private GameBoardPanel m_GameBoardPanel;
        public GameDisplay(string i_Player1Name,string i_Player2Name,ushort i_BoardSize)
        {
            InitializeComponent();
            initializeGameSetting(i_Player1Name,i_Player2Name,i_BoardSize);
        }


        //TODO :need to be fix the arrage on the board and score location
        private void initializeGameSetting(string i_Player1Name, string i_Player2Name, ushort i_BoardSize)
        {
            m_ScoreDisplay = new ScoreDisplay(i_Player1Name, i_Player2Name);
            m_GameBoardPanel = new GameBoardPanel(i_BoardSize);
            this.ClientSize = new Size(
                Math.Max(m_GameBoardPanel.Width, m_ScoreDisplay.Width),
                m_GameBoardPanel.Height + m_ScoreDisplay.Height+60);

       
           // this.flowLayoutPanel1.Visible = false;
           m_GameBoardPanel.Location = caluateCenterPositionInForm(
               m_GameBoardPanel,
               5);
            m_ScoreDisplay.Location = caluateCenterPositionInForm(
                m_ScoreDisplay, 
                120);
            Controls.Add(m_GameBoardPanel);
            Controls.Add(m_ScoreDisplay);
      


        }

        private Point caluateCenterPositionInForm(Control i_Control,int i_TopOffset)
        {
            int topPosition = i_TopOffset + (ClientSize.Height - i_Control.Height) / 2 ;
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
        public void IncrementScoreForPlayer(bool i_IsPlayer1)
        {
            m_ScoreDisplay.IncrementScoreForPlayer(i_IsPlayer1);
        }

        private void GameDisplay_Load(object sender, EventArgs e)
        {

        }

        public void UpdateScore(ushort i_ScorePlayer1, ushort i_ScorePlayer2)
        {
            m_ScoreDisplay.UpdateScore(i_ScorePlayer1, i_ScorePlayer2);
        }
    }
}
