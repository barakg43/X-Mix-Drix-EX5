using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class PlayerNew
    {
        private ushort m_Score;

        private readonly bool r_IsComputer;

        private readonly string r_PlayerName;

        private readonly eBoardCellValue r_GameSymbol;

        private const string k_ComputerName = "Computer";

        public PlayerNew(string i_PlayerName, eBoardCellValue i_GameSymbol)
        {
            m_Score = 0;
            r_PlayerName = i_PlayerName;
            r_GameSymbol = i_GameSymbol;
        }

        public PlayerNew(eBoardCellValue i_GameSymbol)
        {
            m_Score = 0;
            r_PlayerName = k_ComputerName;
            r_GameSymbol = i_GameSymbol;
            r_IsComputer = true;
        }

        public ushort Score => m_Score;

        public bool IsComputer => r_IsComputer;

        public string PlayerName => r_PlayerName;

        public eBoardCellValue GameSymbol => r_GameSymbol;

        public void IncrementGameSessionsScore()
        {
            m_Score++;
        }
    }
}
