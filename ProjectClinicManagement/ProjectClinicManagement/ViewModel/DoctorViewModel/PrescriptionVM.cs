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
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class PrescriptionVM : BaseViewModel
    {
      

        public static Prescription PrescriptionInstan;
        private List<Prescription> _Prescriptions;
        public List<Prescription> Prescriptions
        {
            get { return _Prescriptions; }
            set
            {
                _Prescriptions = value;
                OnPropertyChanged();
            }
        }

        private Prescription _Prescription;
        public Prescription Prescription
        {
            get { return (Prescription)_Prescription; }
            set
            {
                _Prescription = value;
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
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                ApplyFilters(); // Thay đổi tên hàm gọi tới
                PlaceHolderText = string.Empty;
            }
        }

        private string placeHolderText;
        public string PlaceHolderText
        {
            get { return placeHolderText; }
            set
            {
                placeHolderText = value;
                OnPropertyChanged();


            }

        }
       
     

        public ICommand FilterByStatusCommand { get; set; }
        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }

        private readonly DataContext _context;


        public PrescriptionVM()
        {
            _context = new DataContext();
            LoadPrescription();
            placeHolderText = "Search by Patient Name, RecordID";

        }




        private void LoadPrescription()
        {
            Prescriptions = new List<Prescription>(_context.Prescriptions);

        }


        private void ApplyFilters()
        {
            // Filter by search text  if applicable
            if (string.IsNullOrEmpty(SearchText))
            {
                Prescriptions = new List<Prescription>(_context.Prescriptions.ToList());
            }
            else
            {
                Prescriptions = _context.Prescriptions
                 .Include(p => p.Patient_Record)
                 .Where(p =>
                     p.PatientRecordId.ToString().Contains(searchText) ||
                     p.Patient_Record.Patient.Name.Contains(searchText))
                 .ToList();


            }
        }
        private void FilterPrescriptions()
        {
            var filteredPrescriptions = _context.Prescriptions.Include(p => p.Patient_Record).AsQueryable();

            if (FromDate.HasValue)
            {
                filteredPrescriptions = filteredPrescriptions.Where(p => p.Date >= FromDate.Value);
            }

            if (ToDate.HasValue)
            {
                filteredPrescriptions = filteredPrescriptions.Where(p => p.Date <= ToDate.Value);
            }

            Prescriptions = new List<Prescription>(filteredPrescriptions.ToList());
            OnPropertyChanged(nameof(Prescriptions));
        }

    }




}
