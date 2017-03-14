namespace GameBaseModelView.Model.GameTypes
{
    #region

    using System;
    using System.Collections.Generic;

    using GameBaseModel.Api;

    #endregion

    public static class GameTypesFactory
    {
        #region Public Methods and Operators

        public static List<AbstractGameType> GameTypes()
        {
            var names = new List<AbstractGameType> { new AllToAllGameType() };

            return names;
        }

        public static string Name(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.AllToAll:
                    return "Każdy z każdym";
                default:
                    throw new ArgumentOutOfRangeException("unexpected gameType");
            }
        }

        #endregion
    }
}