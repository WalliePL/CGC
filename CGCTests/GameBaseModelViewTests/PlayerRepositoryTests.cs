namespace CGCTests.GameBaseModelViewTests
{
    #region

    using GameBaseModel.Api;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class PlayerRepositoryTests
    {
        #region Public Methods and Operators

        [TestCase]
        public void GetXmlPlayerRerpository()
        {
            var playerXmlRepository = PlayerRepository.XmlReposiotry;

            Assert.NotNull(playerXmlRepository);
            Assert.AreEqual(playerXmlRepository, PlayerRepository.XmlReposiotry);
        }

        #endregion
    }
}