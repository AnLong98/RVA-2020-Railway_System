using Client.Contracts;
using Client.ViewModels;
using Common.Contracts;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for StationInputWindow.xaml
    /// </summary>
    public partial class TrackInputWindow : Window
    {
        public TrackInputWindow(IRailwayServiceProxyCreationFacade facade, User principal, ILogging logger, Track track)
        {
            ITrackService trackService = facade.GetTrackServiceProxy(principal.UserName, principal.Password);
            TrackInputViewModel viewModel = new TrackInputViewModel(track, trackService, logger, this);
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
