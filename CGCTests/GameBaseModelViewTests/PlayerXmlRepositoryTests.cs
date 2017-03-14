namespace CGCTests.GameBaseModelViewTests
{
    #region

    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using GameBaseModel.Api;
    using GameBaseModel.Player;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class PlayerXmlRepositoryTests
    {
        #region Public Methods and Operators

        [TestCase]
        public void AddNullPlayerTestShouldThrowException()
        {
            var playerController = new PlayerXmlRepository();
            Assert.That(() => playerController.Add(null), Throws.TypeOf<NullReferenceException>());
        }

        [TestCase]
        public void AddPlayerIdTest()
        {
            var player = new Player { Name = "testName", LastName = "testLastName" };
            var playerController = new PlayerXmlRepository();
            playerController.Add(player);

            Assert.AreEqual(0, player.Id);
        }

        [TestCase]
        public void AddPlayerTest()
        {
            var playerController = new PlayerXmlRepository();
            this.AddDefaultPlayerToReposiotyCreator(playerController);

            Assert.AreEqual(1, playerController.GetPlayers().Count);
        }

        [TestCase]
        public void AddTwiceTheSamePlayerIdTest()
        {
            var player = new Player { Name = "testName", LastName = "testLastName" };
            var playerController = new PlayerXmlRepository();
            playerController.Add(player);

            Assert.AreEqual(player.Id, 0);

            playerController.Add(player);
            Assert.AreEqual(player.Id, 1);
        }

        [TestCase]
        public void AddTwiceTheSamePlayerTest()
        {
            var playerController = new PlayerXmlRepository();
            this.AddDefaultPlayerToReposiotyCreator(playerController);
            this.AddDefaultPlayerToReposiotyCreator(playerController);

            Assert.AreEqual(playerController.GetPlayers().Count, 2);
        }

        [TestCase]
        public void CheckIfLoadFunctionIsCalledOnFirstGet()
        {
            var playerController = new PlayerXmlRepository();

            Assert.NotNull(playerController.GetPlayers());
        }

        [TestCase]
        public void CrateXmlRepositoryDefault()
        {
            var playerController = new PlayerXmlRepository();

            Assert.NotNull(playerController.GetPlayers());
            Assert.IsEmpty(playerController.GetPlayers().ToList());
        }

        [TestCase]
        public void DefaultPathFileIsSet()
        {
            var playerController = new PlayerXmlRepository();

            Assert.NotNull(playerController.PathToFile);
            Assert.IsNotEmpty(playerController.PathToFile);
        }

        [TestCase]
        public void IdTestAfterAddAndRemove()
        {
            var playerController = new PlayerXmlRepository();
            this.AddDefaultPlayerToReposiotyCreator(playerController);
            var player = playerController.GetPlayers().First();

            playerController.Remove(player.Id);
            this.AddDefaultPlayerToReposiotyCreator(playerController);

            player = playerController.GetPlayers().First();
            Assert.AreEqual(player.Id, 0);

            Assert.AreEqual(playerController.GetPlayers().Count, 1);
        }

        [TestCase]
        public void LoadEmptyFileTest()
        {
            var playerController = new PlayerXmlRepository();
            var pathToFile = playerController.PathToFile;

            this.RemovePlayerFile(pathToFile);
            using (File.Create(pathToFile))
            {
                ;
            }

            Assert.That(() => playerController.GetPlayers(), Throws.TypeOf<InvalidOperationException>());
            this.RemovePlayerFile(pathToFile);
        }

        [TestCase]
        public void LoadFromDefaultPathTest()
        {
            var fileName = this.GetDefaultPathName();
            this.CreatePlayerFile(fileName);
            var playerController = new PlayerXmlRepository(fileName);

            Assert.AreEqual(playerController.GetPlayers().Count, 1);
            this.RemovePlayerFile(fileName);
        }

        [TestCase]
        public void LoadFromValidPathTest()
        {
            const string fileName = "test.xml";
            this.CreatePlayerFile(fileName);
            var playerController = new PlayerXmlRepository(fileName);

            Assert.AreEqual(playerController.GetPlayers().Count, 1);
            this.RemovePlayerFile(fileName);
        }

        [TestCase]
        public void RemovePlayerByIdTest()
        {
            var playerController = new PlayerXmlRepository();
            this.AddDefaultPlayerToReposiotyCreator(playerController);

            var player = playerController.GetPlayers().First();

            playerController.Remove(player.Id);
            Assert.AreEqual(playerController.GetPlayers().Count, 0);
        }

        public void SaveFromDefaultPathTest()
        {
            var player = new Player { Name = "testName", LastName = "testLastName" };
            var playerController = new PlayerXmlRepository();
            playerController.Add(player);

            playerController.SavePlayers();

            Assert.IsTrue(File.Exists(playerController.PathToFile));

            this.RemovePlayerFile(playerController.PathToFile);
        }

        [TestCase]
        public void SaveFromValidPathTest()
        {
            var player = new Player { Name = "testName", LastName = "testLastName" };
            const string fileName = "test.xml";
            var playerController = new PlayerXmlRepository(fileName);
            playerController.Add(player);

            playerController.SavePlayers();

            Assert.IsTrue(File.Exists(fileName));

            this.RemovePlayerFile(fileName);
        }

        [TestCase]
        public void SaveWithDifferentThanGiventInConstructorPathTest()
        {
            var player = new Player { Name = "testName", LastName = "testLastName" };
            const string fileName = "test.xml";
            var playerController = new PlayerXmlRepository(fileName);
            playerController.Add(player);

            const string fileName2 = "test.xml";
            playerController.SavePlayers(fileName2);

            Assert.IsTrue(File.Exists(fileName2));

            this.RemovePlayerFile(fileName2);
            this.RemovePlayerFile(fileName);
        }

        [TestCase]
        public void TestIfNotExistingFileIsCreatedOnLoading()
        {
            var playerController = new PlayerXmlRepository();
            var pathToFile = playerController.PathToFile;

            this.RemovePlayerFile(pathToFile);

            Assert.IsFalse(File.Exists(pathToFile));
            playerController.GetPlayers();
            Assert.IsTrue(File.Exists(pathToFile));

            this.RemovePlayerFile(pathToFile);
        }

        #endregion

        #region Methods

        private void AddDefaultPlayerToReposiotyCreator(PlayerXmlRepository playerController)
        {
            var player = new Player { Name = "testName", LastName = "testLastName" };
            playerController.Add(player);
        }

        private void CreatePlayerFile(string pathToFile)
        {
            new XDocument(
                new XElement(
                    "root", 
                    new XElement(
                        "Players", 
                        new XElement(
                            "Player", 
                            new XElement("Name", "Test Name"), 
                            new XElement("LastName", "Last Name Test"), 
                            new XElement("id", "100"))))).Save(pathToFile);
        }

        private string GetDefaultPathName()
        {
            var playerController = new PlayerXmlRepository();
            this.RemovePlayerFile(playerController.PathToFile);
            return playerController.PathToFile;
        }

        private void RemovePlayerFile(string pathToFile)
        {
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);
            }
        }

        #endregion
    }
}