using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class MedicineVM : BaseViewModel
    {
        private List<Medicine> _medicines;
        public List<Medicine> Medicines
        {
            get { return _medicines; }
            set
            {
                _medicines = value;
                OnPropertyChanged();
            }
        }

        private Medicine _selectedMedicine;
        public Medicine SelectedMedicine
        {
            get { return _selectedMedicine; }
            set
            {
                _selectedMedicine = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddMedicineCommand { get; set; }
        public ICommand EditMedicineCommand { get; set; }

        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }

        private readonly DataContext _context;

        public MedicineVM()
        {
            _context = new DataContext();
            Medicines = new List<Medicine>(_context.Medicines);

            AddMedicineCommand = new RelayCommand(NavigateToAddMedicinePage);
            EditMedicineCommand = new RelayCommand(NavigateToEditMedicinePage, CanExecuteUserCommand);
        }

        private void NavigateToAddMedicinePage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Doctor/AddMedicine.xaml", UriKind.Relative));
        }

        private void NavigateToEditMedicinePage(object parameter)
        {
            if (parameter is Medicine selectedMedicine)
            {
                NavigationService?.Navigate(new Uri($"Views/Doctor/EditMedicine.xaml?id={selectedMedicine.Id}", UriKind.Relative));
            }
        }


        private bool CanExecuteUserCommand(object parameter)
        {
            return SelectedMedicine != null;
        }
    }
}
