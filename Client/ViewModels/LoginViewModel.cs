using Client.Commands;
using Client.Contracts;
using Common.Contracts;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel(Window window, ILogging logger, IRailwayServiceProxyCreationFacade connector)
        {
            Window = window;
            Logger = logger;
            LoginCommand = new LoginCommand(this);
            QuitCommand = new QuitLoginWindowCommand(this);
            Connector = connector;
        }

        public Window Window { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IUserService UserService { get; set; }
        public ILogging Logger { get; set; }
        public IRailwayServiceProxyCreationFacade Connector { get; set; }
        

        public ICommand LoginCommand
        {
            get;
            private set;
        }

        public ICommand QuitCommand
        {
            get;
            private set;
        }


        public bool CanLogin()
        {
            return Username != "" && Password != "";
        }

        public bool Login()
        {
            try
            {
                Logger.LogNewMessage($"Trying to log in with specified client credentials..", LogType.INFO);
                Window.Cursor = Cursors.Wait;
                UserService = Connector.GetUserServiceProxy(Username, Password);
                new MainWindow(UserService.FindUserByUserName(Username)).Show();
                Window.Close();

                return true;
            }catch(MessageSecurityException ex)
            {
                Logger.LogNewMessage($"Credentials invalid, access denied by host.", LogType.WARNING);
                MessageBox.Show(Window, "Entered credentials are invalid. Access denied.", "Wrong credentials");
                Window.Cursor = Cursors.Arrow;
                return false;
            }
            catch(Exception exc)
            {
                Logger.LogNewMessage($"Unknown error happened while trying to log in {exc.Message}", LogType.ERROR);
                MessageBox.Show(Window, "Unknown error occured while processing your request. Please try again.", "Error.");
                Window.Cursor = Cursors.Arrow;
                return false;
            }
        }

    }
}
