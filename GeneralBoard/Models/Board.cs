using System;
using System.Linq;
using System.Collections.Generic;
using GeneralBoard.Interfaces;
using GeneralBoard.Repository;

namespace GeneralBoard.Models
{
    public class Board : IBoard
    {
        private List<IMatch> currentMacthes;

        private IRepositoryTeam listTeam;

        public Board()
        {
            currentMacthes = new List<IMatch>();

            listTeam = new ListTeam();
        }

        public Board(IRepositoryTeam list)
        {
            currentMacthes = new List<IMatch>();

            listTeam = list;
        }

        #region Public Methods

        /// <summary>
        /// We create a game 
        /// </summary>
        /// <param name="codeLocalTeam">Code of the local team</param>
        /// <param name="codeAwayTeam">Code of the away team</param>
        /// <returns>Type of te error (ErrorType)</returns>
        public ErrorType StartAGame(string codeLocalTeam, string codeAwayTeam)
        {
            ErrorType error;

            try
            {
                error = CheckMatchAndTeamExist(codeLocalTeam, codeAwayTeam);

                if (error == ErrorType.NoMistake)
                {
                    error = CheckTeamsAreNotPlaying(codeLocalTeam, codeAwayTeam);

                    if (error == ErrorType.NoMistake)
                    {
                        // Look for the name of the teams in the "repository"
                        var nameLocalTeam = listTeam.GetTeamNameByCode(codeLocalTeam);
                        var nameAwayTeam = listTeam.GetTeamNameByCode(codeAwayTeam);

                        currentMacthes.Add(new Match
                        {
                            AwayScore = 0,
                            AwayTeam = new Team { CodeTeam = codeAwayTeam, NameTeam = nameAwayTeam },
                            LocalScore = 0,
                            LocalTeam = new Team { CodeTeam = codeLocalTeam, NameTeam = nameLocalTeam },
                            StartMacthDate = DateTime.Now
                        });
                    }
                }

            } 
            catch
            {
                error = ErrorType.ErrorNoControlled;
            }

            return error;
        }

