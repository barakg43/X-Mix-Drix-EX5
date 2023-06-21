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
    public partial class ScoreDisplay : UserControl
    {
        private ushort m_ScorePlayer1;
        private ushort m_ScorePlayer2;
        public ScoreDisplay(string i_Player1Name,string i_Player2Name)
        {
            InitializeComponent();
            initializeProperties(i_Player1Name, i_Player2Name);
            initializeScoreDisplay();
        }
        private void initializeProperties(string i_Player1Name, string i_Player2Name)
        {
            player1NameLabel.Text = i_Player1Name + @":";
            player2NameLabel.Text = i_Player2Name + @":";
            scorePlayer1Label.Left = player1NameLabel.Left + player1NameLabel.Width + 1;
            player2NameLabel.Left = scorePlayer1Label.Left + scorePlayer1Label.Width + 15;
            scorePlayer2Label.Left = player2NameLabel.Left + player2NameLabel.Width + 1;
            Width = scorePlayer2Label.Left+ scorePlayer2Label.Width + 30;
        }
        private void initializeScoreDisplay()
        {
            m_ScorePlayer1 = 0;
            m_ScorePlayer2 = 0;
            scorePlayer1Label.Text = m_ScorePlayer1.ToString();
            scorePlayer2Label.Text = m_ScorePlayer2.ToString();
        }

        public void SetScorePlayer(eSessionWinner i_SessionWinnerPlayer, int i_Score)
        {
            if(i_SessionWinnerPlayer == eSessionWinner.FirstPlayer)
            {
                scorePlayer1Label.Text = i_Score.ToString();
            }
            else if(i_SessionWinnerPlayer == eSessionWinner.SecondPlayer)
            {
                scorePlayer2Label.Text = i_Score.ToString();
            }
            else
            {
                throw new ArgumentException($"Cannot use this enum value[{i_SessionWinnerPlayer}] in this method");
            }
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

        public void UpdateScore(ushort i_ScorePlayer1, ushort i_ScorePlayer2)
        {
            m_ScorePlayer1 = i_ScorePlayer1;
            m_ScorePlayer2 = i_ScorePlayer2;
            scorePlayer1Label.Text = m_ScorePlayer1.ToString();
            scorePlayer2Label.Text = m_ScorePlayer2.ToString();
        }
    }
}
