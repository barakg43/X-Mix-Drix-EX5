namespace Engine
{
    public class Player
    {
        private int m_Score;

        public Player(ePlayerType i_Type, eBoardCellValue i_GameSymbol)
        {
            Type = i_Type;
            GameSymbol = i_GameSymbol;
            m_Score = 0;
        }

        public ePlayerType Type
        {
            get;
        }

        public void IncrementGameSessionsScore()
        {
            m_Score++;
        }

        public eBoardCellValue GameSymbol
        {
            get;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
        }
    }
}
