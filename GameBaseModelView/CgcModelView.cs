namespace GameBaseModelView.Model
{
    #region

    using System.ComponentModel;

    using GameBaseModel.Api;

    #endregion

    public abstract class CgcModelView : INotifyPropertyChanged
    {
        #region Public Events

        public event DataChangeDelegate DataChanged = null;

        public event PropertyChangedEventHandler PropertyChanged = null;

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