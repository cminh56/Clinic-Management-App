using ProjectClinicManagement.Command;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
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

namespace ProjectClinicManagement.ViewModel.Receiptor
{
    public class ReceiptorNavigationVM : BaseViewModel
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
        public ICommand LogoutCommand { get; }
        public ICommand DoctorPage { get; }
        public ICommand ReceiptPage { get; }
        public ICommand PatientPage { get; }

        public ReceiptorNavigationVM()
        {
            Account User = (Account)Application.Current.Properties["User"];
            Name = User.Name;
            CurrentPage = new Views.Receiptor.ViewReceipts();
            PatientPage = new RelayCommand(NavigateToPatientPage);
            ReceiptPage = new RelayCommand(NavigateToReceiptPage);
            DoctorPage = new RelayCommand(NavigateToDoctorPage);
            LogoutCommand = new RelayCommand(Logout);


        }
        private void NavigateToDoctorPage(Object pra)
        {
            CurrentPage = new Views.Receiptor.ViewReceipts();
        }
        private void NavigateToPatientPage(Object pra)
        {
            CurrentPage = new Views.Receiptor.ViewReceipts();
        }
        private void NavigateToReceiptPage(Object pra)
        {
            CurrentPage = new Views.Receiptor.ViewReceipts();
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
