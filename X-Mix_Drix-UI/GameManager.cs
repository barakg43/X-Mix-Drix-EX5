using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Ex02;

namespace X_Mix_Drix_UI
{
    public class GameManager
    {
        private const string k_WinnerSessionStringFormat = @"The winner is {0}!";
        private const string k_ScoreDisplayStringFormat = @"Score Balance: {0} - {1}
              {2} - {3}";
        private const string k_TieMsg = "Its a tie";
        private readonly GameEngine r_Engine;
        private readonly Menu r_Menu;
        private BoardPrinter m_BoardPrinter;
        
        public GameManager()
        {
            r_Menu = new Menu();
            r_Engine = new GameEngine();
        }

        public void RunMenu()
        {
            int size;
            bool isUsersWantToPlay = true;

            while (isUsersWantToPlay)
            {
                r_Menu.PrintMainMenu();
                switch (r_Menu.GetAndCheckUserInputForMenuItem())
                {
                    case Menu.eMenuOptions.StartGameAgainstPc:
                        r_Engine.Create2Players(ePlayerName.Player1, ePlayerName.Computer);
                    break;
                    case Menu.eMenuOptions.StartGameAgainstPlayer:
                        r_Engine.Create2Players(ePlayerName.Player1, ePlayerName.Player2);
                        break;
                    case Menu.eMenuOptions.Quit:
                        isUsersWantToPlay = false;
                        break;
                }

                if(isUsersWantToPlay)
                {
                    size = r_Menu.GetAndCheckUserInputForBoardSize(r_Engine.GetMinBoardSize(), r_Engine.GetMaxBoardSize());
                    m_BoardPrinter = new BoardPrinter((ushort)size);
                    r_Engine.CreateNewEmptyGameBoard((ushort)size);
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
                r_Menu.PrintCurrentPlayerTurn(r_Engine.GetCurrentTurnPlayerName().ToString());
                makePlayerMove();
                if(r_Engine.IsSessionOver)
                {
                    clearScreenAndPrintBoard();
                    printResults(r_Engine.IsSessionHaveWinner);
                    isPlayerWantToQuit = r_Menu.GetEndOfGameInput();
                    if(!isPlayerWantToQuit)
                    {
                        r_Engine.StartNewGameSession();
                    }
                }
            }
        }

        private void printResults(bool i_IsSessionHasPlayerWon)
        {
            Player[] players = r_Engine.GetPlayers();

            if(i_IsSessionHasPlayerWon)
            {
                Console.WriteLine(string.Format(k_WinnerSessionStringFormat, r_Engine.GetCurrentTurnPlayerName()));
            }
            else
            {
                Console.WriteLine(k_TieMsg);
            }

            Console.WriteLine(string.Format(k_ScoreDisplayStringFormat, players[0].Name, players[0].Score, players[1].Name, players[1].Score));
        }

        private void makePlayerMove()
        {
            CellBoardCoordinate turnData;
            bool currentPlayerWantsToQuit = false;
            eCellError cellError = eCellError.NoError;

            if(r_Engine.GetCurrentTurnPlayerName() == ePlayerName.Computer)
            {
                r_Engine.MakeComputerMoveInHisTurn();  
            }
            else
            {
                do
                {
                    turnData = r_Menu.GetAndCheckUserInputForTurnDataMove(ref currentPlayerWantsToQuit, cellError);
                }
                while (!r_Engine.MakeValidGameMoveForCurrentPlayer(turnData, currentPlayerWantsToQuit, out cellError));
            }
        }

        private void clearScreenAndPrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear(); 
            m_BoardPrinter.PrintGameBoard(r_Engine.GetBoard());
        }
    }
}
