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
                        //m_Menu.PrintSizeSelect();
                        m_Engine.Create2Players(ePlayerName.Player1, ePlayerName.Computer);
                        size = m_Menu.GetAndCheckUserInputForBoardSize(m_Engine.GetMinBoardSize(), m_Engine.GetMaxBoardSize());
                        m_BoardPrinter = new BoardPrinter((ushort)size);
                        m_Engine.CreateNewEmptyGameBoard((ushort)size);
                        runGame();
                        break;

                    case Menu.eMenuOptions.StartGameAgaintsPlayer:
                        m_Engine.Create2Players(ePlayerName.Player1,ePlayerName.Player2);
                        //m_Menu.PrintSizeSelect();
                        size = m_Menu.GetAndCheckUserInputForBoardSize(m_Engine.GetMinBoardSize(), m_Engine.GetMaxBoardSize());
                        m_BoardPrinter = new BoardPrinter((ushort)size);
                        m_Engine.CreateNewEmptyGameBoard((ushort)size);
                        runGame();
                        break;

                    case Menu.eMenuOptions.Quit:
                        return;
                }
            }
        }

        private void displayBoardOnConsole()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_BoardPrinter.PrintGameBoard(m_Engine.GetBoard());
        }
        private void runGame()
        {
            bool isPlayerWantToQuit = false;

            while(!isPlayerWantToQuit)
            {
                clearScreenAndPrintBoard();
                m_Menu.PrintCurrentPlayerTurn(m_Engine.GetCurrentTurnPlayerName().ToString());
                makePlayerMove();
                if(m_Engine.GameIsOver)
                {
                    clearScreenAndPrintBoard();
                    printResults();
                    isPlayerWantToQuit = m_Menu.GetEndOfGameInput();
                    if(!isPlayerWantToQuit)
                    {
                        m_Engine.StartNewGameSession();
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
            Player[] players = m_Engine.GetPlayers();
            Console.WriteLine(string.Format(
@"The winner is {0}!

Score Balance: {1} - {2}
               {3} - {4}",
m_Engine.GetCurrentTurnPlayerName(),
players[0].Name, players[0].Score,
players[1].Name, players[1].Score));
        }





        private void makePlayerMove()
        {
            CellBoardCoordinate turnData;
            bool currentPlayerWantsToQuit = false;
            eCellError cellError = eCellError.NoError;
            if(false && m_Engine.GetCurrentTurnPlayerName() == ePlayerName.Computer)
            {
                //generate move
            }
            else
            {
                do
                {
                    turnData = m_Menu.GetAndCheckUserInputForTurnDataMove(ref currentPlayerWantsToQuit, cellError);
                }
                while (!m_Engine.MakeValidGameMoveForCurrentPlayer(turnData, currentPlayerWantsToQuit, ref cellError));

            }
            
        }

        private void clearScreenAndPrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_BoardPrinter.PrintGameBoard(m_Engine.GetBoard());
        }
    }

}
