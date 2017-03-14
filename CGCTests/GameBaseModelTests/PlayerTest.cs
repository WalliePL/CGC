namespace CGCTests.GameBaseModelTests
{
    #region

    using GameBaseModel.Player;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class PlayerTest
    {
        #region Public Methods and Operators

        [TestCase]
        public void CrateEmptyPlayerTest()
        {
            var player = new Player();

            Assert.AreEqual(player.Name, "N/A");
            Assert.AreEqual(player.LastName, "N/A");
            Assert.AreEqual(player.Id, -1);
            Assert.AreEqual(player.IsChoosen, false);
        }

        [TestCase]
        public void SetPlayerIdTest()
        {
            var player = new Player { Id = -100 };

            Assert.AreEqual(player.Id, -100);

            player.Id = 100;
            Assert.AreEqual(player.Id, 100);
        }

        [TestCase]
        public void SetPlayerLastNameTest()
        {
            var player = new Player { LastName = "testLastName" };

            Assert.AreEqual(player.LastName, "testLastName");
        }

        [TestCase]
        public void SetPlayerNameTest()
        {
            var player = new Player { Name = "testName" };

            Assert.AreEqual(player.Name, "testName");
        }

        #endregion
    }
}