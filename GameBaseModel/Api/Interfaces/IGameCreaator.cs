namespace GameBaseModel.Api.Interfaces
{
    #region

    using System.Collections.Generic;

    using GameBaseModel.Player;

    #endregion

    public interface IGameCreaator
    {
        #region Public Methods and Operators

        List<Oponents> CrateOponentsForGames(Players playersInGame);

        #endregion
    }
}