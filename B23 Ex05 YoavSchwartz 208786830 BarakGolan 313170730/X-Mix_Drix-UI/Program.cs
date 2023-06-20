using System.Windows.Forms;

namespace X_Mix_Drix_UI
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            GameManager gameManager = new GameManager();
            gameManager.RunGame();
            //GameManager gameManager = new GameManager();
            //gameManager.RunMenu();
        }
    }
}