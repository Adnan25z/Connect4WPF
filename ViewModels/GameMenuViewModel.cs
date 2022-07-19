using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Input;

namespace Connect4Game.ViewModels
{
    class GameMenuViewModel : Screen
    {

        private System.Action _closeWindow;


        /// <summary>
        /// Action encapsulates a method that has no parameters and does not return a value.
        /// Using this constructor to initialize the private _closeWindow variable to the parameter closeWindow
        /// </summary>
        /// <param name="closeWindow"></param>
        public GameMenuViewModel(System.Action closeWindow)
        {
            _closeWindow = closeWindow;
        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the ChooseDifficulty view model when the player clicks on a single player game
        /// </summary>
        public async void SinglePlayer()
        {
            var conductor = this.Parent as IConductor;
            await conductor.ActivateItemAsync(new ChooseDifficultyViewModel());
        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the UserDetails view model when the player clicks on the multiplayer game option
        /// </summary>
        public async void MultiPlayer()
        {
            var conductor = this.Parent as IConductor;
            await conductor.ActivateItemAsync(new UserDetailsViewModel());
        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the LoadGame view model when the player clicks on the load game option on the home screen
        /// </summary>
        public async void LoadGame()
        {
            var conductor = this.Parent as IConductor;
            await conductor.ActivateItemAsync(new LoadGameViewModel());
        }

        /// <summary>
        /// Invoking the closewindow after action _closeWindow has been passed to the constructor
        /// </summary>
        public void QuitCommand()
        {
            _closeWindow?.Invoke();
        }
    }
}
