﻿using Client.Contracts;
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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow(User principal, IRailwayServiceProxyCreationFacade connector, ILogging logger)
        {
            IUserService userService = connector.GetUserServiceProxy(principal.UserName, principal.Password);
            AddUserViewModel viewModel = new AddUserViewModel(userService, logger, principal, new User(), this);
            DataContext = viewModel;
            InitializeComponent();
            
        }
    }
}
