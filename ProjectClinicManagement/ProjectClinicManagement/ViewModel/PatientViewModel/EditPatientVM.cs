using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ProjectClinicManagement.ViewModel.DoctorViewModel;
using ProjectClinicManagement.ViewModel.Common;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    internal class EditPatientVM: BaseViewModel
    {
        private int id;
        private string name;
        private int age;
        private double weight;
        private double height;
        private string phone;
        private string email;
        private string address;
        private string emergency;
        private Patient.StatusType status;
        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }
        public int Age
        {
            get => age;
            set { age = value; OnPropertyChanged(); }
        }
        public double Weight
        {
            get => weight;
            set { weight = value; OnPropertyChanged(); }
        }
        public double Height
        {
            get => height;
            set { height = value; OnPropertyChanged(); }
        }
        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }
        public string Address
        {
            get => address;
            set { address = value; OnPropertyChanged(); }
        }
        public string Emergency
        {
            get => emergency;
            set { emergency = value; OnPropertyChanged(); }
        }
        public Patient.StatusType Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }
        private readonly DataContext _context;

        public ICommand SaveChangesCommand { get; }

        public EditPatientVM(int patientId)
        {
            _context = new DataContext();
            _context = new DataContext();
            var patient = _context.Medicines.Find(patientId);
            if(patient != null) 
            {
                Name = this.Name;
                Id = this.Id;
                Address = this.Address;
                Age = this.Age;
                Email = this.Email;
                Phone = this.Phone;
                Emergency = this.Emergency;
                Height = this.Height;
                Weight = this.Weight;
                Status = this.Status;
            };
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }
        private void SaveChanges(object parameter)
        {
            var patient = _context.Patients.Find(Id);
            if (patient != null)
            {
                patient.Weight = this.Weight;
                patient.Status = this.Status;
                patient.Name = this.Name;
                patient.Address = this.Address;
                patient.Age = this.Age;
                patient.Email = this.Email;
                patient.Phone = this.Phone;
                patient.Height = this.Height;
                patient.Emergency = this.Emergency;
                _context.SaveChanges();
                MessageBox.Show("Changes saved successfully");
            }
            else
            {
                MessageBox.Show("Patient not found");
            }
        }

    }
}
