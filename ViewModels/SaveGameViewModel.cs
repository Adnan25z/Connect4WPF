using Caliburn.Micro;

using Connect4Game.Models;
using Connect4Game.Utils;

using System.Collections.Generic;
using System.Windows.Input;

namespace Connect4Game.ViewModels
{
    class SaveGameViewModel : Screen
    {
        private List<GameModel> _games;
        private GameModel _game;

        /// <summary>
        /// property to get and set name of the game
        /// </summary>
        public string Name
        {
            get { return _game.Name; }
            set { _game.Name = value;
            }
        }

        /// <summary>
        /// stores every row and column of the game map, checks the depth of the game if it is a player vs ai game and stores the depth
        /// Also stores the player names, colors and the gamemap 
        /// It also stores all the game data in a list which is then deserialized to show the data stored which was previously in json format
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="gameMap"></param>
        public SaveGameViewModel(Player player1, Player player2, int[,] gameMap)
        {
            var list = new List<int>();
            foreach (var item in gameMap)
            {
                list.Add(item);
            }
            _game = new GameModel() { Player1 = player1.Name, Player1Color = player1.Color, Player2 = player2.Name, Player2Color = player2.Color, GameMap = list };
            if (player2.GetType() == typeof(AIPlayer))
            {
                _game.Depth = (int)((AIPlayer)player2).Lavel;
            }
            _games = DataHelper.GetValue<List<GameModel>>("games")?? new List<GameModel>();
        }

        /// <summary>
        /// checks if string entered is empty or list _games is null and whether a game exists by that name already
        /// </summary>
        /// <param name="name"></param>
        /// <returns>false if any of the above condition is matched else returns true</returns>
        public bool CanSave(string name)
        {
            if (string.IsNullOrEmpty(name) || _games is null || _games.Exists(x=>x.Name == _game.Name))
                return false;
            return true;
        }

        /// <summary>
        /// adds the game to the list which is passed on to the datahelper class to be stored in json format
        /// we then cast the parent item to the interface so that we can make the correct call on the parent.
        /// And then use the conductor to access the back home command
        /// </summary>
        /// <param name="name"></param>
        public void Save(string name)
        {
            _games.Add(_game);
            DataHelper.SetValue("games", _games);
            var conductor = this.Parent as ShellViewModel;
            conductor.BackHomeCommand();
        }
    }
}
