﻿using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    class AddPatientRecordVM : BaseViewModel, INotifyDataErrorInfo
    {
        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];

            }
            else
            {
                return Enumerable.Empty<string>();
            }

        }

        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);


            if (results.Any())
            {
                // Check if propertyName already exists in Errors
                if (Errors.ContainsKey(propertyName))
                {
                    // Update existing errors for propertyName
                    Errors[propertyName] = results.Select(r => r.ErrorMessage).ToList();
                }
                else
                {
                    // Add new entry for propertyName
                    Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }



            EditPatientRecordCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }
        public Patient patientInstan;
        private int id;
        private string patientName;
        private string doctorName;
        private string disease;
        private string symptoms;
        private string diagnosis;
        private DateTime date;




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
        [Required(ErrorMessage = "Disease is required.")]

        public string Disease
        {
            get { return disease; }
            set { disease = value; Validate(nameof(Disease), value); }
        }
        [Required(ErrorMessage = "Symptoms is required.")]

        public string Symptoms
        {
            get { return symptoms; }
            set { symptoms = value; Validate(nameof(Symptoms), value); }
        }
        [Required(ErrorMessage = "Diagnosis is required.")]

        public string Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; Validate(nameof(Diagnosis), value); }

        }
        [Required(ErrorMessage = "Date is required.")]

        public DateTime Date
        {
            get { return date; }
            set { date = value; Validate(nameof(Date), value); }
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


        public ICommand EditPatientRecordCommand { get; }
        public ICommand PatientRecordCommand { get; }
        public AddPatientRecordVM(Patient patient)
        {
            patientInstan=patient;
            _context = new DataContext();
            EditPatientRecordCommand = new RelayCommand(EditPatientRecord, CanSubmit);
            PatientRecordCommand = new RelayCommand(NavigateToEditPatientRecordPage);
            patientName=patient.Name;
            Account a = (Account)Application.Current.Properties["User"];
            DoctorName = a.Name;
            Date= DateTime.Today;



        }
        private void EditPatientRecord(object parameter)
        {
            try
            {
                // Kiểm tra các trường thông tin cần thiết trước khi lưu vào cơ sở dữ liệu
                var newPatient = new Patient_Record
                {
                    Symptoms = this.Symptoms,
                    Diagnosis = this.Diagnosis,
                    Date = this.Date,
                    Disease = this.Disease,
                    PatientId = patientInstan.Id
                };
                Account a = (Account)Application.Current.Properties["User"];
                newPatient.DoctorId = a.Id;



                _context.Patient_Records.Add(newPatient);
                _context.SaveChanges();
                MessageBox.Show("Patient added successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error patient record: {ex.Message}");

            }

        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

        }
        private void NavigateToEditPatientRecordPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Patient/ViewPatients.xaml", UriKind.Relative));
        }
    }
}