        /// <summary>
        /// Finish a game
        /// </summary>
        /// <param name="codeLocalTeam">Code of the local team</param>
        /// <param name="codeAwayTeam">Code of the away team</param>
        /// <returns>Type of te error (ErrorType)</returns>
        public ErrorType FinishGame(string codeLocalTeam, string codeAwayTeam)
        {
            ErrorType error;

            try
            {
                error = CheckMatchAndTeamExist(codeLocalTeam, codeAwayTeam);

                if (error == ErrorType.NoMistake)
                {
                    error = CheckMatchIsPlaying(codeLocalTeam, codeAwayTeam);

                    if (error == ErrorType.NoMistake)
                    {
                        var match = currentMacthes.Where(m => m.LocalTeam.CodeTeam == codeLocalTeam).Single();

                        currentMacthes.Remove(match);
                    }
                }
            } 
            catch
            {
                error = ErrorType.ErrorNoControlled;
            }

            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeLocalTeam">Code of the local team</param>
        /// <param name="codeAwayTeam">Code of the away team</param>
        /// <param name="localScore">local score to update</param>
        /// <param name="awayScore">away score to update</param>
        /// <returns>Type of te error (ErrorType)</returns>
        public ErrorType UpdateScore(string codeLocalTeam, string codeAwayTeam, short localScore, short awayScore)
        {
            ErrorType error;

            try
            {
                if (localScore < 0 || awayScore < 0)
                {
                    error = ErrorType.ScoreNegavite;
                }
                else
                {
                    error = CheckMatchAndTeamExist(codeLocalTeam, codeAwayTeam);

                    if (error == ErrorType.NoMistake)
                    {
                        error = CheckMatchIsPlaying(codeLocalTeam, codeAwayTeam);

                        if (error == ErrorType.NoMistake)
                        {
                            var match = currentMacthes.Where(team => team.LocalTeam.CodeTeam == codeLocalTeam && team.AwayTeam.CodeTeam == codeAwayTeam).Single();

                            match.LocalScore = localScore;
                            match.AwayScore = awayScore;
                        }
                    }
                }
            }
            catch
            {
                error = ErrorType.ErrorNoControlled;
            }

            return error;
        }

        /// <summary>
        /// Get all the games there are being playing now
        /// </summary>
        /// <returns>List of the games</returns>
        public List<Summary> GetSummaryOfGames()
        {
            var list = new List<Summary>();

            var select = from s in  currentMacthes select new { localScore = s.LocalScore, 
                                                                awayScore = s.AwayScore, 
                                                                nameLocalTeam = s.LocalTeam.NameTeam, 
                                                                nameAwayTeam = s.AwayTeam.NameTeam, 
                                                                totalScore = s.LocalScore + s.AwayScore, 
                                                                startDate = s.StartMacthDate 
                                                                };

            var listMatch = select.OrderByDescending(d => d.totalScore).ThenByDescending(d => d.startDate).ToList();

            foreach(var match in listMatch)
            {
                list.Add(new Summary { AwayTeam = match.nameAwayTeam, AwayScore = match.awayScore, LocalScore = match.localScore, LocalTeam = match.nameLocalTeam });
            }

            return list;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if the team are not playing in that moment
        /// </summary>
        /// <param name="codeLocalTeam">Code of the local team</param>
        /// <param name="codeAwayTeam">Code of the away tem</param>
        /// <returns>MatchAlreadyPlaying, LocalTeamAlreadyPlaying or AwayTeamAlreadyPlaying</returns>
        private ErrorType CheckTeamsAreNotPlaying(string codeLocalTeam, string codeAwayTeam)
        {
            ErrorType error = ErrorType.NoMistake;

            var localTeamPlaying = currentMacthes.Where(team => team.LocalTeam.CodeTeam == codeLocalTeam).Count() == 1;
            
            var awayTeamPlaying = currentMacthes.Where(team => team.AwayTeam.CodeTeam == codeAwayTeam).Count() == 1;

            if (localTeamPlaying && awayTeamPlaying)
            {
                error = ErrorType.MatchAlreadyPlaying;
            }
            else if (localTeamPlaying || awayTeamPlaying)
            {
                error = localTeamPlaying ? ErrorType.LocalTeamAlreadyPlaying : ErrorType.AwayTeamAlreadyPlaying;
            }

            return error;
        }

        /// <summary>
        /// Chech if the team are playin in that moment
        /// </summary>
        /// <param name="codeLocalTeam">Code of the local team</param>
        /// <param name="codeAwayTeam">Code of the away tem</param>
        /// <returns>MatchNoExist, LocalTeamInMatchNoExist or AwayTeamInMatchNoExist</returns>
        private ErrorType CheckMatchIsPlaying(string codeLocalTeam, string codeAwayTeam)
        {
            ErrorType error = ErrorType.NoMistake;

            var localTeamNoPlaying = currentMacthes.Where(team => team.LocalTeam.CodeTeam == codeLocalTeam).Count() == 0;

            var awayTeamNoPlaying = currentMacthes.Where(team => team.AwayTeam.CodeTeam == codeAwayTeam).Count() == 0;

            if (localTeamNoPlaying && awayTeamNoPlaying)
            {
                error = ErrorType.MatchNoPlaying;
            }
            else if (localTeamNoPlaying || awayTeamNoPlaying)
            {
                error = localTeamNoPlaying ? ErrorType.LocalTeamNoPlaying : ErrorType.AwayTeamNoPlaying;
            }

            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeLocalTeam">Code of the local team</param>
        /// <param name="codeAwayTeam">Code of the away tem</param>
        /// <returns>NoTeamNoExist, LocalTeamNoExist or AwayTeamNoExist</returns>
        private ErrorType CheckMatchAndTeamExist(string codeLocalTeam, string codeAwayTeam)
        {
            ErrorType error = ErrorType.NoMistake;

            var localTeam = !listTeam.ExistTeam(codeLocalTeam);

            var awayTeam = !listTeam.ExistTeam(codeAwayTeam);

            if (localTeam && awayTeam)
            {
                error = ErrorType.NoTeamNoExist;
            }
            else if (localTeam || awayTeam)
            {
                error = localTeam ? ErrorType.LocalTeamNoExist : ErrorType.AwayTeamNoExist;
            }

            return error;
        }

        #endregion
    }
}
