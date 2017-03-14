namespace GameBaseModel.Api
{
    #region

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using GameBaseModel.Api.Interfaces;
    using GameBaseModel.CgcExceptions;
    using GameBaseModel.Player;

    #endregion

    public class PlayerXmlRepository : IPlayerRepository
    {
        #region Constructors and Destructors

        public PlayerXmlRepository()
        {
            const string DefaultFilePath = "players.cgc";
            this.PathToFile = DefaultFilePath;
        }

        public PlayerXmlRepository(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new NullReferenceException("File path to save/load xml data cannot be empty or null.");
            }

            this.PathToFile = filePath; // TODO: change this, path shuld be passed by parametr
        }

        #endregion

        #region Public Properties

        public string PathToFile { get; set; }

        public List<Player> Players
        {
            get
            {
                return this._players.PlayersList;
            }

            set
            {
                this._players.PlayersList = value;
            }
        }

        #endregion

        #region Properties

        private Players _players { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Add(Player player)
        {
            if (player == null)
            {
                throw new NullReferenceException("Added Player object cannot be null value.");
            }

            var idControler = new IdSetter(this.GetPlayers().ToList().Select(c => (IId)c).ToList());
            var newId = idControler.getId();
            player.Id = newId;
            Console.WriteLine("NEW ID IS : {0}", newId);

            this._players.Add(player);
        }

        public Players GetPlayers()
        {
            return this._players ?? (this._players = this.LoadFromFile());
        }

        public void LoadPlayers(string pathToLoad)
        {
            this.PathToFile = pathToLoad;
            this._players = null;
            this.GetPlayers();
        }

        public void Remove(Player player)
        {
            this._players.Remove(player);
        }

        public void Remove(int playerId)
        {
            try
            {
                var player = this.GetPlayerById(playerId);
                this.Remove(player);
            }
            catch (PlayerXmlRepositoryException e)
            {
                Console.WriteLine("Cannot remove id: {0}", e);
            }
        }

        public void SavePlayers()
        {
            var root = new XmlRoot { Players = this._players };
            var serializer = new XmlSerializer(typeof(XmlRoot), new XmlRootAttribute("root"));
            using (var writer = new StreamWriter(this.PathToFile))
            {
                serializer.Serialize(writer, root);
            }
        }

        public void SavePlayers(string filePath)
        {
            this.PathToFile = filePath;
            this.SavePlayers();
        }

        #endregion

        #region Methods

        private Player GetPlayerById(int id)
        {
            var player = this._players.Where(x => x.Id == id);
            if (player.Count() > 1)
            {
                throw new PlayerXmlRepositoryException(string.Format("There are more than one Player with id {0} ", id));
            }

            if (player.Count() != 1)
            {
                throw new PlayerXmlRepositoryException(string.Format("There are no Player with id {0} ", id));
            }

            return player.First();
        }

        private Players LoadFromFile()
        {
            this.ValidateExistingOfFile();

            var serializer = new XmlSerializer(typeof(XmlRoot), new XmlRootAttribute("root"));

            using (var stream = new FileStream(this.PathToFile, FileMode.Open))
            {
                var playersFromFile = new Players();
                try
                {
                    var root = serializer.Deserialize(stream) as XmlRoot;
                    playersFromFile = root.Players;
                }
                catch (InvalidOperationException)
                {
                    throw new CgcExceptionCorruptedPlayerFile();
                    throw;
                }

                return playersFromFile;
            }
        }

        private void ValidateExistingOfFile()
        {
            if (!File.Exists(this.PathToFile))
            {
                this._players = new Players();
                this.SavePlayers();
            }
        }

        #endregion

        public class XmlRoot
        {
            #region Public Properties

            public Players Players { get; set; }

            #endregion
        }
    }
}