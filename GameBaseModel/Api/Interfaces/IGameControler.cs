namespace GameBaseModel.Api.Interfaces
{
    #region

    using GameBaseModel.Player;

    #endregion

    public interface IGameControler
    {
        #region Public Methods and Operators

        Oponents CurrentGameOponents();

        void EndGame(Oponents oponents, Players winners);

        #endregion
    }
}