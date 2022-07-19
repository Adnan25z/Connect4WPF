using Caliburn.Micro;

using Connect4Game.Models;
using Connect4Game.Utils;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Connect4Game.ViewModels
{
    class LoadGameViewModel : Screen
    {
        private ObservableCollection<GameModel> _games = new();

        /// <summary>
        /// property to get and set the value of _games
        /// </summary>
        public ObservableCollection<GameModel> Games
        {
            get { return _games; }
            set { _games = value; }
        }

        /// <summary>
        /// gets the data stored in json files and adds it to the games list
        /// </summary>
        public LoadGameViewModel()
        {
            var savedGames = DataHelper.GetValue<List<GameModel>>("games")??new();
            foreach (var item in savedGames)
            {
                Games.Add(item);
            }
        }


        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the GamePlayViewModel by passing in value of the variable selectedItem
        /// </summary>
        public void ItemClicked(object obj)
        {
            var selectedItem = (GameModel)((Button)obj).DataContext;
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new GamePlayViewModel(selectedItem));
        }

        /// <summary>
        /// Clear the data stored in json file also clear the whole dynamic list where data was added
        /// </summary>
        public void Clear()
        {
            DataHelper.Clear();
            Games.Clear();
        }
    }
}
