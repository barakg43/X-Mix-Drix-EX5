using System;
using System.Windows.Forms;

namespace X_Mix_Drix_UI
{
    public partial class GameSetting : Form
    {
        private const string k_DefaultPlayer2Name = "[Computer]";
        private const string k_ErrorMassagePlayerNames = "Player1 or Player2 names cannot be emtpy!";
        public GameSetting()
        {
            InitializeComponent();
            textBoxPlayer2Name.Text = k_DefaultPlayer2Name;
        }

        private void numericUpDownRowsCols_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown senderObject=sender as NumericUpDown;

            numericUpDownRows.Value = senderObject.Value;
            numericUpDownCols.Value = senderObject.Value;

        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (textBoxPlayer2Name.Enabled)
            {
                textBoxPlayer2Name.Text = k_DefaultPlayer2Name;
            }
            else
            {
                textBoxPlayer2Name.Text = "";
            }

            textBoxPlayer2Name.Enabled = checkBoxPlayer2.Checked;
        }

        private bool isValidPlayerNames()
        {
            bool isValidPlayer1Name = !string.IsNullOrEmpty(textBoxPlayer1Name.Text);
            bool isValidPlayer2Name = !string.IsNullOrEmpty(textBoxPlayer2Name.Text);

           return isValidPlayer1Name && isValidPlayer2Name;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if(isValidPlayerNames())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show( k_ErrorMassagePlayerNames, "Error", MessageBoxButtons.OK);
            }
        }
    }
}