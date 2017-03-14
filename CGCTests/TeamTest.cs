namespace CGCTests
{
    #region

    using GameBaseModel.Team;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    internal class TeamTest
    {
        #region Public Methods and Operators

        [TestCase]
        public void CrateEmptyTeamTest()
        {
            var team = new Team();

            Assert.AreEqual(team.Name, "N/A");
            Assert.AreEqual(team.Id, -1);
            Assert.IsEmpty(team.Players, "Player list in not empty");
        }

        #endregion
    }
}