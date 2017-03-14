namespace GameBaseModelView.Model
{
    #region

    using System.ComponentModel;

    #endregion

    public class WindowModelView : INotifyPropertyChanged
    {
        #region Constants

        private const string ConstProgramTitle = "CGC";

        #endregion

        #region Fields

        private string title = ConstProgramTitle;

        public bool IsDataDirty { get; private set; }

        #endregion

        #region Constructors and Destructors

        public WindowModelView()
        {
            this.ResetModifited();
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged = null;

        #endregion

        #region Public Properties

        public string MyTitle
        {
            get { return this.title; }

            set
            {
                this.title = value;
                this.OnPropertyChanged("MyTitle");
            }
        }

        #endregion

        #region Public Methods and Operators

        public void ResetModifited()
        {
            this.MyTitle = ConstProgramTitle;
            this.IsDataDirty = false;
        }

        public void WasModified()
        {
            this.MyTitle = ConstProgramTitle + "*";
            this.IsDataDirty = true;
        }

        #endregion

        #region Methods

        protected virtual void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}