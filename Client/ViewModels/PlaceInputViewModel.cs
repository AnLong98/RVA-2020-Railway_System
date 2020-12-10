using Client.Commands.Place_commands;
using Common.Contracts;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class PlaceInputViewModel : INotifyPropertyChanged
    {
        #region constructor
        public PlaceInputViewModel(Place newPlace, IPlaceService placeService, ICountryService countryService, ILogging logger, Window window)
        {
            PlaceService = placeService;
            CountryService = countryService;
            Logger = logger;
            Window = window;
            NewPlace = newPlace;
            if (NewPlace.IsValid())
            {
                SavePlaceCommand = new UpdatePlaceCommand(this);
            }
            else
            {
                SavePlaceCommand = new AddPlaceCommand(this);
            }
            InitCountries();
        }
        #endregion

        #region Get set
        private Place newPlace;

        public Place NewPlace
        {
            get
            {
                return newPlace;
                
            }
            set
            {
                newPlace = value;
                OnPropertyChanged("NewPlace");
            }
        }

        private BindingList<Country> countries;
        public BindingList<Country> Countries
        {
            get
            {
                return countries;
            }
            set
            {
                countries = value;
                OnPropertyChanged("Countries");
            }
        }
        public Window Window { get; set; }
        #endregion

        #region Proxies
        public IPlaceService PlaceService { get; set; }
        public ICountryService CountryService { get; set; }
        #endregion

        #region Commands
        public ICommand SavePlaceCommand { get; set; }
        #endregion

        #region Logger
        public ILogging Logger { get; set; }
        #endregion

        #region Functionalities
        public void AddPlace(Place place)
        {
            try
            {
                Logger.LogNewMessage($"Trying to add new place with name {place.Name}", LogType.INFO);
                PlaceService.AddPlace(place);
                Window.Close();
            }
            catch (Exception ex)
            {
                Logger.LogNewMessage($"Error occured adding place with name {place.Name}. Message {ex.Message}", LogType.ERROR);
            }
        }

        public void UpdatePlace(Place place)
        {
            try
            {
                Logger.LogNewMessage($"Trying to update place with id {place.Id}", LogType.INFO);
                PlaceService.UpdatePlace(place);
                Window.Close();
            }
            catch (Exception ex)
            {
                Logger.LogNewMessage($"Error occured updating place with id {place.Id}. Message {ex.Message}", LogType.ERROR);
            }
        }
        #endregion

        #region Initializers
        public void InitCountries()
        {
            Countries = new BindingList<Country>(CountryService.GetAll());
            if(NewPlace.IsValid())
            {
                NewPlace.Country = Countries.First(x => x.Id == NewPlace.Country.Id); //Necessary because initial value in combo box is not set. 
                //Initial value must reference one of the collections members
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
