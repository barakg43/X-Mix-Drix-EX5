using Engine;
using System;
using System.Windows.Forms;

namespace X_Mix_Drix_UI
{
    public partial class ScoreDisplay : UserControl
    {
        public ScoreDisplay(string i_Player1Name, string i_Player2Name)
        {
            InitializeComponent();
            initializeProperties(i_Player1Name, i_Player2Name);
            initializeScoreDisplay();
        }
        private void initializeProperties(string i_Player1Name, string i_Player2Name)
        {
            player1NameLabel.Text = i_Player1Name + ":";
            player2NameLabel.Text = "| "+i_Player2Name + ":";
            updateControlWidth();


        }
        private void initializeScoreDisplay()
        {
            scorePlayer1Label.Text = "0";
            scorePlayer2Label.Text = "0";
            updateControlWidth();
        }

        private void updateControlWidth()
        {
            scorePlayer1Label.Left = player1NameLabel.Left + player1NameLabel.Width;
            player2NameLabel.Left = scorePlayer1Label.Left + scorePlayer1Label.Width + 10;
            scorePlayer2Label.Left = player2NameLabel.Left + player2NameLabel.Width;
            Width = scorePlayer2Label.Left + scorePlayer2Label.Width + 20;
        }
        public void SetScorePlayer(eSessionWinner i_SessionWinnerPlayer, int i_Score)
        {
            if (i_SessionWinnerPlayer == eSessionWinner.FirstPlayer)
            {
                scorePlayer1Label.Text = i_Score.ToString();
            }
            else if (i_SessionWinnerPlayer == eSessionWinner.SecondPlayer)
            {
                scorePlayer2Label.Text = i_Score.ToString();
            }
            else
            {
                throw new ArgumentException($"Cannot use this enum value[{i_SessionWinnerPlayer}] in this method");
            }

            updateControlWidth();
        }
    }
}
