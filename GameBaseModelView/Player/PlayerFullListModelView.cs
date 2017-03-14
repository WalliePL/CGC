namespace GameBaseModelView.Model.Player
{
    #region

    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Input;

    using GameBaseModel.Api;
    using GameBaseModel.Player;
    using GameBaseModel.Utils;

    using GameTypes;

    #endregion

    public class PlayerFullListModelView : CgcModelView
    {
        #region Fields

        private readonly PlayerXmlRepository playersRepository;

        private ObservableCollection<Player> players;

        #endregion

        #region Constructors and Destructors

        public PlayerFullListModelView()
        {
            this.playersRepository = PlayerRepository.XmlReposiotry;
            this.playersRepository.GetPlayers();
            this.SetPlayerObservableCollection();
            this.GameTypes = new ObservableCollection<AbstractGameType>(GameTypesFactory.GameTypes());
            this.SelectedGameType = this.GameTypes[0];
        }

        #endregion

        #region Public Events

        public event NewWindowAllToAllHandler NewGameRequest = null;

        #endregion

        #region Public Properties

        public ICommand AddPlayerCommand
        {
            get
            {
                return new RelayCommand(this.AddPlayer);
            }
        }

        public ObservableCollection<AbstractGameType> GameTypes { get; private set; }

        public ICommand NewGameCommand
        {
            get
            {
                return new RelayCommand(this.NewGame);
            }
        }

        public ObservableCollection<Player> Players
        {
            get
            {
                return this.players;
            }

            set
            {
                this.players = value;
                this.OnPropertyChanged("Players");
            }
        }

        public ICommand RemoveChoosenPlayersCommand
        {
            get
            {
                return new RelayCommand(this.RemoveChoosenPlayers);
            }
        }

        public ICommand SavePlayesCommand
        {
            get
            {
                return new RelayCommand(this.SavePlayers);
            }
        }

        public AbstractGameType SelectedGameType { get; set; }

        #endregion

        #region Public Methods and Operators

        public void LoadPlayers(string pathToFile)
        {
            this.Players.Clear();
            this.playersRepository.LoadPlayers(pathToFile);
            this.SetPlayerObservableCollection();
        }

        public void NewGame()
        {
            if (this.NewGameRequest != null)
            {
                if (this.SelectedGameType == null)
                {
                    throw new Exception("Game Type not selected");
                }

                var playersInGame = this.GetSelectedPlayers();
                this.NewGameRequest(this.SelectedGameType.GameType, playersInGame);
            }
        }

        public void SavePlayers(string pathToFile)
        {
            this.playersRepository.SavePlayers(pathToFile);
        }

        public void SavePlayers()
        {
            this.playersRepository.SavePlayers();
        }

        #endregion

        #region Methods

        private void AddPlayer()
        {
            var newPlayer = new Player();
            this.Players.Add(newPlayer);
        }

        private Players GetSelectedPlayers()
        {
            return new Players(this.Players.Where(player => player.IsChoosen).ToList());
        }

        private void PlayerObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Player removedPlayer in e.OldItems)
                {
                    this.playersRepository.Remove(removedPlayer);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Player newPlayer in e.NewItems)
                {
                    newPlayer.DataChanged += this.OnDataChanged;
                    this.playersRepository.Add(newPlayer);
                }
            }

            this.OnDataChanged();
        }

        private void RemoveChoosenPlayers()
        {
            var playerQuantity = this.Players.Count - 1;

            for (var playerIndex = playerQuantity; playerIndex != 0; playerIndex--)
            {
                var player = this.Players[playerIndex];
                if (player.IsChoosen)
                {
                    this.Players.RemoveAt(playerIndex);
                }
            }
        }

        private void SetPlayerObservableCollection()
        {
            this.playersRepository.Players.ForEach(player => player.DataChanged += this.OnDataChanged);
            this.Players = new ObservableCollection<Player>(this.playersRepository.Players);
            this.Players.CollectionChanged += this.PlayerObservableCollectionChanged;
        }

        #endregion
    }
}