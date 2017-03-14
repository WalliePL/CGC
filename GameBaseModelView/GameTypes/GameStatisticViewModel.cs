namespace GameBaseModelView.Model.GameTypes
{
    #region

    using System.Text;

    using GameBaseModel.Player;

    #endregion

    public class GameStatisticViewModel
    {
        #region Fields

        private readonly GameStatistic gameStatistic;

        #endregion

        #region Constructors and Destructors

        public GameStatisticViewModel(GameStatistic gameStatistic)
        {
            this.gameStatistic = gameStatistic;
        }

        #endregion

        #region Public Properties

        public Oponents Oponents
        {
            get
            {
                return this.gameStatistic.Oponents;
            }
        }

        public string WinnerText
        {
            get
            {
                return this.generateWinnerText();
            }
        }

        #endregion

        #region Methods

        private string generateWinnerText()
        {
            var winners = this.gameStatistic.Winners;
            if (winners == null)
            {
                return string.Empty;
            }

            if (this.gameStatistic.Oponents.TeamA == winners || this.gameStatistic.Oponents.TeamB == winners)
            {
                var stringBuilder = new StringBuilder();
                foreach (var winner in winners)
                {
                    stringBuilder.AppendLine(winner.Name + " " + winner.LastName);
                }

                return stringBuilder.ToString();
            }

            return "Remis";
        }

        #endregion
    }
}