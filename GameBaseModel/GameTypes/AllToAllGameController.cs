namespace GameBaseModel.GameTypes
{
    #region

    using System.Collections.Generic;
    using System.Linq;

    using GameBaseModel.Api.Interfaces;
    using GameBaseModel.CgcExceptions;
    using GameBaseModel.Player;

    #endregion

    public class AllToAllGameController : IGameControler
    {
        #region Constants

        private const int PointsForWinGame = 1;

        #endregion

        #region Fields

        private readonly Dictionary<Oponents, bool> gameToGamePlayedCollection = new Dictionary<Oponents, bool>();

        #endregion

        #region Constructors and Destructors

        public AllToAllGameController(Players players, IGameCreaator gameCreaator)
        {
            var localPlayersCollecction = new Players(players.GetClone());
            this.GamesInSortedQueue = this.SortOponentsWithMaxGamesInRow(gameCreaator.CrateOponentsForGames(localPlayersCollecction), 2);
            this.PreparePlayersWithPointsCollection(localPlayersCollecction);
        }

        #endregion

        #region Public Properties

        public IReadOnlyList<GameStatistic> GamesInSortedQueue { get; private set; }

        public bool IsNextGameAvailable
        {
            get
            {
                return this.GamesInSortedQueue.Any(this.IsNotPlayedGame);
            }
        }

        public List<PlayerGameStatistic> Players { get; private set; }

        #endregion

        #region Public Methods and Operators

        public Oponents CurrentGameOponents()
        {
            if (!this.GamesInSortedQueue.Any(this.IsNotPlayedGame))
            {
                throw new CgcExceptionEmptyPlayersList();
            }

            return this.GamesInSortedQueue.First(this.IsNotPlayedGame).Oponents;
        }

        public void EndGame(Oponents oponents, Players winners)
        {
            if (oponents.TeamA != winners && oponents.TeamB != winners)
            {
                throw new CgcExceptionCannotEndGameWinnerWasNotOponentInChosenGame();
            }

            this.GamesInSortedQueue.First(game => this.CurrentGameOponents() == game.Oponents).Winners = winners;
            this.AddPointsToWinners(winners, PointsForWinGame, 1);
        }

        public void EndGame(Players winners)
        {
            this.EndGame(this.CurrentGameOponents(), winners);
        }

        public void EndGameAsTie()
        {
            this.AddPointsToWinners(this.CurrentGameOponents().TeamA, PointsForWinGame, 0);
            this.AddPointsToWinners(this.CurrentGameOponents().TeamB, PointsForWinGame, 0);
            this.GamesInSortedQueue.First(game => this.CurrentGameOponents() == game.Oponents).Winners = new Players();
        }

        #endregion

        #region Methods

        private void AddPointsToWinners(IEnumerable<Player> winners, int points, int winsPoints)
        {
            foreach (var winner in winners)
            {
                var winerPlayer = this.Players.Find(player => player.Player == winner);
                winerPlayer.Points += points;
                winerPlayer.Wins += winsPoints;
            }
        }

        private List<Players> GetTeamsFromComponents(IEnumerable<Oponents> opononetToSort)
        {
            var uniqeOponents = new HashSet<Players>();
            foreach (var oponent in opononetToSort)
            {
                uniqeOponents.Add(oponent.TeamA);
            }

            return uniqeOponents.ToList();
        }

        private bool IsNotPlayedGame(GameStatistic game)
        {
            return game.Winners == null;
        }

        private void PreparePlayersWithPointsCollection(IEnumerable<Player> players)
        {
            this.Players = new List<PlayerGameStatistic>();

            foreach (var player in players)
            {
                this.Players.Add(new PlayerGameStatistic { Player = player, Points = 0, Wins = 0, GamesPlayedTotal = 0 });
            }
        }

        private List<GameStatistic> SortOponentsWithMaxGamesInRow(List<Oponents> opononetToSort, int howManyGamesForOneTeam)
        {
            var sortedOponents = new List<GameStatistic>();
            var teamsInGames = this.GetTeamsFromComponents(opononetToSort);

            while (opononetToSort.Any())
            {
                foreach (
                    var gamesWithOponent in
                        teamsInGames.Select(currentOponent => opononetToSort.Where(oponent => oponent.TeamA.Equals(currentOponent)).ToList()))
                {
                    for (var currentGameIndex = 0; currentGameIndex < howManyGamesForOneTeam; currentGameIndex++)
                    {
                        if (gamesWithOponent.Count() > currentGameIndex)
                        {
                            sortedOponents.Add(new GameStatistic { Oponents = gamesWithOponent[currentGameIndex], Winners = null });
                            opononetToSort.Remove(gamesWithOponent[currentGameIndex]);
                        }
                    }
                }
            }

            return sortedOponents;
        }

        #endregion
    }
}