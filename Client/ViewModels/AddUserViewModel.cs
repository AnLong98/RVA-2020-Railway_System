using Client.Commands;
using Client.Contracts;
using Client.Models;
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
    public class AddUserViewModel : INotifyPropertyChanged
    {
        public AddUserViewModel(IUserService userService, ILogging logger, User principal, User newUser, Window window)
        {
            UserService = userService;
            Logger = logger;
            Principal = principal;
            NewUser = newUser;
            Window = window;
            AddUserCommand = new AddUserCommand(this);
        }

        #region fields
        public IUserService UserService { get; set; }
        public ILogging Logger { get; set; }
        public User Principal { get; set; }
        private User newUser;
        public Window Window { get; set; }
        public User NewUser
        {
            get { return newUser; }
            set {
                newUser = value;
                OnPropertyChanged("NewUser");
            }
        }
        #endregion

        #region commands
        public ICommand AddUserCommand { get; set; }
        #endregion

        #region Bindings
        public string Name { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        #endregion

        #region AddUser
        public void AddUser(User user)
        {
            try
            {
                Logger.LogNewMessage($"Trying to add new user with username {NewUser.UserName}", LogType.INFO);
                UserService.AddUser(user);
                Logger.LogNewMessage($"User added successfully", LogType.INFO);
                Window.Close();
            }
            catch(Exception ex)
            {
                Logger.LogNewMessage($"Failed to add user. Error message {ex.Message}", LogType.ERROR);
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
