namespace Engine
{
    public class Player
    {
        public Player(ePlayerType i_Type, eBoardCellValue i_GameSymbol)
        {
            Type = i_Type;
            GameSymbol = i_GameSymbol;
            Score = 0;
        }

        public ePlayerType Type { get; }

        public eBoardCellValue GameSymbol { get; }

        public int Score { get; private set; }

        public void IncrementGameSessionsScore()
        {
            Score++;
        }
    }
}