// Code by: @andrelosse

using System.Windows;
using MinkVPN.Core;

namespace MinkVPN.MVVM.ViewModel
{
    internal class MainViewModel : ObservObj
    {
        /* Commands */
        public RelayCommands ToMoveWindow { get; set; }
        public RelayCommands ToRemoveWindow { get; set; }
        public RelayCommands ToMinimizeWindow { get; set; }
        public RelayCommands ToMaximizeWindow { get; set; }

        public RelayCommands ShowProtectionView { get; set; }
        public RelayCommands ShowSettingsView { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                OnPropertyChanged();
            }
        }

        public ProtectionViewModel ProtectionVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }


        public MainViewModel()
        {

            ProtectionVM = new ProtectionViewModel();
            CurrentView = ProtectionVM;
            SettingsVM = new SettingsViewModel();

            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Application.Current.MainWindow.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            ToMoveWindow = new RelayCommands(o => { Application.Current.MainWindow.DragMove(); });
            ToRemoveWindow = new RelayCommands(o => { Application.Current.Shutdown(); });
            ToMinimizeWindow = new RelayCommands(o => { Application.Current.MainWindow.WindowState = WindowState.Normal; });
            ToMaximizeWindow = new RelayCommands(o => {
                // Simplify this part in a new version
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }
            });

            ShowProtectionView = new RelayCommands(o => { CurrentView = ProtectionVM; });
            ShowSettingsView = new RelayCommands(o => { CurrentView = SettingsVM; });

        }
    }
}
