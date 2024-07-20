using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.Views.Admin;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

using static ProjectClinicManagement.Models.Medicine;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    class EditMedicineVM : BaseViewModel, INotifyDataErrorInfo
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



            EditMedicineCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }
        private Medicine _medicine;
        public Medicine Medicine
        {
            get { return (Medicine)_medicine; }
            set
            {
                _medicine = value;
                OnPropertyChanged();
            }
        }
        private int id;
        private string name;
        private string atcCode;
        private string genericName;
        private string description;
        private string manufacturer;
        private string type;
        private string category;
        private string unit;
        private float price;
        private int quantity;
        private string status;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); Validate(nameof(Name), value); }
        }

        [Required(ErrorMessage = "ATCCode is required.")]
        public string ATCCode
        {
            get => atcCode;
            set { atcCode = value; OnPropertyChanged(); Validate(nameof(ATCCode), value);  }
        }
        [Required(ErrorMessage = "GenericName is required.")]
        public string GenericName
        {
            get => genericName;
            set { genericName = value; OnPropertyChanged(); Validate(nameof(GenericName), value);  }
        }
        [Required(ErrorMessage = "Description is required.")]
        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); Validate(nameof(Description), value);  }
        }
        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer
        {
            get => manufacturer;
            set { manufacturer = value; OnPropertyChanged(); Validate(nameof(Manufacturer), value); }
        }
        [Required(ErrorMessage = "Type is required.")]
        public string Type
        {
            get => type;
            set { type = value; OnPropertyChanged(); Validate(nameof(Type), value); }
        }
        [Required(ErrorMessage = "Category is required.")]
        public string Category
        {
            get => category;
            set { category = value; OnPropertyChanged(); Validate(nameof(Category), value);}
        }
        [Required(ErrorMessage = "Unit is required.")]
        public string Unit
        {
            get => unit;
            set { unit = value; OnPropertyChanged(); Validate(nameof(Unit), value);}
        }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public float Price
        {
            get => price;
            set { price = value; OnPropertyChanged(); Validate(nameof(Price), value);}
        }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(); Validate(nameof(Quantity), value);}
        }

        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }
        public EditMedicineVM(Medicine medicine)
        {

            Medicine = medicine;

            Name = Medicine.Name;
            ATCCode = Medicine.ATCCode;
            GenericName = Medicine.GenericName;
            Description = Medicine.Description;
            Manufacturer = Medicine.Manufacturer;
            Type = Medicine.Type;
            Category = Medicine.Category;
            Unit = Medicine.Unit;
            Price = Medicine.Price;
            Quantity = Medicine.Quantity;
            Status = Medicine.Status.ToString();

            _context = new DataContext();
            EditMedicineCommand = new RelayCommand(EditMedicine, CanSubmit);
            ChangeStatusCommand = new RelayCommand(ChangeStatusMedicine);
        }
        private readonly DataContext _context;
        public ICommand EditMedicineCommand { get; }
        public ICommand ChangeStatusCommand { get; }

        private void ChangeStatusMedicine(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Do you want change?", "Confirm ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (Medicine.Status == StatusType.Active)
                {
                    Medicine.Status = StatusType.Inactive;

                }
                else
                {
                    Medicine.Status = StatusType.Active;
                }

                // Cập nhật Status trong ViewModel
                Status = Medicine.Status.ToString();
                // Optionally, save changes to the database
                _context.Medicines.Update(Medicine);
                _context.SaveChanges(); // Assuming _context is your DbContext instance

                MessageBox.Show("Update successfully.");
            }
        }

        private void EditMedicine(object parameter)
        {
            try {

                if (string.IsNullOrWhiteSpace(Name))
                {
                    MessageBox.Show("Name is required.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ATCCode))
                {
                    MessageBox.Show("ATC Code is required.");
                    return;
                }

                if (Price <= 0)
                {
                    MessageBox.Show("Price must be a positive value.");
                    return;
                }

                if (Quantity < 0)
                {
                    MessageBox.Show("Quantity cannot be negative.");
                    return;
                }

                

                Medicine.Name = Name;
                Medicine.ATCCode = ATCCode;
                Medicine.GenericName = GenericName;
                Medicine.Description = Description;
                Medicine.Manufacturer = Manufacturer;
                Medicine.Type = Type;
                Medicine.Category = Category;
                Medicine.Unit = Unit;
                Medicine.Price = Price;
                Medicine.Quantity = Quantity;
               
                // Lưu thay đổi vào cơ sở dữ liệu
                _context.Medicines.Update(Medicine);
                _context.SaveChanges();

                MessageBox.Show("Medicine updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing Medicine: {ex.Message}");
            }
        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

        }
    }
}
