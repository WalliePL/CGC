namespace CGC
{
    #region

    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using GameBaseModel.Api;
    using GameBaseModel.Player;
    using GameBaseModelView.Model;
    using Microsoft.Win32;

    #endregion

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        public readonly WindowModelView MainWindowModelView;

        private readonly PlayersPage playerPage;

        #endregion

        #region Constructors and Destructors

        public MainWindow()
        {
            this.InitializeComponent();
            this.MainWindowModelView = new WindowModelView();
            this.DataContext = this.MainWindowModelView;

            this.playerPage = new PlayersPage(this.MainWindowModelView);
            this.playerPage.PlayerFullListModelView.NewGameRequest += this.NewGameWindow;
            this.MainFrame.Content = this.playerPage;
        }

        #endregion

        #region Public Methods and Operators

        public void NewGameWindow(GameType gameType, Players playingPlayers)
        {
            var window = GameWindowFactory.CreateInstance(gameType, playingPlayers);
            window.Show();
        }

        private void ClosingWindowCommand(object sender, CancelEventArgs e)
        {
            // If data is dirty, notify user and ask for a response
            if (!this.MainWindowModelView.IsDataDirty)
            {
                return;
            }

            const string Msg = "Data is dirty. Close without saving?";
            var result =
                MessageBox.Show(
                    Msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.playerPage.PlayerFullListModelView.SavePlayers();
            }
        }

        #endregion

        #region Methods

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Card Game Project (*.cgc)|*.cgc"};
            if (openFileDialog.ShowDialog() == true)
            {
                this.playerPage.PlayerFullListModelView.LoadPlayers(openFileDialog.FileName);
                this.MainWindowModelView.ResetModifited();
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog {Filter = "Card Game Project (*.cgc)|*.cgc"};
            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }

            this.playerPage.PlayerFullListModelView.LoadPlayers(saveFileDialog.FileName);
            this.MainWindowModelView.ResetModifited();
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = sender as Button;
            (sender as Button).ContextMenu.Placement = PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.playerPage.PlayerFullListModelView.SavePlayers();
            this.MainWindowModelView.ResetModifited();
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NewButton_Click(sender, e);
        }

        #endregion
    }
}