using System.Windows.Forms;

namespace X_Mix_Drix_UI
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            GameDisplay gameSettingForm = new GameDisplay("player1","player2",4);
            gameSettingForm.ShowDialog();
            //GameManager gameManager = new GameManager();
            //gameManager.RunMenu();
        }
    }
}