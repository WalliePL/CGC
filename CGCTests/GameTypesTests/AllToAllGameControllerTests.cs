namespace CGCTests.GameTypesTests
{
    #region

    using System;
    using System.Linq;

    using GameBaseModel.CgcExceptions;
    using GameBaseModel.GameTypes;
    using GameBaseModel.Player;

    using NUnit.Framework;
    using NUnit.Framework.Internal;

    #endregion

    [TestFixture]
    internal class AllToAllGameControllerTests
    {
        #region Public Methods and Operators

        [TestCase]
        public void EndAllGamesTest()
        {
            var players = this.CreatePlayersCollection(2);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.CurrentGameOponents();
            var winners = oponents.TeamA;
            gameController.EndGame(winners);
            Assert.IsFalse(gameController.IsNextGameAvailable);

            foreach (var winner in winners)
            {
                Assert.IsTrue(gameController.Players.Find(player => player.Player == winner).Points == 1);
            }
        }

        [TestCase]
        public void IsACopyOfPlayers()
        {
            var players = this.CreatePlayersCollection(2);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            foreach (var playerInController in gameController.Players)
            {
                var referenceFound = players.Any(player => {
                                                               Console.WriteLine(player);
                                                               Console.WriteLine(playerInController.Player);
                                                               return ReferenceEquals(player, playerInController.Player);
                });
                Assert.IsFalse(referenceFound, string.Format("Player {0} was not copied. Feference found", playerInController.Player.Name));
            }
        }

        [TestCase]
        public void EndAllGamesViaSpecyficOponentTest()
        {
            var players = this.CreatePlayersCollection(2);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.GamesInSortedQueue.Select(games => games.Oponents).ToList();
            gameController.EndGame(oponents[0], oponents[0].TeamA);
            Assert.IsFalse(gameController.IsNextGameAvailable);
        }

        [TestCase]
        public void EndGamesFromManyTest()
        {
            var players = this.CreatePlayersCollection(3);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.CurrentGameOponents();
            gameController.EndGame(oponents.TeamA);
            Assert.IsTrue(gameController.IsNextGameAvailable);
        }

        [TestCase]
        public void EndGamesFromManyViaSpecyficOponentTest()
        {
            var players = this.CreatePlayersCollection(3);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.GamesInSortedQueue.Select(games => games.Oponents).ToList();
            
            gameController.EndGame(oponents[0], oponents[0].TeamA);
            Assert.IsTrue(gameController.IsNextGameAvailable);
        }

        [TestCase]
        public void EndGamesTie()
        {
            var players = this.CreatePlayersCollection(2);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.CurrentGameOponents();
            var winners = oponents.TeamA.Concat(oponents.TeamB).ToList();
            gameController.EndGameAsTie();
            Assert.IsFalse(gameController.IsNextGameAvailable);

            foreach (var winner in winners)
            {
                Assert.IsTrue(gameController.Players.Find(player => player.Player == winner).Points == 1);
            }
        }

        [TestCase]
        public void EndGamesWithPlayerNotInCurrentOponentGameCollectionWithSpecyficOponentTestt()
        {
            var players = this.CreatePlayersCollection(3);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.GamesInSortedQueue.Select(games => games.Oponents).ToList();
            
            Assert.That(
                () => gameController.EndGame(oponents[1], oponents[0].TeamB), 
                Throws.TypeOf<CgcExceptionCannotEndGameWinnerWasNotOponentInChosenGame>());
        }

        [TestCase]
        public void EndSocondGamesFromManyViaSpecyficOponentTest()
        {
            var players = this.CreatePlayersCollection(3);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.GamesInSortedQueue.Select(games => games.Oponents).ToList();
            
            gameController.EndGame(oponents[1], oponents[1].TeamA);
            Assert.IsTrue(gameController.IsNextGameAvailable);
        }

        [TestCase]
        public void IsNextGameAvailableTest()
        {
            var players = this.CreatePlayersCollection(2);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            Assert.IsTrue(gameController.IsNextGameAvailable);
        }

        [TestCase]
        public void NextGameGetFirstGameTest()
        {
            var players = this.CreatePlayersCollection(4);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.GamesInSortedQueue.Select(games => games.Oponents).ToList();
            Assert.IsTrue(gameController.CurrentGameOponents() == oponents.First());
        }

        [TestCase]
        public void RunFirstGameTest()
        {
            var players = this.CreatePlayersCollection(4);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());
            var oponents = gameController.GamesInSortedQueue.Select(games => games.Oponents).ToList();
            Assert.IsTrue(gameController.CurrentGameOponents() == oponents.First());
        }

        [TestCase]
        public void TestIfEmpytPlayerCollectionAddedToControllerThrowsException()
        {
            var players = this.CreatePlayersCollection(0);
            Assert.That(() => new AllToAllGameController(players, new AllToAllOponentsCreator()), Throws.TypeOf<CgcCannotCreateOponentsExceptions>());
        }

        [TestCase]
        public void TestIfPlayerCollectionWithOnePlayerAddedToControllerThrowsException()
        {
            var players = this.CreatePlayersCollection(1);
            Assert.That(() => new AllToAllGameController(players, new AllToAllOponentsCreator()), Throws.TypeOf<CgcCannotCreateOponentsExceptions>());
        }

        [TestCase]
        public void TestIfSortedCollectionHas2MaxGamesInRow()
        {
            var players = this.CreatePlayersCollection(4);
            var gameController = new AllToAllGameController(players, new AllToAllOponentsCreator());

            var oponents = gameController.GamesInSortedQueue.Select(games => games.Oponents).ToList();
            
            Assert.AreEqual(oponents.Count, 6);
            Assert.IsTrue(oponents[0].TeamA.First().Name == "player0" && oponents[0].TeamB.First().Name == "player1");
            Assert.IsTrue(oponents[1].TeamA.First().Name == "player0" && oponents[1].TeamB.First().Name == "player2");
            Assert.IsTrue(oponents[2].TeamA.First().Name == "player1" && oponents[2].TeamB.First().Name == "player2");
            Assert.IsTrue(oponents[3].TeamA.First().Name == "player1" && oponents[3].TeamB.First().Name == "player3");
            Assert.IsTrue(oponents[4].TeamA.First().Name == "player2" && oponents[4].TeamB.First().Name == "player3");
            Assert.IsTrue(oponents[5].TeamA.First().Name == "player0" && oponents[5].TeamB.First().Name == "player3");
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