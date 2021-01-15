using System;

namespace GeneralBoard.Interfaces
{
    public interface IMatch
    {
        public ITeam LocalTeam { get; set; }
        public ITeam AwayTeam { get; set; }

        public short LocalScore { get; set; }
        public short AwayScore { get; set; }

        public DateTime StartMacthDate { get; set; }
    }
}
