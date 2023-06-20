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
            NumericUpDown senderObject = sender as NumericUpDown;

            numericUpDownRows.Value = senderObject.Value;
            numericUpDownCols.Value = senderObject.Value;

        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2Name.Text = textBoxPlayer2Name.Enabled ? k_DefaultPlayer2Name : "";

            textBoxPlayer2Name.Enabled = checkBoxPlayer2.Checked;
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1Name.Text;
            }
        }
        public string Player2Name
        {
            get
            {
                return textBoxPlayer2Name.Text;
            }
        }
        public ushort BoardSize
        {
            get
            {
                return (ushort)numericUpDownRows.Value;
            }
        }
        private bool isValidPlayerNames()
        {
            bool isValidPlayer1Name = !string.IsNullOrEmpty(textBoxPlayer1Name.Text);
            bool isValidPlayer2Name = !string.IsNullOrEmpty(textBoxPlayer2Name.Text);

           return isValidPlayer1Name && isValidPlayer2Name;
        }

        public bool IsPlayingVsComputer
        {
            get
            {
                return !checkBoxPlayer2.Checked;
            }
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

        private void textBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void GameSetting_Load(object sender, EventArgs e)
        {

        }
    }
}