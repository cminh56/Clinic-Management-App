using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Helper;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.Views.Admin;
using ProjectClinicManagement.Views.Doctor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    internal class AddPatientVM : BaseViewModel, INotifyDataErrorInfo
    {
        //Validation
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
            var results = new List<ValidationResult>();

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



            AddPatientCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }

        private string name ;
        private int age ;
        private double weight ;
        private double height ; 
        private string phone ;
        private string email ;
        private string address ;
        private string emergency ;
       

        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get { return name; }
            set { name = value; Validate(nameof(Name), value); }
        }
        [Required(ErrorMessage = "Age is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be greater than 0.")]
        public int Age
        {
            get { return age; }
            set { age = value; Validate(nameof(Age), value); }
        }
        [Required(ErrorMessage = "Weight is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double Weight
        {
            get { return weight; }
            set { weight = value; Validate(nameof(Weight), value); }
        }
        [Required(ErrorMessage = "Height is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Height must be greater than 0.")]
        public double Height
        {
            get { return height; }
            set { height = value; Validate(nameof(Height), value); }
        }
        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(11, MinimumLength = 10,ErrorMessage ="Phone Number is 10 or 11")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; Validate(nameof(Phone), value); }
        }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email
        {
            get { return email; }
            set { email = value; Validate(nameof(Email), value); }
        }
        [Required(ErrorMessage = "Address is required.")]

        public string Address
        {
            get { return address; }
            set { address = value; Validate(nameof(Address), value); }
        }
        [Required(ErrorMessage = "Emergency is required.")]

        public string Emergency
        {
            get { return emergency; }
            set { emergency = value; Validate(nameof(Email), value); }
        }
       

       
        private readonly DataContext _context;

        public ICommand AddPatientCommand { get; }

        public AddPatientVM()
        {
            _context = new DataContext();
            AddPatientCommand = new RelayCommand(AddPatient,CanSubmit);
        }
       
        private void AddPatient(object parameter)
        {
            var newPatient = new Patient
            {
                Name = this.Name,
                Address = this.Address,
                Age = this.Age,
                Email = this.Email,
                Phone = this.Phone,
                Emergency = this.Emergency,
                Height = this.Height,
                Weight = this.Weight,
                Status = Patient.StatusType.Active,
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();
            MessageBox.Show("Patient added successfully");
        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

        }
    }
}
