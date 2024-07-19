using ProjectClinicManagement.Command;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class AdminNavigationVM : BaseViewModel
    {
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public RelayCommand NavigateToPage1Command { get; }
        public RelayCommand NavigateToPage2Command { get; }
        public ICommand LogoutCommand { get; }

        public AdminNavigationVM()
        {
            NavigateToPage1Command = new RelayCommand(_ => NavigateToPage1());
            NavigateToPage2Command = new RelayCommand(_ => NavigateToPage2());
            LogoutCommand = new RelayCommand(Logout);
            CurrentPage = new Views.Admin.ViewUsers();
        }

        private void NavigateToPage1()
        {
            CurrentPage = new Views.Admin.ViewUsers();
        }

        private void NavigateToPage2()
        {
            //CurrentPage = new Views.Page2();
        }
        private void Logout(object? parameter)
        {
            // Clear user information
            Application.Current.Properties["UserName"] = null;
            Application.Current.Properties["UserRole"] = null;

            // Reopen login window
            Login loginWindow = new Login();
            loginWindow.Show();

            // Close current window
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }

            // Set current window to login window
            Application.Current.MainWindow = loginWindow;
        }
    }
}