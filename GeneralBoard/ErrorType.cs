using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBoard
{
    public enum ErrorType
    {
        NoMistake = 0,
        LocalTeamInMatchNoExist = -1,
        AwayTeamInMatchNoExist = -2,
        MatchNoExist = -3,
        LocalTeamAlreadyPlaying = -4,
        AwayTeamAlreadyPlaying = -5,
        MatchAlreadyPlaying = -6,
        LocalTeamNoExist = -7,
        AwayTeamNoExist = -8,
        NoTeamNoExist = -9,
        ScoreNegavite = -10,
        LocalTeamNoPlaying = -11,
        AwayTeamNoPlaying = -12,
        MatchNoPlaying = -13,
        ErrorNoControlled = -100
    }
}
