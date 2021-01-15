using System;
using GeneralBoard.Interfaces;

namespace GeneralBoard.Models
{
    public class Match : IMatch
    {
        public ITeam LocalTeam { get; set; }
        public ITeam AwayTeam { get; set; }
        public short LocalScore { get; set; }
        public short AwayScore { get; set; }
        public DateTime StartMacthDate { get; set; }
    }
}
