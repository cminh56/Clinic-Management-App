using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class PrescriptionVM : BaseViewModel
    {
        public static Prescription PrescriptionInstan;

        private List<Prescription> _prescriptions;
        public List<Prescription> Prescriptions
        {
            get { return _prescriptions; }
            set
            {
                _prescriptions = value;
                OnPropertyChanged();
            }
        }

        private Prescription _prescription;
        public Prescription Prescription
        {
            get { return _prescription; }
            set
            {
                _prescription = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _fromDate;
        private DateTime? _toDate;
        public DateTime? FromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
                OnPropertyChanged(nameof(FromDate));
                FilterPrescriptions();
            }
        }

        public DateTime? ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                OnPropertyChanged(nameof(ToDate));
                FilterPrescriptions();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilters();
                PlaceHolderText = string.Empty;
            }
        }

        private string _placeHolderText;
        public string PlaceHolderText
        {
            get { return _placeHolderText; }
            set
            {
                _placeHolderText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ViewPrescriptionCommand { get; set; }
        public ICommand EditPrescriptionCommand { get; set; }
        public ICommand FilterByStatusCommand { get; set; }
        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }

        private readonly DataContext _context;

        public int _currentPage = 1; // Current page
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                LoadPrescription(); // Load prescriptions when the current page changes
            }
        }

        public int _itemsPerPage = 10; // Items per page
        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set
            {
                _itemsPerPage = value;
                OnPropertyChanged();
                LoadPrescription(); // Reload prescriptions when items per page changes
            }
        }

        public int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }
        private Patient_Record _patient;
        public Patient_Record Patient
        {
            get { return (Patient_Record)_patient; }
            set
            {
                _patient = value;
                OnPropertyChanged();

            }
        }

        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand PatientRecordCommand { get; }
        public PrescriptionVM(Patient_Record patient)
        {
            Patient = patient;
            _context = new DataContext();
            LoadPrescription();
            PlaceHolderText = "Search by Patient Name, RecordID";
            ViewPrescriptionCommand = new RelayCommand(NavigateToViewPrescriptionPage);
            EditPrescriptionCommand = new RelayCommand(NavigateToEditPrescriptionPage);
            PatientRecordCommand = new RelayCommand(NavigateToEditPatientRecordPage);
            NextPageCommand = new RelayCommand(NextPage, CanNextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage, CanPreviousPage);
        }

        private void NavigateToViewPrescriptionPage(object parameter)
        {
            if (_prescription != null)
            {
                PrescriptionInstan = Prescription;
                NavigationService.Navigate(new Uri("Views/Doctor/ViewPrescriptionDetail.xaml", UriKind.Relative));
            }
        }

        private void NavigateToEditPrescriptionPage(object parameter)
        {
            if (_prescription != null)
            {
                PrescriptionInstan = Prescription;
                NavigationService.Navigate(new Uri("Views/Doctor/EditPrescription.xaml", UriKind.Relative));
            }
        }

        private void LoadPrescription()
        {
            var query = _context.Prescriptions.Include(p => p.Patient_Record).Where(p => p.PatientRecordId == Patient.Id).AsQueryable();

            if (!string.IsNullOrEmpty(SearchText))
            {
                query = query.Where(p =>
                    p.Duration.Contains(SearchText) ||
                    p.Remark.Contains(SearchText));
            }

            if (FromDate.HasValue)
            {
                query = query.Where(p => p.Date >= FromDate.Value);
            }

            if (ToDate.HasValue)
            {
                query = query.Where(p => p.Date <= ToDate.Value);
            }

            var totalItems = query.Count();
            TotalPage = (totalItems + ItemsPerPage - 1) / ItemsPerPage;

            Prescriptions = query
                .Skip((CurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToList();
        }

        private void ApplyFilters()
        {
            LoadPrescription(); // Reapply the filter and reload prescriptions
        }

        private void FilterPrescriptions()
        {
            LoadPrescription(); // Reload prescriptions based on date filters
        }

        private void NextPage(object parameter)
        {
            if (CurrentPage < TotalPage)
            {
                CurrentPage++;
            }
        }

        private bool CanNextPage(object parameter)
        {
            return CurrentPage < TotalPage;
        }

        private void PreviousPage(object parameter)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        private bool CanPreviousPage(object parameter)
        {
            return CurrentPage > 1;
        }
        private void NavigateToEditPatientRecordPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Patient/PatientRecord.xaml", UriKind.Relative));
        }
    }
}
