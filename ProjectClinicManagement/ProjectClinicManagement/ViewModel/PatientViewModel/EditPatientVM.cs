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
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    internal class EditPatientVM: BaseViewModel,INotifyDataErrorInfo
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



            EditPatientCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }

        private string name;
        private int age;
        private double weight;
        private double height;
        private string phone;
        private string email;
        private string address;
        private string emergency;
        private string status;


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
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Phone Number is 10 or 11")]
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
        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }
        private Patient _patient;
        public Patient Patient
        {
            get { return (Patient)_patient; }
            set
            {
                _patient = value;
                OnPropertyChanged();
            }
        }


        private readonly DataContext _context;

        public ICommand EditPatientCommand { get; }
        public ICommand ChangeStatusCommand { get; }

        public EditPatientVM(Patient patient)
        {
            _context = new DataContext();
            EditPatientCommand = new RelayCommand(EditPatient, CanSubmit);
            ChangeStatusCommand = new RelayCommand(ChangeStatusPatient);

            Patient = patient;
            Name = Patient.Name;
            Address = Patient.Address;
            Age = Patient.Age;
            Email = Patient.Email;
            Phone = Patient.Phone;
            Emergency = Patient.Emergency;
            Height = Patient.Height;
            Weight = Patient.Weight;
            status = Patient.Status.ToString();

            
        }
        private void ChangeStatusPatient(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Do you want change?", "Confirm ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (Patient.Status == Patient.StatusType.Active)
                {
                    Patient.Status = Patient.StatusType.Inactive;

                }
                else
                {
                    Patient.Status = Patient.StatusType.Active;
                }

                // Cập nhật Status trong ViewModel
                Status = Patient.Status.ToString();
                // Optionally, save changes to the database
                _context.Patients.Update(Patient);
                _context.SaveChanges(); // Assuming _context is your DbContext instance

                MessageBox.Show("Update successfully.");
            }
        }

        private void EditPatient(object parameter)
        {
            try
            {
                // Kiểm tra các trường thông tin cần thiết trước khi lưu vào cơ sở dữ liệu
                
                Patient.Name = Name;
                Patient.Address = Address;
                Patient.Age = Age;
                Patient.Email = Email;
                Patient.Phone = Phone;
                Patient.Emergency = Emergency;
                Patient.Height = Height;
                Patient.Weight = Weight;


                _context.Patients.Update(Patient);
                _context.SaveChanges();
                MessageBox.Show("Patient updated successfully");
            }catch(Exception ex)
            {
                MessageBox.Show($"Error editing patient: {ex.Message}");

            }
        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

        }

    }
}
