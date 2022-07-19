using System.ComponentModel;

namespace Connect4Game.ViewModels
{
    /// <summary>
    /// INotifyPropertyChanged is an interface member in System.ComponentModel Namespace. This 
    /// interface is used to notify the control that the property value has changed
    /// </summary>
    class BaseviewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// On inherit, this event is created automatically
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// When the PropertyChanged event is raised, this method will instantiate an object containing the name of the property that was changed
        /// so the UI control can connect to the appropriate property
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
