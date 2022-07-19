using Caliburn.Micro;

namespace Connect4Game.ViewModels
{
    class CustomAlertViewModel : Conductor<object>
    {

        private string _message;

        /// <summary>
        /// NotifyOfPropertyChange tells the view that it should display the new value every time the Message property is changed.
        /// </summary>
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// initialising the message variables to the parameter
        /// </summary>
        /// <param name="message"></param>
        public CustomAlertViewModel(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Tries to close this instance when clicked. Also provides an opportunity to pass a message
        /// If the screen is being controlled by a conductor it asks the conductor to initiate the shutdown
        /// CanClose logic will be invoked and if allowed, OnDeactivate will be called with a value of True.
        /// </summary>
        public void CloseCommand()
        {
            TryCloseAsync();
        }

    }
}
