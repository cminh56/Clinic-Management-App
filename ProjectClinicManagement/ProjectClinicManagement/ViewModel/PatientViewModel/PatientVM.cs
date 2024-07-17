using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    public class PatientVM : BaseViewModel
    {
        private List<Patient> _Patients;
        public List<Patient> Patients
        {
            get { return _Patients; }
            set
            {
                _Patients = value;
                OnPropertyChanged();
            }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPatientCommand { get; set; }
        public ICommand EditPatientCommand { get; set; }

        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }

        private readonly DataContext _context;

        public PatientVM()
        {
            _context = new DataContext();
            Patients = new List<Patient>(_context.Patients);

            AddPatientCommand = new RelayCommand(NavigateToAddPatientPage);
            EditPatientCommand = new RelayCommand(NavigateToEditPatientPage, CanExecuteUserCommand);
        }

        private void NavigateToAddPatientPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Doctor/AddPatient.xaml", UriKind.Relative));
        }

        private void NavigateToEditPatientPage(object parameter)
        {
            if (SelectedPatient != null)
            {
                NavigationService?.Navigate(new Uri($"Views/Doctor/EditPatient.xaml?id={SelectedPatient.Id}", UriKind.Relative));
            }
        }

        private bool CanExecuteUserCommand(object parameter)
        {
            return SelectedPatient != null;
        }
    }
}
