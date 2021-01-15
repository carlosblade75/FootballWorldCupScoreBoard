using GeneralBoard.Models;
using System;

namespace FootballWorldCupScoreBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Football World Cup Board!!");

            var board = new Board();
            
            /*
            board.StartAGame("MXO2", "CAD");
            board.StartAGame("MXO", "CAD2");
            board.StartAGame("MXO3", "CAD3");
            */

            board.StartAGame("MXO", "CAD");
            board.StartAGame("SPA", "BZL");
            board.StartAGame("GRD", "FRZ");
            board.StartAGame("URG", "ITA");
            board.StartAGame("ARG", "AUS");

            board.UpdateScore("MXO", "CAD", 0, 5);
            board.UpdateScore("SPA", "BZL", 10, 2);
            board.UpdateScore("GRD", "FRZ", 2, 2);
            board.UpdateScore("URG", "ITA", 6, 6);
            board.UpdateScore("ARG", "AUS", 3, 1);

            var result = board.GetSummaryOfGames();
        }
    }
}
