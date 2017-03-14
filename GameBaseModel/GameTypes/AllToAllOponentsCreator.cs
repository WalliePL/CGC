namespace GameBaseModel.GameTypes
{
    #region

    using System.Collections.Generic;
    using System.Linq;

    using GameBaseModel.Api.Interfaces;
    using GameBaseModel.CgcExceptions;
    using GameBaseModel.Player;

    #endregion

    public class AllToAllOponentsCreator : IGameCreaator
    {
        #region Public Methods and Operators

        public List<Oponents> CrateOponentsForGames(Players playersInGame)
        {
            var players = playersInGame.PlayersList.ToList();
            if (!players.Any())
            {
                throw new CgcCannotCreateOponentsExceptions("Players collection in game cannot be empty");
            }

            if (players.Count == 1)
            {
                throw new CgcCannotCreateOponentsExceptions("Players collection in game cannot have only one player ");
            }

            var oponents = new List<Oponents>();
            while (players.Any())
            {
                var currentPlayer = players.First();
                players.Remove(currentPlayer);
                oponents.AddRange(players.Select(player => CreateOponent(currentPlayer, player)));
            }

            return oponents;
        }

        public void GameEnded(Oponents oponents, Player winner)
        {
            if (winner == null)
            {
            }
        }

        #endregion

        #region Methods

        private static Oponents CreateOponent(Player currentPlayer, Player player)
        {
            return new Oponents { TeamA = new Players { currentPlayer }, TeamB = new Players { player } };
        }

        #endregion
    }
}