namespace Engine
{
    class Player
    {
        private int m_Score;

        public Player(ePlayerName i_Name, eBoardCellValue i_GameSymbol)
        {
            Name = i_Name;
            GameSymbol = i_GameSymbol;
            m_Score = 0;
        }

        public ePlayerName Name
        {
            get;
        }

        public void incrementGameSessionsScore()
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
