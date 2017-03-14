namespace GameBaseModel.CgcExceptions
{
    #region

    using System;

    #endregion

    public class CgcCannotCreateOponentsExceptions : Exception
    {
        #region Constructors and Destructors

        public CgcCannotCreateOponentsExceptions()
        {
        }

        public CgcCannotCreateOponentsExceptions(string message)
            : base(message)
        {
        }

        public CgcCannotCreateOponentsExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}