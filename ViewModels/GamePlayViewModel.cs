using Caliburn.Micro;

using Connect4Game.Models;

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    /// <summary>
    /// Declaring a struct which contains a parameterized constructor
    /// </summary>
    #region CustomStruct
    public struct Field
    {
        public int rol;
        public int col;

        public Field(int r, int c)
        {
            rol = r;
            col = c;
        }
    }
    #endregion

    public class GamePlayViewModel : Screen
    {

        private Player _player1;
        private Player _player2;
        private StackPanel ColumnStacks;
        private Stack<GameStep> _steps;

        private const int MaxRow = 6;
        private const int MaxColumn = 7;
        private int[,] _gameMap = new int[MaxRow, MaxColumn] {
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 }
                                                   };

        /// <summary>
        /// get and set _switchPlayers as true or false
        /// used in runoperation
        /// </summary>
        private bool _switchPlayers { get; set; }

        /// <summary>
        /// gets and sets _canPlay as true
        /// </summary>
        private bool _canPlay { get; set; } = true;

        /// <summary>
        /// checks condition if steps count greater than 0 and _canPlay=true then returns true
        /// else return false
        /// </summary>
        public bool CanUndo => _steps.Count > 0 && _canPlay;

        /// <summary>
        /// readonly property to set player1 token colour
        /// </summary>
        public Brush Player1Color
        {
            get => (Brush)new BrushConverter().ConvertFromString(_player1?.Color);
        }

        /// <summary>
        /// readonly property to set player1 token colour
        /// </summary>
        public Brush Player2Color
        {
            get => (Brush)new BrushConverter().ConvertFromString(_player2?.Color);
        }

        /// <summary>
        /// readonly property to set player1 name
        /// </summary>
        public string Player1Name
        {
            get => _player1?.Name;
        }

        /// <summary>
        /// readonly property to set player2 name
        /// </summary>
        public string Player2Name
        {
            get => _player2.Name;
        }


        private string _gameState;

        /// <summary>
        /// property to set game state
        /// When the PropertyChanged event is raised, this method will instantiate an object containing the name of the property that was changed
        /// so the UI control can connect to the appropriate property
        /// </summary>
        public string GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                NotifyOfPropertyChange();
            }
        }

        private Dictionary<string, int> _gameMapIndexDictionary;

        /// <summary>
        /// checks the buttons views are attached to the view then displays them along with player colour and all the buttons
        /// </summary>
        /// <param name="view"></param>
        /// <param name="context"></param>
        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            var frameworkElement = view as FrameworkElement;

            if (frameworkElement == null)
            {
                return;
            }

            var control = frameworkElement.FindName("ColumnStacks") as StackPanel;

            if (control == null)
            {
                return;
            }
            else
            {
                ColumnStacks = control;
                for (int i = 0; i < ColumnStacks.Children.Count; i++)
                {
                    var buttons = ((StackPanel)ColumnStacks.Children[i]).Children;
                    for (int j = 0; j < buttons.Count; j++)
                    {
                        ((Button)buttons[j]).Foreground = _gameMap[j, i] switch
                        {
                            1 => Player1Color,
                            2 => Player2Color,
                            _ => ((Button)buttons[j]).Foreground,
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Assigns the variables to the parameters passed makes switch player as true 
        /// We create a dictionary to store the columns of the board
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public GamePlayViewModel(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;

            _switchPlayers = true;
            _gameMapIndexDictionary = new Dictionary<string, int>();

            _gameMapIndexDictionary.Add("col1", 0);
            _gameMapIndexDictionary.Add("col2", 1);
            _gameMapIndexDictionary.Add("col3", 2);
            _gameMapIndexDictionary.Add("col4", 3);
            _gameMapIndexDictionary.Add("col5", 4);
            _gameMapIndexDictionary.Add("col6", 5);
            _gameMapIndexDictionary.Add("col7", 6);

            GameState = Player1Name + (Player1Name is "You" ? "r Turn" : "'s Turn");
            _canPlay = true;
            _steps = new();
        }

        /// <summary>
        /// Creates the required players and sets up the gamemap
        /// </summary>
        /// <param name="gameModel"></param>
        public GamePlayViewModel(GameModel gameModel) 
            : this(
                  new HumanPlayer(gameModel.Player1) { Color = gameModel.Player1Color },
                  gameModel.Depth > 0 ? new AIPlayer(gameModel.Player2, (DifficultyLevel)gameModel.Depth) { Color = gameModel.Player2Color }
                  : new HumanPlayer(gameModel.Player1) { Color = gameModel.Player1Color })
        {
            for (int i = 0; i < gameModel.GameMap.Count; i++)
            {
                _gameMap[i / 7, i % 7] = gameModel.GameMap[i]; 
            }
        }


        /// <summary>
        /// Handles the flow of the game. Uses stack to drop tokens and then uses the method checkwin from AIPlayer to check the winning condition
        /// also handles the logic to switch players after each turn
        /// </summary>
        /// <param name="obj"></param>
        public void RunOperation(object obj)
        {
            if (_canPlay == false)
                return;
            var colIndex = -1;
            StackPanel stackContainer;
            var selectedButton = (Button)obj;
        Play:
            if (colIndex == -1)
            {
                stackContainer = (StackPanel)selectedButton.Parent;
                colIndex = _gameMapIndexDictionary[stackContainer.Name];
            }
            else
            {
                stackContainer = (StackPanel)ColumnStacks.Children[colIndex];
            }
            //var rowIndex = 0;
            //if (_gameMap[rowIndex, colIndex] == 2 || _gameMap[rowIndex, colIndex] == 5)
            //    return;

            for (int i = 0; i < MaxRow; i++)     // check for unfilled in seleceted position
            {
                if (_gameMap[MaxRow - i - 1, colIndex] == 0)
                {
                    var calButton = (Button)stackContainer.Children[MaxRow - i - 1];

                    if (_switchPlayers)
                    {
                        calButton.Foreground = (Brush)new BrushConverter().ConvertFromString(_player1.Color);
                        _gameMap[MaxRow - i - 1, colIndex] = 1;
                        var result = checkWin(1, stackContainer);
                        if (result)
                        {
                            _canPlay = false;
                            GameState = "Game Over";
                            WindowManager windowManager = new WindowManager();
                            windowManager.ShowDialogAsync(new CustomAlertViewModel($"{Player1Name} Won!"));
                        }
                    }
                    else
                    {
                        calButton.Foreground = (Brush)new BrushConverter().ConvertFromString(_player2.Color);
                        _gameMap[MaxRow - i - 1, colIndex] = 2;
                        var result = checkWin(2, stackContainer);
                        if (result)
                        {
                            _canPlay = false;
                            GameState = "Game Over";
                            WindowManager windowManager = new WindowManager();
                            windowManager.ShowDialogAsync(new CustomAlertViewModel($"{Player2Name} Won!"));
                        }
                    }

                    _steps.Push(new() { Player = _switchPlayers ? _player1 : _player2, X = i, Y = colIndex });
                    NotifyOfPropertyChange(nameof(CanUndo));
                    break;
                }
            }

            _switchPlayers = !_switchPlayers;

            if (_switchPlayers && _canPlay)
                GameState = Player1Name + (Player1Name is "You" ? "r Turn" : "'s Turn");

            else if (_switchPlayers == false && _canPlay)
            {
                if (_player2.GetType() == typeof(AIPlayer))
                {
                    colIndex = ((AIPlayer)_player2).Move(_gameMap);
                    goto Play;
                }
                GameState = $"{Player2Name}'s Turn";
            }

        }

        /// <summary>
        /// We are casting the parent item to the interface so that we can make the correct call on the parent.
        /// Activating the SaveGameViewModel by passing in players and gameMap
        /// </summary>
        public void SaveGame()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new SaveGameViewModel(_player1, _player2, _gameMap));
        }

        /// <summary>
        /// We use the Pop method on _steps since its a stack and it has a LIFO structure so to get the last step we just need to use the pop method
        /// If last step is of the computer we just recursively call the undo function 
        /// and if last step was of the player then we need to switch the players first and change the game state.
        /// finally, NotifyOfPropertyChange tells the view that it should check that the canUndo property is changed and is still valid.
        /// </summary>
        public void Undo()
        {
            var lastStep = _steps.Pop();
            var buttonsStack = ColumnStacks.Children[lastStep.Y];
            ((Button)((StackPanel)buttonsStack).Children[MaxRow - lastStep.X - 1]).Foreground = Brushes.White;
            _gameMap[MaxRow - lastStep.X - 1, lastStep.Y] = 0;
            if (lastStep.Player.Name is "Computer")
            {
                Undo();
            }
            else
            {
                _switchPlayers = lastStep.Player.Name == _player1.Name;
                GameState = (_switchPlayers ? Player1Name : Player2Name) + (_switchPlayers && Player1Name is "You" ? "r Turn" : "'s Turn");
            }
            NotifyOfPropertyChange(nameof(CanUndo));
        }

        /// <summary>
        /// Checks for win by checking against the patterns of horizontal vertical and diognal checks
        /// if any of the condition meets we increase the count and add the field to the points list 
        /// if count equals 4 found pattern turns true unless all 4 tokens are of the same player
        /// and we display an X on the points[p].rols
        /// </summary>
        /// <param name="player"></param>
        /// <param name="container"></param>
        /// <returns>foundPattern = true if it matches any check else returns false</returns>
        private bool checkWin(int player, StackPanel container)
        {
            int count = 0;
            List<Field> points = new List<Field>();
            bool foundPattern = false;

            #region HorizontalCheck

            for (int i = 0; i < MaxRow; ++i)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int j = 0; j < MaxColumn; ++j)
                {
                    if (_gameMap[MaxRow - i - 1, j] == player)
                    {
                        ++count;
                        points.Add(new Field(MaxRow - i - 1, j));
                        if (count == 4) break;
                    }

                    else if (_gameMap[MaxRow - i - 1, j] != player)
                    {
                        count = 0;
                        points.Clear();
                    }
                }//

                if (count == 4)
                {
                    var containerParent = (StackPanel)container.Parent;
                    foundPattern = true;

                    for (int p = 0; p < points.Count; ++p)
                    {
                        var col = points[p].col;
                        var innerContainer = (StackPanel)containerParent.Children[col];
                        var btn = (Button)innerContainer.Children[points[p].rol];
                        btn.Content = "X";
                    }
                }
            }//
            #endregion

            if (foundPattern)
                return foundPattern;


            #region VerticalCheck
            count = 0;
            points.Clear();

            for (int i = 0; i < MaxColumn; ++i)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int j = 0; j < MaxRow; ++j)
                {
                    if (_gameMap[j, i] == player)
                    {
                        ++count;
                        points.Add(new Field(j, i));
                        if (count == 4) break;
                    }

                    else if (_gameMap[j, i] != player)
                    {
                        count = 0;
                        points.Clear();
                    }
                }//

                if (count == 4)
                {
                    var containerParent = (StackPanel)container.Parent;
                    foundPattern = true;

                    for (int p = 0; p < points.Count; ++p)
                    {
                        var col = points[p].col;
                        var innerContainer = (StackPanel)containerParent.Children[col];
                        var btn = (Button)innerContainer.Children[points[p].rol];
                        btn.Content = "X";
                    }
                }
            }//
            #endregion

            if (foundPattern)
                return foundPattern;

            #region DiagonalWin_1

            count = 0;
            points.Clear();

            for (int col = 0; col < MaxColumn; ++col)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int rol = 3; rol < MaxRow; ++rol)
                {
                    count = 0;
                    points.Clear();

                    if (foundPattern)
                        break;

                    int i = rol;

                    for (int j = col; j < MaxColumn; ++j, --i)
                    {
                        if (i == -1)
                            break;
                        if (_gameMap[MaxRow - i - 1, j] == player)
                        {
                            points.Add(new Field(MaxRow - i - 1, j));
                            ++count;
                            if (count == 4)
                                break;
                        }

                        else if (_gameMap[MaxRow - i - 1, j] != player)
                        {
                            count = 0;
                            points.Clear();
                        }
                    }

                    if (count == 4)
                    {
                        var containerParent = (StackPanel)container.Parent;
                        foundPattern = true;

                        for (int p = 0; p < points.Count; ++p)
                        {
                            var co = points[p].col;
                            var innerContainer = (StackPanel)containerParent.Children[co];
                            var btn = (Button)innerContainer.Children[points[p].rol];
                            btn.Content = "X";
                        }
                    }//

                }//
            }//

            #endregion

            if (foundPattern)
                return foundPattern;


            #region DiagonalWin_2

            count = 0;
            points.Clear();

            for (int col = 0; col < MaxColumn; ++col)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int rol = 3; rol < MaxRow; ++rol)
                {
                    count = 0;
                    points.Clear();

                    if (foundPattern)
                        break;

                    int i = rol;

                    for (int j = col; j >= 0; --j, --i)
                    {
                        if (i == -1)
                            break;
                        if (_gameMap[MaxRow - i - 1, j] == player)
                        {
                            points.Add(new Field(MaxRow - i - 1, j));
                            ++count;
                            if (count == 4)
                                break;
                        }

                        else if (_gameMap[MaxRow - i - 1, j] != player)
                        {
                            count = 0;
                            points.Clear();
                        }
                    }

                    if (count == 4)
                    {
                        var containerParent = (StackPanel)container.Parent;
                        foundPattern = true;

                        for (int p = 0; p < points.Count; ++p)
                        {
                            var co = points[p].col;
                            var innerContainer = (StackPanel)containerParent.Children[co];
                            var btn = (Button)innerContainer.Children[points[p].rol];
                            btn.Content = "X";
                        }
                    }//
                }//
            }//

            #endregion

            if (foundPattern)
                return foundPattern;


            return false;
        }//
    }
}
