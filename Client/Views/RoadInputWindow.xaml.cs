using Client.Commands.Road_commands;
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
    /// Interaction logic for RoadInputWindow.xaml
    /// </summary>
    public partial class RoadInputWindow : Window
    {
        public RoadInputWindow(User user, IRailwayServiceProxyCreationFacade facade, Road predicate, ILogging logger, IPrimaryEntityCommandManagement manager)
        {
            IRoadService roadService = facade.GetRoadServiceProxy(user.UserName, user.Password);
            IStationService stationService = facade.GetStationServiceProxy(user.UserName, user.Password);
            RoadInputViewModel viewModel = new RoadInputViewModel(roadService, stationService, predicate, logger, this, manager);
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
