namespace GameBaseModel.CgcExceptions
{
    #region

    using System;

    #endregion

    internal class PlayerXmlRepositoryException : Exception
    {
        #region Constructors and Destructors

        public PlayerXmlRepositoryException()
        {
        }

        public PlayerXmlRepositoryException(string message)
            : base(message)
        {
        }

        public PlayerXmlRepositoryException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}