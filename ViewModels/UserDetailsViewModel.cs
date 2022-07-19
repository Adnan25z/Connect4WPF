using Caliburn.Micro;
using Connect4Game.Models;
using System;
using System.Windows;

namespace Connect4Game.ViewModels
{
    public class UserDetailsViewModel : Screen
    {
        private string _player1;
        private string _player2;

        /// <summary>
        /// This is done to involve encapsulation in our code
        /// we declare the variables as private
        /// and provide public get and set methods through properties, to access and update the value of the private variable _player1
        /// </summary>
        public string Player1
        {
            get => _player1;
            set
            {
                _player1 = value;
            }
        }
        
        /// <summary>
        /// This is done to involve encapsulation in our code
        /// we declare the variables as private
        /// and provide public get and set methods through properties, to access and update the value of the private variable _player2
        /// </summary>
        public string Player2
        {
            get => _player2;
            set
            {
                _player2 = value;
            }
        }

        /// <summary>
        /// empty constructor needed to create a new instance
        /// </summary>
        public UserDetailsViewModel()
        {
        }

        /// <summary>
        /// checks if both the player names are entered and also makes sure that both the players do not have the same name.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns>
        /// returns false if player 1 or player 2 name is empty or both have the same name
        /// else it returns true
        /// </returns>
        public bool CanPlayGameCommand(string player1, string player2)
        {
            if (string.IsNullOrEmpty(player1) || string.IsNullOrEmpty(player2) || player1 == player2)
                return false;

            return true;
        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the chooseColorViewModel by passing in player name and difficulty level
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void PlayGameCommand(string player1, string player2)
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync( new ChooseColorViewModel(new HumanPlayer(player1), new HumanPlayer(player2)));
        }
    }
}
