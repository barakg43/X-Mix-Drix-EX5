using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
namespace X_Mix_Drix_UI
{
    class Controller
    {
        private GameEngine m_GameEngine;
        private readonly Menu m_GameMenu;

        public Controller()
        {
            m_GameEngine = new GameEngine();
            m_GameMenu = new Menu();
        }

        /*public void StartGame()
        {
            gameInitialization();

            while (m_GameEngine.)



        }

        private getUserInputFromConsole()
        private void gameInitialization()
        {
            eMenuOptions selectedOption= m_GameMenu.getValidMenuOptionFromUser();
            ushort selectedBoardSize;
            bool printErrorInputMassage = false;

            switch(selectedOption)
            {
                case eMenuOptions.StartGameAgainstPlayer:
                    m_GameEngine.Create2Players(ePlayerName.Player1, ePlayerName.Player2);
                    break;
                case eMenuOptions.StartGameAgainstPc:
                    m_GameEngine.Create2Players(ePlayerName.Player1,ePlayerName.Computer);
                    break;
            }

            selectedBoardSize = m_GameMenu.GetGameBoardSizeFromUser(printErrorInputMassage);
            while(m_GameEngine.CreateNewEmptyGameBoard(selectedBoardSize) != eBoardSizeError.Valid)
            {
                printErrorInputMassage = true;
                selectedBoardSize = m_GameMenu.GetGameBoardSizeFromUser(printErrorInputMassage);
            }
        }*/
       
    }
}
