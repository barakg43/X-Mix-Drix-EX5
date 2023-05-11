using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Ex02;

namespace X_Mix_Drix_UI
{
    

    class GameManager
    {
        private Menu m_Menu;
        private BoardPrinter m_BoardPrinter;
        GameEngine m_Engine;

        public GameManager()
        {
            m_Menu = new Menu();
            m_Engine = new GameEngine();
        }

        public void RunMenu()
        {
            int size;
            while (true)
            {
                m_Menu.PrintMainMenu();
                switch (m_Menu.GetAndCheckUserInputForMenuItem())
                {
                    case Menu.eMenuOptions.StartGameAgainstPC:
                        m_Menu.PrintSizeSelect();
                        size = m_Menu.GetAndCheckUserInputForBoardSize();
                        m_BoardPrinter = new BoardPrinter((ushort)size);
                        m_Engine.CreateNewEmptyGameBoard((ushort)size);

                        break;

                    case Menu.eMenuOptions.StartGameAgaintsPlayer:
                        m_Engine.Create2Players();
                        m_Menu.PrintSizeSelect();
                        size = m_Menu.GetAndCheckUserInputForBoardSize();
                        m_BoardPrinter = new BoardPrinter((ushort)size);
                        m_Engine.CreateNewEmptyGameBoard((ushort)size);


                        break;

                    case Menu.eMenuOptions.Quit:
                        return;
                }
            }
        }

        public void RunGame()
        {
            ePlayerName currentPlayerName;
            bool playerWantsToQuit = false;
            while (true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                m_BoardPrinter.PrintGameBoard(m_Engine.GetBoard());
                currentPlayerName = m_Engine.GetCurrentTurnPlayerName();
                playerWantsToQuit = makePlayerMove();
                if(playerWantsToQuit || gameIsOver())
                {
                    printResults();
                    if (getEndOfGameInput())
                    {
                        return;
                    }
                }

            }
        }

        private bool getEndOfGameInput()
        {
            throw new NotImplementedException();
        }

        private void printResults()
        {
            throw new NotImplementedException();
        }

        private bool gameIsOver()
        {
            throw new NotImplementedException();
        }



        private bool makePlayerMove()
        {
            MoveData turnData;

            do
            {
                turnData = m_Menu.GetAndCheckUserInputForTurnDataMove();
            }
            while(!m_Engine.MakeValidGameMoveForCurrentPlayer(turnData.SelectedRow, turnData.SelectedColumn));

            return true; //need to add quit user request to GetAndCheckUserInputForTurnDataMove
        }
    }

}
