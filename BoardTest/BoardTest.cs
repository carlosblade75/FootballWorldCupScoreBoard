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

        [TestMethod]
        public void CheckBoard_Add_Match_Score_Zero()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 1);
            Assert.IsTrue(summary[0].LocalScore == 0);
            Assert.IsTrue(summary[0].AwayScore == 0);
        }

        [TestMethod]
        public void CheckBoard_Update_Match_Score()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            board.UpdateScore(MXO_TEAM, CAD_TEAM, 1, 0);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 1);
            Assert.IsTrue(summary[0].LocalScore == 1);
            Assert.IsTrue(summary[0].AwayScore == 0);
        }

        [TestMethod]
        public void CheckBoard_Finish_Match()
        {
            var board = new Board();

            board.StartAGame(MXO_TEAM, CAD_TEAM);

            board.FinishGame(MXO_TEAM, CAD_TEAM);

            var summary = board.GetSummaryOfGames();

            Assert.IsTrue(summary.Count == 0);
        }
    }
}
