namespace GameBaseModel.Player
{
    public class PlayerGameStatistic
    {
        #region Public Properties

        public int GamesPlayedTotal { get; set; }

        public Player Player { get; set; }

        public int Points { get; set; }

        public int Wins { get; set; }

        #endregion
    }
}