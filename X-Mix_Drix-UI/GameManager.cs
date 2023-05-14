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
        private GameEngine m_Engine;
        private const string k_WinnerSessionStringFormat = @"The winner is {0}!";
        private const string k_ScoreDisplayStringFormat = @"Score Balance: {0} - {1}
         {2} - {3}";
        public GameManager()
        {
            m_Menu = new Menu();
            m_Engine = new GameEngine();
        }

        public void RunMenu()
        {
            int size;
            bool isUsersWantToPlay = true;
            while (isUsersWantToPlay)
            {
                m_Menu.PrintMainMenu();
                switch (m_Menu.GetAndCheckUserInputForMenuItem())
                {
                    case Menu.eMenuOptions.StartGameAgainstPC:
                        m_Engine.Create2Players(ePlayerName.Player1, ePlayerName.Computer);
                    break;
                    case Menu.eMenuOptions.StartGameAgaintsPlayer:
                        m_Engine.Create2Players(ePlayerName.Player1,ePlayerName.Player2);
                        break;
                    case Menu.eMenuOptions.Quit:
                        isUsersWantToPlay = false;
                        break;
                }
                if(isUsersWantToPlay)
                {
                    size = m_Menu.GetAndCheckUserInputForBoardSize(m_Engine.GetMinBoardSize(), m_Engine.GetMaxBoardSize());
                    m_BoardPrinter = new BoardPrinter((ushort)size);
                    m_Engine.CreateNewEmptyGameBoard((ushort)size);
                    runGame();
                }
            }
        }

        private void runGame()
        {
            bool isPlayerWantToQuit = false;

            while(!isPlayerWantToQuit)
            {
                clearScreenAndPrintBoard();
                m_Menu.PrintCurrentPlayerTurn(m_Engine.GetCurrentTurnPlayerName().ToString());
                makePlayerMove();
                if(m_Engine.IsSessionOver)
                {
                    clearScreenAndPrintBoard();
                    printResults(m_Engine.IsSessionHaveWinner);
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

        private void printResults(bool i_IsSessionHasPlayerWon)
        {
            Player[] players = m_Engine.GetPlayers();
            if(i_IsSessionHasPlayerWon)
            {

                Console.WriteLine(string.Format(k_WinnerSessionStringFormat, m_Engine.GetCurrentTurnPlayerName()));
            }

            Console.WriteLine(string.Format(k_ScoreDisplayStringFormat, players[0].Name, players[0].Score, players[1].Name, players[1].Score));
        }
        private void makePlayerMove()
        {
            CellBoardCoordinate turnData;
            bool currentPlayerWantsToQuit = false;
            eCellError cellError = eCellError.NoError;
            if( m_Engine.GetCurrentTurnPlayerName() == ePlayerName.Computer)
            {
                m_Engine.MakeComputerMoveInHisTurn();  
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
        //    Ex02.ConsoleUtils.Screen.Clear(); //TODO: need to uncomment
            m_BoardPrinter.PrintGameBoard(m_Engine.GetBoard());
        }
    }

}
