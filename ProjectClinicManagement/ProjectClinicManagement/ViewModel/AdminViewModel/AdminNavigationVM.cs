using ProjectClinicManagement.Command;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.Views;
using ProjectClinicManagement.Views.Authentication;
using ProjectClinicManagement.Views.Doctor;
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
        public string Name { get; set; }


        public RelayCommand NavigateToPage1Command { get; }
        public RelayCommand NavigateToPage2Command { get; }
        public RelayCommand NavigateToDoctorCommand { get; }
        public RelayCommand NavigateToReceiptorCommand { get; }
        public ICommand LogoutCommand { get; }

        public AdminNavigationVM()
        {
            Account User = (Account)Application.Current.Properties["User"];
            Name = User.Name;

            NavigateToPage1Command = new RelayCommand(_ => NavigateToPage1());
            NavigateToDoctorCommand= new RelayCommand(_ => NavigateToDoctor());
            NavigateToReceiptorCommand = new RelayCommand(_ => NavigateToReceiptor());
            LogoutCommand = new RelayCommand(Logout);
            CurrentPage = new Views.Admin.ViewUsers();
        }

        private void NavigateToPage1()
        {
            CurrentPage = new Views.Admin.ViewUsers();
        }
        private void NavigateToDoctor()
        {
            DoctorWindow doctorWindow = new DoctorWindow();
            doctorWindow.Show();
        }

        private void NavigateToReceiptor()
        {
            DoctorWindow doctorWindow = new DoctorWindow();
            doctorWindow.Show();
        }



        private void Logout(object? parameter)
        {
            // Clear user information
            Application.Current.Properties["UserName"] = null;
            Application.Current.Properties["UserRole"] = null;
            Application.Current.Properties["User"] = null;
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            // Reopen login window
            AuthenWindow loginWindow = new AuthenWindow();
            loginWindow.Show();

             
    if (currentWindow != null)
    {
        currentWindow.Close();
    }

    // Set the current main window to the login window
    Application.Current.MainWindow = loginWindow;
        }
    }
}