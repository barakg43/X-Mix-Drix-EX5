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

        private void initializeGameSetting(string i_Player1Name, string i_Player2Name, ushort i_BoardSize)
        {
            m_ScoreDisplay = new ScoreDisplay(i_Player1Name, i_Player2Name);
            m_GameBoardPanel = new GameBoardPanel(i_BoardSize);

           // this.flowLayoutPanel1.Visible = false;
            m_ScoreDisplay.Top = 100;
            m_ScoreDisplay.Left = 10;
            this.Controls.Add(m_GameBoardPanel);
            this.Controls.Add(m_ScoreDisplay);
        }
    }
}
