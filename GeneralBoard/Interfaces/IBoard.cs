using System.Collections.Generic;
using GeneralBoard.Models;

namespace GeneralBoard.Interfaces
{
    public interface IBoard
    {
        public ErrorType StartAGame(string codeLocalTeam, string codeAwayTeam);

        public ErrorType FinishGame(string codeLocalTeam, string codeAwayTeam);

        public ErrorType UpdateScore(string codeLocalTeam, string codeAwayTeam, short localScore, short awayScore);

        public List<Summary> GetSummaryOfGames();
    }
}
