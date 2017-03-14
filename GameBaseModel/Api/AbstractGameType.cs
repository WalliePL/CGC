namespace GameBaseModel.Api
{
    #region

    using System;

    #endregion

    public abstract class AbstractGameType
    {
        #region Fields

        protected string Name;

        #endregion

        #region Public Properties

        public GameType GameType { get; protected set; }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw new Exception("Game type name cannot be null");
            }

            return this.Name;
        }

        #endregion
    }
}