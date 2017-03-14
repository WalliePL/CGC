namespace CGC
{
    #region

    using System;
    using System.Windows;

    using CGC.GameTypes;

    using GameBaseModel.Api;
    using GameBaseModel.Player;

    #endregion

    public static class GameWindowFactory
    {
        #region Public Methods and Operators

        public static Window CreateInstance(GameType gameType, Players playingPlayers)
        {
            switch (gameType)
            {
                case GameType.AllToAll:
                    return new AllToAllView(playingPlayers);
                default:
                    throw new Exception("undefinded game type");
            }
        }

        #endregion
    }
}