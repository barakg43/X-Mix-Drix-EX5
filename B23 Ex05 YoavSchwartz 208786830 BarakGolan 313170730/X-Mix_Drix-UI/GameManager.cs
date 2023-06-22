using System;
using System.Text;
using System.Windows.Forms;
using Engine;
using Screen = Ex02.ConsoleUtils.Screen;

namespace X_Mix_Drix_UI
{
    public class GameManager
    {

        private readonly GameEngine r_Engine;
        private readonly Menu r_Menu;
        private BoardPrinter m_BoardPrinter;

        private GameDisplay m_GameBoardDisplay;
        private readonly GameSetting r_GameSetting;

        public GameManager()
        {
            r_Menu = new Menu();
            r_Engine = new GameEngine();
            r_GameSetting = new GameSetting();
        }

        public void RunGame()
        { 
            DialogResult gameSettingDialogResult =  r_GameSetting.ShowDialog();

           if (gameSettingDialogResult == DialogResult.OK)
           {
               createGameDisplay();
           }

        
            //bool isPlayerWantToQuit = false;

            //while(!isPlayerWantToQuit)
            //{
            //    clearScreenAndPrintBoard();
            //    r_Menu.PrintCurrentPlayerTurn(r_Engine.GetCurrentTurnPlayerName().ToString());
            //    markPlayerMoveInGameBoard();
            //    if(r_Engine.IsSessionOver)
            //    {
            //        clearScreenAndPrintBoard();
            //        printResults(r_Engine.IsSessionHaveWinner);
            //        isPlayerWantToQuit = r_Menu.GetEndOfGameInput();
            //        if(!isPlayerWantToQuit)
            //        {
            //            r_Engine.StartNewGameSession();
            //        }
            //    }
            //}
        }

        private void setupGameEngineSetting()
        {
            if(r_GameSetting.IsPlayingVsComputer)
            {
                r_Engine.Create2Players(ePlayerName.Player1, ePlayerName.Computer);
            }
            else
            {
                r_Engine.Create2Players(ePlayerName.Player1, ePlayerName.Player2);
            }
            r_Engine.CreateNewEmptyGameBoard(r_GameSetting.BoardSize);
            r_Engine.SessionOverNotifier += sessionOver;
            r_Engine.PlayerScoreUpdater += m_GameBoardDisplay.UpdateScore;
            r_Engine.ValidMoveTurnNotifier += markPlayerMoveInGameBoard;
        }
        
        private void createGameDisplay()
        {
            m_BoardPrinter = new BoardPrinter(r_GameSetting.BoardSize);

            m_GameBoardDisplay = new GameDisplay(
                r_GameSetting.Player1Name,
                r_GameSetting.Player2Name,
                r_GameSetting.BoardSize);
            m_GameBoardDisplay.RegisterForCellBoardClickedEvent(r_Engine.MakeValidGameMoveForCurrentPlayer);
            setupGameEngineSetting();
            m_GameBoardDisplay.ShowDialog();
        }

        private void sessionOver(eSessionWinner i_SessionWinner)
        {
    
            switch (i_SessionWinner)
            {
                case eSessionWinner.FirstPlayer:
                    checkIfWantPlayingAnotherSession(
                        m_GameBoardDisplay.AnnounceSessionWinnerAndAskForNewSession(r_GameSetting.Player1Name));
                    break;
                case eSessionWinner.SecondPlayer:
                    checkIfWantPlayingAnotherSession(
                        m_GameBoardDisplay.AnnounceSessionWinnerAndAskForNewSession(r_GameSetting.Player2Name));
                    break;
                case eSessionWinner.Tie:
                    checkIfWantPlayingAnotherSession(
                        m_GameBoardDisplay.AnnounceSessionTieAndAskForNewSession());
                    break;
            }
            Screen.Clear();
            //m_GameBoardDisplay.UpdateScore(r_Engine.FirstPlayerScore, r_Engine.SecondPlayerScore);
            //if(r_Engine.IsSessionHaveWinner)
            //{
            //    //TODO: Winner dialogue
            //}
            ////TODO: Tie dialogue
        }

        private void checkIfWantPlayingAnotherSession(DialogResult i_DialogResult)
        {
            if(i_DialogResult == DialogResult.Yes)
            {
                m_GameBoardDisplay.StartNewGameSession();
                r_Engine.StartNewGameSession();
            }
            else if(i_DialogResult ==DialogResult.No)
            {
                m_GameBoardDisplay.Close();
            }
        }

        private void markPlayerMoveInGameBoard(MoveData i_TurnData)
        {
            clearScreenAndPrintBoard();
            m_GameBoardDisplay.ChangeCellBoardValue(i_TurnData);
        }
      

        private void markPlayerMoveInGameBoard()
        {
            CellBoardCoordinate turnData;
            bool currentPlayerWantsToQuit = false;
            eCellError cellError = eCellError.NoError;

            if(r_Engine.GetCurrentTurnPlayerName() == ePlayerName.Computer)
            {
              //  r_Engine.MakeComputerMoveInHisTurn();
            }
            else
            {
                //do
                //{
                //    turnData = r_Menu.GetAndCheckUserInputForTurnDataMove(ref currentPlayerWantsToQuit, cellError);
                //}
                //while(!r_Engine.MakeValidGameMoveForCurrentPlayer(turnData, currentPlayerWantsToQuit, out cellError));
            }
        }

        private void clearScreenAndPrintBoard()
        {
   //         Screen.Clear();
            m_BoardPrinter.PrintGameBoard(r_Engine.GetBoard());
        }
    }
}