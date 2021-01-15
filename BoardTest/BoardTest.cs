using GeneralBoard;
using GeneralBoard.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardTest
{
    [TestClass]
    public class BoardTesting
    {
        private const string MXO_TEAM = "MXO";
        private const string CAD_TEAM = "CAD";
        private const string SPA_TEAM = "SPA";
        private const string BZL_TEAM = "BZL";
        private const string GRD_TEAM = "GRD";
        private const string FRZ_TEAM = "FRZ";
        private const string URG_TEAM = "URG";
        private const string ITA_TEAM = "ITA";
        private const string ARG_TEAM = "ARG";
        private const string AUS_TEAM = "AUS";

        [TestMethod]
        public void CheckBoard_Start_No_Match()
        {
            var board = new Board();

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 0);
        }

        #region StartGame Method

        [TestMethod]
        public void CheckBoard_Add_Match_Score_Zero()
        {
            var board = new Board();

            var result = board.StartAGame(MXO_TEAM, CAD_TEAM);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 1);
            Assert.IsTrue(result == ErrorType.NoMistake);
            Assert.IsTrue(summary[0].LocalScore == 0);
            Assert.IsTrue(summary[0].AwayScore == 0);
        }

        [TestMethod]
        public void CheckBoard_ErrorType_When_Add_Match_LocalTeam_Wrong()
        {
            var board = new Board();

            var result =  board.StartAGame("XXX", CAD_TEAM);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(result == ErrorType.LocalTeamNoExist);
            Assert.IsTrue(summary.Count == 0);
        }

        [TestMethod]
        public void CheckBoard_ErrorType_When_Add_Match_AwayTeam_Wrong()
        {
            var board = new Board();

            var result = board.StartAGame(CAD_TEAM, "XXX");

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(result == ErrorType.AwayTeamNoExist);
            Assert.IsTrue(summary.Count == 0);
        }

        [TestMethod]
        public void CheckBoard_ErrorType_When_Add_Match_BothTeam_Wrong()
        {
            var board = new Board();

            var result = board.StartAGame("YYY", "XXX");

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(result == ErrorType.NoTeamNoExist);
            Assert.IsTrue(summary.Count == 0);
        }

        #endregion

        [TestMethod]
        public void CheckBoard_Update_Match_Score()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.UpdateScore(MXO_TEAM, CAD_TEAM, 1, 0);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 1);
            Assert.IsTrue(result == ErrorType.NoMistake);
            Assert.IsTrue(summary[0].LocalScore == 1);
            Assert.IsTrue(summary[0].AwayScore == 0);
        }

        [DataTestMethod]
        [DataRow(-1,0)]
        [DataRow(4, -2)]
        [DataRow(-1, -3)]
        public void CheckBoard_Update_Match_Score_Negative(int localScore, int awayScore)
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.UpdateScore(MXO_TEAM, CAD_TEAM, (short)localScore, (short)awayScore);

            Assert.IsTrue(result == ErrorType.ScoreNegavite);
        }

        [TestMethod]
        public void CheckBoard_Update_Match_Local_Team_Wrong()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.UpdateScore("XXX", CAD_TEAM, 1, 0);

            Assert.IsTrue(result == ErrorType.LocalTeamNoExist);
        }

        [TestMethod]
        public void CheckBoard_Update_Match_Away_Team_Wrong()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.UpdateScore(MXO_TEAM, "XXX", 1, 0);

            Assert.IsTrue(result == ErrorType.AwayTeamNoExist);
        }

        [TestMethod]
        public void CheckBoard_Update_Match_Both_Team_Wrong()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.UpdateScore("YYY", "XXX", 1, 0);

            Assert.IsTrue(result == ErrorType.NoTeamNoExist);
        }

        #region Finish Method

        [TestMethod]
        public void CheckBoard_Finish_Match()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            board.FinishGame(MXO_TEAM, CAD_TEAM);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 0);
        }

        [TestMethod]
        public void CheckBoard_Finish_Local_Team_Wrong()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.FinishGame("XXX", CAD_TEAM);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 1);
            Assert.IsTrue(result == ErrorType.LocalTeamNoExist);
        }

        [TestMethod]
        public void CheckBoard_Finish_Away_Team_Wrong()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.FinishGame(MXO_TEAM, "XXX");

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 1);
            Assert.IsTrue(result == ErrorType.AwayTeamNoExist);
        }

        [TestMethod]
        public void CheckBoard_Finish_Both_Team_Wrong()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var result = board.FinishGame("YYY", "XXX");

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 1);
            Assert.IsTrue(result == ErrorType.NoTeamNoExist);
        }

        #endregion

        [TestMethod]
        public void CheckGetSummary()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);
            board.StartAGame(SPA_TEAM, BZL_TEAM);
            board.StartAGame(GRD_TEAM, FRZ_TEAM);
            board.StartAGame(URG_TEAM, ITA_TEAM);
            board.StartAGame(ARG_TEAM, AUS_TEAM);

            board.UpdateScore(MXO_TEAM, CAD_TEAM, 0, 5);
            board.UpdateScore(SPA_TEAM, BZL_TEAM, 10, 2);
            board.UpdateScore(GRD_TEAM, FRZ_TEAM, 2, 2);
            board.UpdateScore(URG_TEAM, ITA_TEAM, 6, 6);
            board.UpdateScore(ARG_TEAM, AUS_TEAM, 3, 1);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 5);

            Assert.IsTrue(summary[0].LocalScore == 6);
            Assert.IsTrue(summary[0].AwayScore == 6);

            Assert.IsTrue(summary[1].LocalScore == 10);
            Assert.IsTrue(summary[1].AwayScore == 2);

            Assert.IsTrue(summary[2].LocalScore == 0);
            Assert.IsTrue(summary[2].AwayScore == 5);

            Assert.IsTrue(summary[3].LocalScore == 3);
            Assert.IsTrue(summary[3].AwayScore == 1);

            Assert.IsTrue(summary[4].LocalScore == 2);
            Assert.IsTrue(summary[4].AwayScore == 2);
        }
    }
}
