using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;

namespace Connect4Game.ViewModels
{

    /// <summary>
    /// This implementation has all the features of Conductor but also adds the notion of a “screen collection.”
    /// Since conductors in CM can conduct any type of class, this collection is exposed through an IObservableCollection called Items rather than Screens.
    /// As a result of the presence of the Items collection, deactivation and closing of conducted items are not treated synonymously. 
    /// When a new item is activated, the previous active item is deactivated only and it remains in the Items collection. 
    /// To close an item with this conductor, you must explicitly call its CloseItem method. 
    /// When an item is closed and that item was the active item, the conductor must then determine which item should be activated next. 
    /// By default, this is the item before the previous active item in the list. If you need to change this behavior, you can override DetermineNextItemToActivate.
    /// </summary>
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {

        /// <summary>
        /// ShellViewModel will be the class that will handle all our windows on the application
        /// </summary>
        public ShellViewModel()
        {
            ActivateItemAsync(new GameMenuViewModel(CloseCommand));
        }

        /// <summary>
        /// Displays the system menu for the specified window.
        /// </summary>
        public void MenuCommand()
        {
            Window mwindow = (Window)GetView();
            SystemCommands.ShowSystemMenu(mwindow, mwindow.PointToScreen(Mouse.GetPosition(mwindow))); //The window to have its system menu displayed, The location of the system menu.
        }

        /// <summary>
        /// The getView() method can inflate the view from a layout resource or create it programmatically. It gets a view previously attached to the instance 
        /// The Window.close() method closes the current window, or the window on which it was called.
        /// </summary>
        public void CloseCommand()
        {
            Window mwindow = (Window)GetView();
            mwindow.Close();
        }

        /// <summary>
        /// Specifies whether a window is minimized, maximized, or restored. Used by the WindowState property.
        /// </summary>
        public void MinimiseCommand()
        {
            Window mwindow = (Window)GetView();
            mwindow.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Specifies whether a window is minimized, maximized, or restored. Used by the WindowState property.
        /// </summary>
        public void MaximizeCommand()
        {
            Window mwindow = (Window)GetView();
            mwindow.WindowState ^= WindowState.Maximized;
        }

        /// <summary>
        /// Activates the closecommand from gameMenuViewModel and then clears all the items stored and activates homepage as a asynchronous operation
        /// </summary>
        public void BackHomeCommand()
        {
            if (Items.Count > 1)
            {
                ActivateItemAsync(new GameMenuViewModel(CloseCommand));
                var homePage = Items[0];
                Items.Clear();
                ActivateItemAsync(homePage);
            }
        }

        /// <summary>
        /// Checks conmdition to go back a page if it matches gamemenuviewmodel or choosecolorviewmodel
        /// then we call the back home command in the first case otherwise recursively call backOnepage 
        /// then activate the view stored in the previous index
        /// </summary>
        public void BackOnePage()
        {
            if (Items.Count > 1)
            {
                if (Items[Items.Count - 1].GetType() == typeof(GameMenuViewModel))
                {
                    BackHomeCommand();
                    return;
                }
                Items.RemoveAt(Items.Count - 1);
                if(Items[Items.Count - 1].GetType() == typeof(ChooseColorViewModel))
                    BackOnePage();
                var index = Items.Count - 1;
                ActivateItemAsync(Items[index]);
            }
        }
    }
}
