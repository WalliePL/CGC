namespace CGCTests.GameBaseModelTests
{
    #region

    using System;
    using System.Linq;

    using GameBaseModel.Player;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class PlayersTest
    {
        #region Public Methods and Operators

        [TestCase]
        public void AddNullPlayerTest()
        {
            var players = new Players();
            Player player = null;
            Assert.That(() => players.Add(player), Throws.TypeOf<NullReferenceException>());
        }

        [TestCase]
        public void AddPlayerTest()
        {
            var players = new Players();
            var player = new Player();
            players.Add(player);
            Assert.AreEqual(players.Count, 1);
        }

        [TestCase]
        public void CheckIfDefaultPlayersListIsEmptyTest()
        {
            var players = new Players();
            Assert.IsFalse(players.ToList().Any());
        }

        [TestCase]
        public void CloningTest()
        {
            var players = new Players();
            var player = new Player();
            players.Add(player);

            var copy = players.GetClone();
            Assert.IsFalse(ReferenceEquals(players, copy));
        }

        [TestCase]
        public void DefaultPlayersToListTest()
        {
            var players = new Players();
            Assert.IsEmpty(players.ToList());
        }

        [TestCase]
        public void RemoveNotAvaiableOnListPlayerTest()
        {
            var players = new Players();
            Assert.That(() => players.Remove(new Player()), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [TestCase]
        public void RemoveNullPlayerTest()
        {
            var players = new Players();
            Assert.That(() => players.Remove(null), Throws.TypeOf<NullReferenceException>());
        }

        [TestCase]
        public void RemovePlayerTest()
        {
            var players = new Players();
            var player = new Player();
            players.Add(player);
            players.Remove(player);
            Assert.IsFalse(players.ToList().Contains(player));
        }

        #endregion
    }
}