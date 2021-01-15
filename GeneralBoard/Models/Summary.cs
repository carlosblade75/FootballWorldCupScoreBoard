using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBoard.Models
{
    public class Summary
    {
        public string LocalTeam { get; set; }
        public string AwayTeam { get; set; }

        public short LocalScore { get; set; }
        public short AwayScore { get; set; }
    }
}
