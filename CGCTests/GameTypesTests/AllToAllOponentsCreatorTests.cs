namespace CGCTests.GameTypesTests
{
    #region

    using System.Linq;

    using GameBaseModel.CgcExceptions;
    using GameBaseModel.GameTypes;
    using GameBaseModel.Player;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class AllToAllOponentsCreatorTests
    {
        #region Public Methods and Operators

        [TestCase]
        public void TestIfEmptyPlayerListsThrowException()
        {
            var players = new Players();
            var allToAll = new AllToAllOponentsCreator();
            Assert.That(() => allToAll.CrateOponentsForGames(players), Throws.TypeOf<CgcCannotCreateOponentsExceptions>());
        }

        [TestCase]
        public void TestIfGameWithManyPlayerCreateProperOponentsCollection()
        {
            var players = this.CreatePlayersCollection(4);
            var allToAll = new AllToAllOponentsCreator();
            var oponents = allToAll.CrateOponentsForGames(players);
            Assert.AreEqual(oponents.Count, 6);
        }

        [TestCase]
        public void TestIfGameWithOnePlayerThrowException()
        {
            var players = new Players { new Player() };
            var allToAll = new AllToAllOponentsCreator();
            Assert.That(() => allToAll.CrateOponentsForGames(players), Throws.TypeOf<CgcCannotCreateOponentsExceptions>());
        }

        [TestCase]
        public void TestIfGameWithThreePlayerCreateProperOponentsCollection()
        {
            var players = this.CreatePlayersCollection(3);
            var allToAll = new AllToAllOponentsCreator();
            var oponents = allToAll.CrateOponentsForGames(players);
            Assert.AreEqual(oponents.Count, 3);
        }

        [TestCase]
        public void TestIfGameWithTwoPlayerCreateProperOponentsCollection()
        {
            var players = this.CreatePlayersCollection(2);
            var allToAll = new AllToAllOponentsCreator();
            var oponents = allToAll.CrateOponentsForGames(players);
            Assert.AreEqual(oponents.Count, 1);
            Assert.IsTrue(oponents.First().TeamA.Count == 1);
            Assert.IsTrue(oponents.First().TeamB.Count == 1);
            Assert.IsTrue(oponents.First().TeamA != oponents.First().TeamB);
            Assert.IsTrue(oponents.First().TeamA.First().Name != oponents.First().TeamB.First().Name);
            Assert.IsFalse(string.IsNullOrEmpty(oponents.First().TeamA.First().Name));
            Assert.IsFalse(string.IsNullOrEmpty(oponents.First().TeamB.First().Name));
            Assert.IsTrue(oponents.First().TeamA.First().Name == "player0" || oponents.First().TeamA.First().Name == "player1");
        }

        #endregion

        #region Methods

        private Players CreatePlayersCollection(int howManyPlayers)
        {
            var players = new Players();

            for (var i = 0; i < howManyPlayers; i++)
            {
                players.Add(new Player { Name = "player" + i });
            }

            return players;
        }

        #endregion
    }
}