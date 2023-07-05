using System;
using System.Windows.Forms;
using Engine;

namespace X_Mix_Drix_UI
{
    public partial class GameSetting : Form
    {
        private const string k_DefaultPlayer2Name = "Computer";
        private const string k_ErrorMassagePlayerNames = "Player1 or Player2 names cannot be emtpy!";
        private const string k_ErrorMessageBoxCaption = "Error";

        public GameSetting()
        {
            InitializeComponent();
            initializeProperties();
        }

        private void initializeProperties()
        {
            textBoxPlayer2Name.Text = k_DefaultPlayer2Name;
            numericUpDownCols.Minimum = (decimal)eBoardSizeError.MinSize;
            numericUpDownCols.Maximum = (decimal)eBoardSizeError.MaxSize;
            numericUpDownRows.Minimum = (decimal)eBoardSizeError.MinSize;
            numericUpDownRows.Maximum = (decimal)eBoardSizeError.MaxSize;
        }

        public string Player1Name => textBoxPlayer1Name.Text;

        public string Player2Name => textBoxPlayer2Name.Text;

        public ushort BoardSize => (ushort)numericUpDownRows.Value;

        public bool IsPlayingVsComputer => !checkBoxPlayer2.Checked;

        private void numericUpDownRowsCols_ValueChanged(object i_Sender, EventArgs i_EventArgs)
        {
            if(i_Sender is NumericUpDown senderObject)
            {
                numericUpDownRows.Value = senderObject.Value;
                numericUpDownCols.Value = senderObject.Value;
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object i_Sender, EventArgs i_EventArgs)
        {
            textBoxPlayer2Name.Text = textBoxPlayer2Name.Enabled ? k_DefaultPlayer2Name : string.Empty;
            textBoxPlayer2Name.Enabled = checkBoxPlayer2.Checked;
        }

        private bool isValidPlayerNames()
        {
            bool isValidPlayer1Name = !string.IsNullOrEmpty(textBoxPlayer1Name.Text);
            bool isValidPlayer2Name = !string.IsNullOrEmpty(textBoxPlayer2Name.Text);

            return isValidPlayer1Name && isValidPlayer2Name;
        }

        private void startButton_Click(object i_Sender, EventArgs i_EventArgs)
        {
            if(isValidPlayerNames())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(k_ErrorMassagePlayerNames, k_ErrorMessageBoxCaption, MessageBoxButtons.OK);
            }
        }
    }
}