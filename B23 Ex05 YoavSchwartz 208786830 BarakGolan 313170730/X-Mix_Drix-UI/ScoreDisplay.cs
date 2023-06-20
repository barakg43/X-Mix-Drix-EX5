using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace X_Mix_Drix_UI
{
    public partial class ScoreDisplay : UserControl
    {
        private ushort m_ScorePlayer1;
        private ushort m_ScorePlayer2;
        public ScoreDisplay(string i_Player1Name,string i_Player2Name)
        {
            InitializeComponent();
            player1NameLabel.Text = i_Player1Name;
            player2NameLabel.Text = i_Player2Name;
          
        }

        public void ResetScoreDisplay()
        {
            m_ScorePlayer1 = 0;
            m_ScorePlayer2 = 0;
            scorePlayer1Label.Text = m_ScorePlayer1.ToString();
            scorePlayer2Label.Text = m_ScorePlayer2.ToString();
        }

        public void IncrementScoreForPlayer(bool i_IsPlayer1)
        {
            if(i_IsPlayer1)
            {
                m_ScorePlayer1++;
                scorePlayer1Label.Text = m_ScorePlayer1.ToString();
            }
            else
            {
                m_ScorePlayer2++;
                scorePlayer2Label.Text = m_ScorePlayer2.ToString();
            }
        }
    }
}
