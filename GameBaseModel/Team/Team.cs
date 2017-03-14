namespace GameBaseModel.Team
{
    #region

    using System.Collections.Generic;

    using GameBaseModel.Api.Interfaces;
    using GameBaseModel.Player;

    #endregion

    public class Team : IId
    {
        #region Constructors and Destructors

        public Team()
        {
            this.Name = "N/A";
            this.Id = -1;
            this.Players = new List<Player>();
        }

        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Player> Players { get; set; }

        #endregion
    }
}