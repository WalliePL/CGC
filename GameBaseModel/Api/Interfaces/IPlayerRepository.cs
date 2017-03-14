namespace GameBaseModel.Api.Interfaces
{
    #region

    using GameBaseModel.Player;

    #endregion

    public interface IPlayerRepository
    {
        #region Public Methods and Operators

        void Add(Player player);

        Players GetPlayers();

        void SavePlayers();

        #endregion
    }
}