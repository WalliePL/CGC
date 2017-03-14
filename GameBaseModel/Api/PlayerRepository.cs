namespace GameBaseModel.Api
{
    public class PlayerRepository
    {
        #region Static Fields

        private static PlayerXmlRepository playerXmlRepository;

        #endregion

        #region Public Properties

        public static PlayerXmlRepository XmlReposiotry
        {
            get
            {
                if (playerXmlRepository == null)
                {
                    playerXmlRepository = new PlayerXmlRepository();
                }

                return playerXmlRepository;
            }
        }

        #endregion
    }
}