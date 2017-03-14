namespace GameBaseModelView.Model.GameTypes
{
    #region

    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;

    using GameBaseModel.GameTypes;
    using GameBaseModel.Player;
    using GameBaseModel.Utils;
    using MessageBox = System.Windows.Forms.MessageBox;

    #endregion

    public class AllToAllModelView : CgcModelView
    {
        #region Fields

        private readonly AllToAllGameController GameControler;

        private bool shouldShowImage;

        #endregion

        #region Constructors and Destructors

        public AllToAllModelView(Players playersForGame)
        {
            this.shouldShowImage = false;
            playersForGame.Shuffle();
            this.GameControler = new AllToAllGameController(playersForGame, new AllToAllOponentsCreator());
        }

        #endregion

        #region Public Properties

        public Visibility AllGamesVisibility
        {
            get
            {
                return this.shouldShowImage ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Oponents CurrentPlaying
        {
            get
            {
                return this.GameControler.CurrentGameOponents();
            }
        }

        public bool GameAvaiable
        {
            get
            {
                return this.GameControler.IsNextGameAvailable;
            }
        }

        public ObservableCollection<GameStatisticViewModel> Games
        {
            get
            {
                return this.CreateObservableGamesCollection(this.GameControler);
            }
        }

        public ObservableCollection<PlayerGameStatistic> Players
        {
            get
            {
                return new ObservableCollection<PlayerGameStatistic>(this.GameControler.Players);
            }
        }

        public ICommand ShowAllGamesCommand
        {
            get
            {
                return new RelayCommand(this.ShowAllGames);
            }
        }

        public ICommand TeamAWinGamesCommand
        {
            get
            {
                return new RelayCommand(this.GameEndWinnerATeam);
            }
        }

        public ICommand TeamBWinGamesCommand
        {
            get
            {
                return new RelayCommand(this.GameEndWinnerBTeam);
            }
        }

        public ICommand TieCommand
        {
            get
            {
                return new RelayCommand(this.GameEndTie);
            }
        }

        #endregion

        #region Methods

        private ObservableCollection<GameStatisticViewModel> CreateObservableGamesCollection(AllToAllGameController gameControler)
        {
            var games = gameControler.GamesInSortedQueue;

            var observableGames = new ObservableCollection<GameStatisticViewModel>();
            foreach (var game in games)
            {
                observableGames.Add(new GameStatisticViewModel(game));
            }

            return observableGames;
        }

        private void GameEnd(Players winner)
        {
            this.GameControler.EndGame(winner);
            this.NotifyAboutGameEnding();
        }

        private void GameEndTie()
        {
            this.GameControler.EndGameAsTie();
            this.NotifyAboutGameEnding();
        }

        private void GameEndWinnerATeam()
        {
            this.GameEnd(this.GameControler.CurrentGameOponents().TeamA);
        }

        private void GameEndWinnerBTeam()
        {
            this.GameEnd(this.GameControler.CurrentGameOponents().TeamB);
        }

        private void NotifyAboutGameEnding()
        {
            this.OnPropertyChanged("Players");
            this.OnPropertyChanged("Games");
            this.UpdateCurrentGameView();
        }

        private void ShowAllGames()
        {
            this.shouldShowImage = !this.shouldShowImage;
            Console.WriteLine(this.shouldShowImage);
            this.OnPropertyChanged("AllGamesVisibility");
        }

        private void UpdateCurrentGameView()
        {
            this.OnPropertyChanged("GameAvaiable");

            if (this.GameControler.IsNextGameAvailable)
            {
                this.OnPropertyChanged("CurrentPlaying");
            }
            else
            {
                MessageBox.Show(" Koniec Gry! ");
            }
        }

        #endregion
    }
}