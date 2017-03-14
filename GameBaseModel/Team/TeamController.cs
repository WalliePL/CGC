namespace GameBaseModel.Team
{
    #region

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using GameBaseModel.Api;
    using GameBaseModel.Api.Interfaces;

    #endregion

    [Obsolete("This class is obsolete.")]
    public class TeamController
    {
        #region Constructors and Destructors

        public TeamController()
        {
            this.Teams = new List<Team>();
        }

        public TeamController(string loadPath)
            : this()
        {
            this.Load(loadPath);
        }

        #endregion

        #region Public Properties

        public List<Team> Teams { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Add(Team newPlayer)
        {
            var idControler = new IdSetter(this.Teams.OfType<IId>().ToList<IId>());
            int newId = idControler.getId();
            newPlayer.Id = newId;
            Console.WriteLine("NEW ID IS : {0}", newId);

            this.Teams.Add(newPlayer);
        }

        public void Load(string pathToFile)
        {
            var serializer = new XmlSerializer(typeof(List<Team>));
            using (var stream = new FileStream(pathToFile, FileMode.Open))
            {
                this.Teams = serializer.Deserialize(stream) as List<Team>;
            }
        }

        public void Save(string pathToFile)
        {
            var serializer = new XmlSerializer(typeof(List<Team>));
            using (var writer = new StreamWriter(pathToFile))
            {
                serializer.Serialize(writer, this.Teams);
            }
        }

        #endregion
    }
}