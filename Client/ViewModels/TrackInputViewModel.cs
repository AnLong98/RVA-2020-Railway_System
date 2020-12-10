using Client.Commands.Track_Commands;
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
    public class TrackInputViewModel : INotifyPropertyChanged
    {
        #region constructor
        public TrackInputViewModel(Track newTrack, ITrackService trackService, ILogging logger, Window window)
        {
            NewTrack = newTrack;
            TrackService = trackService;
            Logger = logger;
            Window = window;
            if(NewTrack.IsValid())
            {
                SaveTrackCommand = new UpdateTrackCommand(this);
            }
            else
            {
                SaveTrackCommand = new AddTrackCommand(this);
            }
        }
        #endregion

        #region Get set
        private Track newTrack;

        public Track NewTrack
        {
            get { return newTrack; }
            set {
                newTrack = value;
                OnPropertyChanged("NewTrack");
            }
        }
        public IList<EntranceType> EntranceTypes
        {
            get
            {
                return Enum.GetValues(typeof(EntranceType)).Cast<EntranceType>().ToList<EntranceType>();
            }
        }
        public Window Window { get; set; }
        #endregion

        #region Proxies
        public ITrackService TrackService { get; set; }
        #endregion

        #region Commands
        public ICommand SaveTrackCommand { get; set; }
        #endregion

        #region Logger
        public ILogging Logger { get; set; }
        #endregion

        #region Functionalities
        public void AddTrack(Track track)
        {
            try
            {
                Logger.LogNewMessage($"Trying to add new track with name {track.Name}", LogType.INFO);
                TrackService.AddTrack(track);
                Window.Close();
            }catch(Exception ex)
            {
                Logger.LogNewMessage($"Error occured adding track with name {track.Name}. Message {ex.Message}", LogType.ERROR);
            }
        }

        public void UpdateTrack(Track track)
        {
            try
            {
                Logger.LogNewMessage($"Trying to update track with id {track.Id}", LogType.INFO);
                TrackService.UpdateTrack(track);
                Window.Close();
            }
            catch (Exception ex)
            {
                Logger.LogNewMessage($"Error occured updating track with id {track.Id}. Message {ex.Message}", LogType.ERROR);
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
