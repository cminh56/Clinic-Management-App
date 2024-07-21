using Microsoft.EntityFrameworkCore;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.Views.Patient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using ProjectClinicManagement.ViewModel.Common;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    public class PatientRecordVM : BaseViewModel
    {
        public static Patient_Record patientInstan;
        private int id;
        private string patientName;
        private string doctorName;
        private string disease;
        private string symptoms;
        private string diagnosis;
        private DateTime date;
        public string name { get; set; }
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

    
         public string PatientName
        {
            get { return patientName; }
            set { patientName = value; OnPropertyChanged(); }
        }
        public string DoctorName
        {
            get { return doctorName; }
            set { doctorName = value; OnPropertyChanged(); }
        }
        public string Disease
        {
            get { return disease; }
            set { disease = value; OnPropertyChanged(); }
        }
        public string Symptoms
        {
            get { return symptoms; }
            set { symptoms = value; OnPropertyChanged(); }
        }
        public string Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; OnPropertyChanged(); }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }


        private readonly DataContext _context;


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
        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }
        public ICommand ViewPrescriptionCommand { get; set; }
        public ICommand AddPrescriptionCommand { get; set; }
        public ICommand EditPatientRecordCommand { get; set; }

        public ICommand ViewPatientCommand { get; }

        public PatientRecordVM(Patient patient)
        {
            _context = new DataContext();
            Account a = (Account)Application.Current.Properties["User"];
            name = a.Name;
            AddPrescriptionCommand = new RelayCommand(NavigateToAddPrescriptionPage);
            ViewPrescriptionCommand = new RelayCommand(NavigateToViewPrescriptionPage);
            EditPatientRecordCommand=new RelayCommand(NavigateToEditPatientRecordPage);
            ViewPatientCommand = new RelayCommand(NavigateToPatientPage);

            var _patient = _context.Patient_Records.Where(x=>x.PatientId==patient.Id).Include(x=>x.Patient).Include(x=>x.Doctor).FirstOrDefault();
            if (_patient != null)
            {
                Patient = _patient;
                Id=_patient.Id;
                PatientName = _patient.Patient.Name;
                DoctorName = _patient.Doctor.Name;
                Symptoms = _patient.Symptoms;
                Diagnosis = _patient.Diagnosis;
                Disease = _patient.Disease;
                Date= _patient.Date;
            }
           
        }

        private void NavigateToAddPrescriptionPage(object parameter)
        {
            patientInstan = _patient;
            NavigationService?.Navigate(new Uri("Views/Doctor/AddPrescription.xaml", UriKind.Relative));
        }
        private void NavigateToViewPrescriptionPage(object parameter)
        {
            patientInstan = _patient;
            NavigationService?.Navigate(new Uri("Views/Doctor/ViewPrescription.xaml", UriKind.Relative));
        }
        private void NavigateToEditPatientRecordPage(object parameter)
        {
            patientInstan = _patient;
            NavigationService?.Navigate(new Uri("Views/Patient/EditPatientRecord.xaml", UriKind.Relative));
        }
        private void NavigateToPatientPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Patient/ViewPatients.xaml", UriKind.Relative));
        }
    }
}
