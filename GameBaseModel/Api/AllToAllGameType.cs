namespace GameBaseModel.Api
{
    public class AllToAllGameType : AbstractGameType
    {
        #region Constructors and Destructors

        public AllToAllGameType()
        {
            this.Name = "Każdy z każdym";
            this.GameType = GameType.AllToAll;
        }

        #endregion
    }
}