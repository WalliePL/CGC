namespace CGC
{
    #region

    using System.Windows.Controls;

    using GameBaseModelView.Model;
    using GameBaseModelView.Model.Player;

    #endregion

    /// <summary>
    ///     Interaction logic for PlayersPage.xaml
    /// </summary>
    public partial class PlayersPage : Page
    {
        #region Constructors and Destructors

        public PlayersPage(WindowModelView mainWindowModelView)
        {
            this.InitializeComponent();

            this.PlayerFullListModelView = new PlayerFullListModelView();
            this.PlayerFullListModelView.DataChanged += mainWindowModelView.WasModified;

            this.DataContext = this.PlayerFullListModelView;
        }

        #endregion

        #region Public Properties

        public PlayerFullListModelView PlayerFullListModelView { get; private set; }

        #endregion
    }
}