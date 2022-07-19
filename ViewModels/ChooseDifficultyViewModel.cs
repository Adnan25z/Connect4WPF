using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    class ChooseDifficultyViewModel : Screen
    {
        private Player player;

        /// <summary>
        /// Creating a New Human Player by using the HumanPlayer class
        /// </summary>
        public ChooseDifficultyViewModel()
        {
            player = new HumanPlayer("You");
        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the chooseColorViewModel by passing in player name and difficulty level
        /// </summary>
        public void Easy()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new AIPlayer("Computer", DifficultyLevel.Easy)));
        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the chooseColorViewModel by passing in player name and difficulty level
        /// </summary>
        public void Medium()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new AIPlayer("Computer", DifficultyLevel.Normal)));
        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the chooseColorViewModel by passing in player name and difficulty level
        /// </summary>
        public void Hard()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new AIPlayer("Computer", DifficultyLevel.Hard)));
        }
    }
}
