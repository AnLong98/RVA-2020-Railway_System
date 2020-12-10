using Client.ViewModels;
using Client.Connectors;
using Client.Loggers;
using System.Windows;
using Client.Managers;
using Common.DomainModels;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(User user)
        {
            MainViewModel mainViewModel = new MainViewModel(user, this, new RailwayServiceConnector(), ClientLogger.GetOrCreate(), new PrimaryEntityCommandManager());
            DataContext = mainViewModel;
            ClientEventManager.GetOrCreate().RegisterEventObserver(mainViewModel);
            InitializeComponent();
            
        }
    }
}
