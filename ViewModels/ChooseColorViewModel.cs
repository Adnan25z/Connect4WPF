using Caliburn.Micro;
using Connect4Game.Models;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    /// <summary>
    /// We use the screen interface in caliburn when we need any of the activation features in our classes
    /// </summary>
    class ChooseColorViewModel : Screen
    {
        private Player _player1;
        private Player _player2;
        private Player currentPlayer;
        public Brush Color { get; set; }

        /// <summary>
        /// We get a string displaying current player name
        /// </summary>
        public string ChooseColor
        {
            get => $"Choose color for {(currentPlayer.Name is "You"?(currentPlayer.Name+"rself"):currentPlayer.Name)}";
        }

        /// <summary>
        /// Setting up players,by initialising the 3 private variables to parameters
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public ChooseColorViewModel(Player player1, Player player2)
        {
            currentPlayer = _player1 = player1;
            _player2 = player2;
        }

        /// <summary>
        /// Randomly choose the the colour for the computer player and then check that it is different from the other player.
        /// I use async and await keywords because it yields a more responsive UI.
        /// and also because We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the chooseColorViewModel by passing in player name and difficulty level
        /// </summary>
        /// <param name="obj"></param>
        public async void SelectColor(object obj)
        {
            var selectedButton = (Button)obj;
            selectedButton.Visibility = System.Windows.Visibility.Collapsed;
            if (currentPlayer == _player1)
            {
                _player1.Color = selectedButton.Foreground.ToString();
                if(_player2.Name is "Computer")
                {
                    do
                    {
                        Random r = new Random();
                        switch (r.Next(0,6))
                        {
                            case 0: _player2.Color = Brushes.Red.ToString(); break;
                            case 1: _player2.Color = Brushes.Green.ToString(); break;
                            case 2: _player2.Color = Brushes.Blue.ToString(); break;
                            case 3: _player2.Color = Brushes.Pink.ToString(); break;
                            case 4: _player2.Color = Brushes.Yellow.ToString(); break;
                            case 5: _player2.Color = Brushes.Gray.ToString(); break;
                        }
                    } while (_player2.Color == _player1.Color);// Will choose a random colour for the ai making sure it doesn't clash with the users.
                    var conductor = this.Parent as IConductor;
                    await conductor.ActivateItemAsync(new GamePlayViewModel(_player1, _player2));
                }
                currentPlayer = _player2;
                NotifyOfPropertyChange("ChooseColor"); //This property is one of the most powerful pros that Caliburn.Micro gave us. This property is notifying the public attribute each change we made to the private one. Use the method in the Caliburn.Micro class in order to capture the event when a property is changed.
            }
            else
            {
                _player2.Color = selectedButton.Foreground.ToString();

                var conductor = this.Parent as IConductor;
                await conductor.ActivateItemAsync(new GamePlayViewModel(_player1, _player2));
            }
        }
    }
}
