namespace GameBaseModel.Player
{
    #region

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    public class Players : IEnumerable<Player>, IEquatable<Players>
    {
        #region Fields

        private List<Player> players;

        #endregion

        #region Constructors and Destructors

        public Players(List<Player> playersCollection)
        {
            this.players = playersCollection;
        }

        public Players()
        {
            this.players = new List<Player>();
        }

        #endregion

        #region Public Properties

        public int Count
        {
            get
            {
                return this.players.Count();
            }
        }

        public List<Player> PlayersList
        {
            get
            {
                return this.players;
            }

            set
            {
                this.players = value.ToList();
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Add(Player player)
        {
            if (player == null)
            {
                throw new NullReferenceException("Cannot add player to list. Player cannot be null.");
            }

            this.players.Add(player);
        }

        public bool Equals(Players other)
        {
            return this.Equals(other as Object);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as Players;
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            return other.players.All(this.players.Contains);
        }

        public List<Player> GetClone()
        {
            var clones = new List<Player>();
            this.players.ForEach(p => clones.Add(new Player(p)));
            return clones;
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return this.players.GetEnumerator();
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            foreach (var player in this.players)
            {
                hashCode ^= player.GetHashCode();
            }

            return hashCode;
        }

        public void Remove(Player player)
        {
            if (!this.players.Contains(player))
            {
                throw new IndexOutOfRangeException(
                    string.Format("Player {0} {1} {2} is not avaiable on list.", player.Id, player.Name, player.LastName));
            }

            if (player == null)
            {
                throw new NullReferenceException("Cannot remove null player from list.");
            }

            this.players.Remove(player);
        }

        public override string ToString()
        {
            var results = new StringBuilder();
            foreach (var player in this.players)
            {
                results.AppendLine(player.Name);
            }

            return results.ToString();
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.players.GetEnumerator();
        }

        #endregion

        #region Methods

        private List<Player> ToList()
        {
            return this.players.ToList();
        }

        #endregion

        public void Shuffle()
        {
            this.players = this.players.OrderBy(a => Guid.NewGuid()).ToList();
        }
    }
}