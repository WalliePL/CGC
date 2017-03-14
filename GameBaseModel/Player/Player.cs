namespace GameBaseModel.Player
{
    #region

    using System.Xml.Serialization;

    using GameBaseModel.Api;
    using GameBaseModel.Api.Interfaces;

    #endregion

    public class Player : IId
    {
        #region Fields

        private bool isChoosen;

        private string lastName;

        private string name;

        private int teamId;

        #endregion

        #region Constructors and Destructors
       
        private Player(bool isChoosen, string lastName, string name, int id)
        {
            this.isChoosen = isChoosen;
            this.lastName = lastName;
            this.name = name;
            this.Id = id;
        }

        public Player() : this(false, "N/A", "N/A", -1)
        {

        }

        public Player(Player player) : this (player.isChoosen, player.lastName, player.name, player.Id)
        {
        }

        #endregion

        #region Public Events

        public event DataChangeDelegate DataChanged = null;

        #endregion

        #region Public Properties

        [XmlElement]
        public int Id { get; set; }

        public bool IsChoosen
        {
            get
            {
                return this.isChoosen;
            }

            set
            {
                this.OnDataChanged();
                this.isChoosen = value;
            }
        }

        [XmlElement]
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                this.OnDataChanged();
                this.lastName = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.OnDataChanged();
                this.name = value;
            }
        }

        #endregion

        #region Properties

        [XmlElement]
        private int TeamId
        {
            get
            {
                return this.teamId;
            }

            set
            {
                this.OnDataChanged();
                this.teamId = value;
            }
        }

        #endregion

        #region Methods

        protected virtual void OnDataChanged()
        {
            var handler = this.DataChanged;
            if (handler != null)
            {
                handler();
            }
        }

        #endregion
    }
}