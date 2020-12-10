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
    /// Interaction logic for PlaceInputWindow.xaml
    /// </summary>
    public partial class PlaceInputWindow : Window
    {
        public PlaceInputWindow(IRailwayServiceProxyCreationFacade facade, User principal, ILogging logger, Place place)
        {
            IPlaceService placeService = facade.GetPlaceServiceProxy(principal.UserName, principal.Password);
            ICountryService countryService = facade.GetCountryServiceProxy(principal.UserName, principal.Password);
            PlaceInputViewModel viewModel = new PlaceInputViewModel(place, placeService, countryService, logger, this);
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
