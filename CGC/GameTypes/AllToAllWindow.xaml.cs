namespace CGC.GameTypes
{
    #region

    using System.Windows;

    using GameBaseModel.Player;

    using GameBaseModelView.Model.GameTypes;

    #endregion

    public partial class AllToAllView : Window
    {
        #region Constructors and Destructors

        public AllToAllView(Players playingPlayers)
        {
            this.InitializeComponent();

            this.PlayerFullListModelView = new AllToAllModelView(playingPlayers);

            this.DataContext = this.PlayerFullListModelView;
        }

        #endregion

        #region Public Properties

        public AllToAllModelView PlayerFullListModelView { get; private set; }

        #endregion
    }
}