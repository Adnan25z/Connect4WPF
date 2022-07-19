using Caliburn.Micro;

using Connect4Game.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Connect4Game
{
    public class BootStrapper : BootstrapperBase
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        SplashScreenViewModel splashScreen = new SplashScreenViewModel();

        /// <summary>
        /// Initialize the bootsrapper base framework
        /// </summary>
        public BootStrapper()
        {
            Initialize();
        }

        /// <summary>
        /// global event handler to provide overrides for internal exception handlers
        /// A DispatcherTimer object named dispatcherTimer is created. 
        /// The event handler OnTimedEvent is added to the Tick event of dispatcherTimer. 
        /// The Interval is set to 5 second using a TimeSpan object, and the timer is started.
        /// Window manager will open the new window of SplashScreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Tick += OnTimedEvent;
            dispatcherTimer.Start();
            WindowManager windowManager = new WindowManager();
            windowManager.ShowWindowAsync(splashScreen);
        }

        /// <summary>
        /// With this code we tell the application to close the splashscreen and start using
        /// bootstrapper by calling the ShellViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            splashScreen.TryCloseAsync();
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
