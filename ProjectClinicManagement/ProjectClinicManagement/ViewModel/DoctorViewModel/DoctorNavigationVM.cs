using ProjectClinicManagement.Command;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    class DoctorNavigationVM : BaseViewModel
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
        public string Title { get; set; }
        public RelayCommand NavigateToPage1Command { get; }
        public RelayCommand NavigateToPage2Command { get; }

        public ICommand LogoutCommand { get; }
        public DoctorNavigationVM()
        {
           Account a= (Account)Application.Current.Properties["User"];
            Title = a.Role + ": " + a.Name;
            NavigateToPage1Command = new RelayCommand(_ => NavigateToPage1());
            NavigateToPage2Command = new RelayCommand(_ => NavigateToPage2());
            LogoutCommand = new RelayCommand(Logout);
            CurrentPage = new Views.Patient.ViewPatients();
        }

        private void NavigateToPage1()
        {
            CurrentPage = new Views.Patient.ViewPatients();
        }

        private void NavigateToPage2()
        {

            CurrentPage = new Views.Doctor.ViewMedicine();

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
