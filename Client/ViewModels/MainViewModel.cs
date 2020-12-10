using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Commands;
using Client.Commands.Dialog_commands;
using Client.Commands.Road_commands;
using Client.Contracts;
using Client.Models;
using Client.Views;
using Common.Contracts;
using Common.DomainModels;

namespace Client.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IClientEventNotifiable
    {
        #region binding fields
        private BindingList<Road> roads;
        private BindingList<Station> stations;
        private BindingList<Track> tracks;
        private BindingList<Place> places;
        private BindingList<User> users;
        private BindingList<ClientEvent> events;
        private RoadSearchModel roadSearch;
        #endregion

        #region service proxies
        public IRoadService RoadServiceProxy { get; set; }
        public IStationService StationServiceProxy { get; set; }
        public ITrackService TrackServiceProxy { get; set; }

       

        public IPlaceService PlaceServiceProxy { get; set; }
        public IUserService UserServiceProxy { get; set; }
        #endregion

        #region Connectors
        public IRailwayServiceProxyCreationFacade ProxyCreationFacade { get; set; }
        #endregion

        #region commands
        public ICommand RefreshUsersCommand { get; set; }
        public ICommand UpdateProfileCommand { get; set; }
        public ICommand RefreshRoadsCommand { get; set; }
        public ICommand RefreshStationsCommand { get; set; }
        public ICommand RefreshTracksCommand { get; set; }
        public ICommand RefreshPlacesCommand { get; set; }

        public ICommand DeleteStationCommand { get; set; }
        public ICommand DeleteTrackCommand { get; set; }
        public ICommand DeletePlaceCommand { get; set; }

        public ICommand LogoutCommand { get; set; }
        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; set; }

        public ICommand OpenAddUserDialogCommand { get; set; }
        public ICommand OpenAddRoadDialogCommand { get; set; }
        public ICommand OpenChangeRoadDialogCommand { get; set; }
        public ICommand OpenAddTrackDialogCommand { get; set; }
        public ICommand OpenChangeTrackDialogCommand { get; set; }
        public ICommand OpenAddPlaceDialogCommand { get; set; }
        public ICommand OpenChangePlaceDialogCommand { get; set; }
        public ICommand OpenAddStationDialogCommand { get; set; }
        public ICommand OpenChangeStationDialogCommand { get; set; }
        public IPrimaryEntityCommand CloneRoadCommand { get; set; }
        public IPrimaryEntityCommand DeleteRoadCommand { get; set; }

        public ICommand SearchRoadsCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }
        #endregion

        #region managers
        public IPrimaryEntityCommandManagement CommandManager { get; set; }
        #endregion

        #region constructor
        public MainViewModel(User user, Window window, IRailwayServiceProxyCreationFacade proxyCreationFacade, ILogging logger, IPrimaryEntityCommandManagement commandManager)
        {
            User = user;
            ProxyCreationFacade = proxyCreationFacade;
            Logger = logger;
            CommandManager = commandManager;
            Window = window;
            RefreshUsersCommand = new RefreshUsersCommand(this);
            UpdateProfileCommand = new UpdateProfileCommand(this);
            RefreshRoadsCommand = new RefreshRoadsCommand(this);
            RefreshStationsCommand = new RefreshStationsCommand(this);
            RefreshTracksCommand = new RefreshTracksCommand(this);
            RefreshPlacesCommand = new RefreshPlacesCommand(this);
            DeleteRoadCommand = new DeleteRoadCommand(this, commandManager);
            DeleteStationCommand = new DeleteStationCommand(this);
            DeleteTrackCommand = new DeleteTrackCommand(this);
            DeletePlaceCommand = new DeletePlaceCommand(this);
            CloneRoadCommand = new CloneRoadCommand(this, commandManager);
            LogoutCommand = new LogoutCommand(this);
            UndoCommand = new UndoCommand(commandManager);
            RedoCommand = new RedoCommand(commandManager);
            OpenAddUserDialogCommand = new OpenAddUserDialogCommand(this);
            OpenAddRoadDialogCommand = new OpenAddRoadDialogCommand(this);
            OpenChangeRoadDialogCommand = new OpenChangeRoadDialogCommand(this);
            OpenAddTrackDialogCommand = new OpenAddTrackDialogCommand(this);
            OpenChangeTrackDialogCommand = new OpenChangeTrackDialogCommand(this);
            OpenAddPlaceDialogCommand = new OpenAddPlaceDialogCommand(this);
            OpenChangePlaceDialogCommand = new OpenChangePlaceDialogCommand(this);
            OpenAddStationDialogCommand = new OpenAddStationDialogCommand(this);
            OpenChangeStationDialogCommand = new OpenChangeStationDialogCommand(this);
            SearchRoadsCommand = new SearchRoadsCommand(this);
            ClearSearchCommand = new ClearSearchCommand(this);
            RoadSearch = new RoadSearchModel();
            ConnectToAllServices();
            RefreshAllLists();

        }
        #endregion

        #region binding set get
        public RoadSearchModel RoadSearch
        {
            get
            {
                return roadSearch;
            }
            set
            {
                roadSearch = value;
                OnPropertyChanged("RoadSearch");
            }
        }


        public BindingList<Road> Roads
        {
            get
            {
                return roads;
            }
            set
            {
                roads = value;
                OnPropertyChanged("Roads");
            }
        }

        public BindingList<ClientEvent> Events
        {
            get
            {
                return events;
            }
            set
            {
                events = value;
                OnPropertyChanged("Events");
            }
        }

        public BindingList<Station> Stations
        {
            get
            {
                return stations;
            }
            set
            {
                stations = value;
                OnPropertyChanged("Stations");
            }
        }

        public BindingList<Track> Tracks
        {
            get
            {
                return tracks;
            }
            set
            {
                tracks = value;
                OnPropertyChanged("Tracks");
            }
        }

        public BindingList<Place> Places
        {
            get
            {
                return places;
            }
            set
            {
                places = value;
                OnPropertyChanged("Places");
            }
        }

        public BindingList<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

        #endregion

        #region misc set get
        public User User { get; set; }
        public ILogging Logger { get; set; }
        public Window Window { get; set; }
        #endregion

        private void ConnectToAllServices()
        {
            RoadServiceProxy = ProxyCreationFacade.GetRoadServiceProxy(User.UserName, User.Password);
            StationServiceProxy = ProxyCreationFacade.GetStationServiceProxy(User.UserName, User.Password);
            TrackServiceProxy = ProxyCreationFacade.GetTrackServiceProxy(User.UserName, User.Password);
            PlaceServiceProxy = ProxyCreationFacade.GetPlaceServiceProxy(User.UserName, User.Password);
            UserServiceProxy = ProxyCreationFacade.GetUserServiceProxy(User.UserName, User.Password);
        }

        private void RefreshAllLists()
        {
            RefreshUsersList();
            RefreshPlacesList();
            RefreshTracksList();
            RefreshStationsList();
            RefreshRoadsList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region refresh methods

        public void RefreshUsersList()
        {
            Logger.LogNewMessage("Getting all users from server..", LogType.INFO);
            try
            {
                Users = new BindingList<User>(UserServiceProxy.GetAllUsers());
                if(Users == null)
                {
                    Users = new BindingList<User>();
                }
            }catch(Exception e)
            {
                Logger.LogNewMessage($"Error occured getting all users. Error message {e.Message}", LogType.ERROR);
            }
            
        }

        public void RefreshRoadsList()
        {
            Logger.LogNewMessage("Getting all roads from server..", LogType.INFO);
            try
            {
                Roads = new BindingList<Road>(RoadServiceProxy.GetAllRoads());
                if (Roads == null)
                {
                    Roads = new BindingList<Road>();
                }
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured getting all roads. Error message {e.Message}", LogType.ERROR);
            }

        }

        public void RefreshPlacesList()
        {
            Logger.LogNewMessage("Getting all places from server..", LogType.INFO);
            try
            {
                Places = new BindingList<Place>(PlaceServiceProxy.GetAllPlaces());
                if (Places == null)
                {
                    Places = new BindingList<Place>();
                }
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured getting all places. Error message {e.Message}", LogType.ERROR);
            }

        }

        public void RefreshTracksList()
        {
            Logger.LogNewMessage("Getting all tracks from server..", LogType.INFO);
            try
            {
                Tracks = new BindingList<Track>(TrackServiceProxy.GetAllTracks());
                if (Tracks == null)
                {
                    Tracks = new BindingList<Track>();
                }
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured getting all tracks. Error message {e.Message}", LogType.ERROR);
            }

        }

        public void RefreshStationsList()
        {
            Logger.LogNewMessage("Getting all stations from server..", LogType.INFO);
            try
            {
                Stations = new BindingList<Station>(StationServiceProxy.GetAllStations());
                if (Stations == null)
                {
                    Stations = new BindingList<Station>();
                }
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured getting all stations. Error message {e.Message}", LogType.ERROR);
            }

        }
        #endregion

        #region update methods
        public void UpdateUser()
        {
            Logger.LogNewMessage("Updating user data..", LogType.INFO);
            try
            {
                UserServiceProxy.UpdateUser(User.ID, User.Name, User.LastName);
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured updating user info. Error message {e.Message}", LogType.ERROR);
            }
        }
        #endregion

        #region delete methods
        public void DeleteSelectedRoad(Road road)
        {

            try
            {
                Logger.LogNewMessage($"Delete called for road with name {road.Name}..", LogType.INFO);
                RoadServiceProxy.DeleteRoad(road.Id);
                RefreshRoadsList();
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured deleting road. Error message {e.Message}", LogType.ERROR);
            }
        }

        public void DeleteSelectedStation(Station station)
        {

            try
            {
                Logger.LogNewMessage($"Delete called for station with name {station.Name}..", LogType.INFO);
                StationServiceProxy.DeleteStation(station.Id);
                RefreshStationsList();
                RefreshRoadsList();
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured deleting station. Error message {e.Message}", LogType.ERROR);
            }
        }

        public void DeleteSelectedTrack(Track track)
        {

            try
            {
                Logger.LogNewMessage($"Delete called for track with name {track.Name}..", LogType.INFO);
                TrackServiceProxy.DeleteTrack(track.Id);
                RefreshTracksList();
                RefreshStationsList();

            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured deleting track. Error message {e.Message}", LogType.ERROR);
            }
        }

        public void DeleteSelectedPlace(Place place)
        {

            try
            {
                Logger.LogNewMessage($"Delete called for place with name {place.Name}..", LogType.INFO);
                PlaceServiceProxy.DeletePlace(place.Id);
                RefreshStationsList();
                RefreshPlacesList();
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured deleting place. Error message {e.Message}", LogType.ERROR);
            }
        }
        #endregion

        #region road cloning
        public Road CloneSelectedRoad(Road road)
        {

            try
            {
                Logger.LogNewMessage($"Clone called for road with name {road.Name}..", LogType.INFO);
                var road_cloned = RoadServiceProxy.CloneRoad(road);
                RefreshRoadsList();
                return road_cloned;
            }
            catch (Exception e)
            {
                Logger.LogNewMessage($"Error occured deleting road. Error message {e.Message}", LogType.ERROR);
                return null;
            }
        }
        #endregion

        #region notify system events
        public void NotifyEventsUpdated(List<ClientEvent> events)
        {
            Events = new BindingList<ClientEvent>(events);
        }
        #endregion

        #region Log out
        public void Logout()
        {
            new LoginWindow().Show();
            Window.Close();
        }
        #endregion

        #region Add methods
        public Road AddRoad(Road road)
        {
            try
            {
                Logger.LogNewMessage($"Adding road with name {road.Name}", LogType.INFO);
                var road_added = RoadServiceProxy.AddRoad(road);
                RefreshRoadsList();
                return road_added;

            }catch(Exception ex)
            {
                Logger.LogNewMessage($"Road couldn't be added. Error: {ex.Message}", LogType.ERROR);
                return null;
            }
        }
        #endregion

        #region Open dialog methods
        public void OpenAddUserDialog()
        {
            new AddUserWindow(User, ProxyCreationFacade, Logger).Show();
        }

        public void OpenAddRoadDialog()
        {
            new RoadInputWindow(User, ProxyCreationFacade, new Road(), Logger, CommandManager).Show();
        }

        public void OpenAddPlaceDialog()
        {
            new PlaceInputWindow(ProxyCreationFacade, User, Logger, new Place()).Show();
        }

        public void OpenAddTrackDialog()
        {
            new TrackInputWindow(ProxyCreationFacade, User, Logger, new Track()).Show();
        }

        public void OpenAddStationDialog()
        {
            new StationInputWindow(User, ProxyCreationFacade, new Station(), Logger).Show();
        }

        public void OpenChangeTrackDialog(Track track)
        {
            new TrackInputWindow(ProxyCreationFacade, User, Logger, track).Show();
        }

        public void OpenChangeRoadDialog(Road road)
        {
            new RoadInputWindow(User, ProxyCreationFacade, road, Logger, CommandManager).Show();
        }

        public void OpenChangePlaceDialog(Place place)
        {
            new PlaceInputWindow(ProxyCreationFacade, User, Logger, place).Show();
        }

        public void OpenChangeStationDialog(Station station)
        {
            new StationInputWindow(User, ProxyCreationFacade, station, Logger).Show();
        }
        #endregion

        #region Search methods
        public void SearchRoads(RoadSearchModel model)
        {
            try
            {
                Logger.LogNewMessage($"Searching for road with name {model.Name} and label {model.Label}.", LogType.INFO);
                Roads =  new BindingList<Road>(RoadServiceProxy.SearchRoads(model.Name, model.Label));

            }catch(Exception ex)
            {
                Logger.LogNewMessage($"Error occured during search. Message {ex.Message}", LogType.ERROR);
            }
        }

        public void ClearRoadsSearch()
        {
            RoadSearch.Name = "";
            RoadSearch.Label = "";
            RefreshRoadsList();
        }
        #endregion
    }
}
