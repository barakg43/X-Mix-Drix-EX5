using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class Player
    {
        private int m_Score;

        public Player(ePlayerName i_Name)
        {
            Name = i_Name;
            m_Score = 0;
        }

        public ePlayerName Name
        {
            get;
        }

        public void incrementGamesScore()
        {
            m_Score++;
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
