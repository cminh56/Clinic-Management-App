using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.Views.Doctor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    internal class AddPatientVM : BaseViewModel
    {
        private int id;
        private string name ;
        private int age ;
        private double weight ;
        private double height ; 
        private string phone ;
        private string email ;
        private string address ;
        private string emergency ;
        private Patient.StatusType status ;
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

        public ICommand AddPatientCommand { get; }

        public AddPatientVM()
        {
            _context = new DataContext();
            AddPatientCommand = new RelayCommand(AddPatient);
        }
        private void AddPatient(object parameter)
        {
            var newPatient = new Patient
            {
                Name = this.Name,
                Id=this.Id,
                Address = this.Address,
                Age = this.Age,
                Email = this.Email,
                Phone = this.Phone,
                Emergency = this.Emergency,
                Height = this.Height,
                Weight = this.Weight,
                Status = this.Status
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();
            MessageBox.Show("Patient added successfully");
        }
    }
}
