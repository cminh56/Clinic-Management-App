using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.Windows.Controls;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    class AddMedicineVM : BaseViewModel, INotifyDataErrorInfo
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


            AddMedicineCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }

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
        private Medicine.StatusType status;

        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get => name;
            set { name = value; Validate(nameof(Name), value); }
        }

        [Required(ErrorMessage = "ATCCode is required.")]
        public string ATCCode
        {
            get => atcCode;
            set { atcCode = value; Validate(nameof(ATCCode), value); }
        }

        [Required(ErrorMessage = "GenericName is required.")]
        public string GenericName
        {
            get => genericName;
            set { genericName = value; Validate(nameof(GenericName), value); }
        }

        [Required(ErrorMessage = "Description is required.")]
        public string Description
        {
            get => description;
            set { description = value; Validate(nameof(Description), value); }
        }

        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer
        {
            get => manufacturer;
            set { manufacturer = value; Validate(nameof(Manufacturer), value); }
        }

        [Required(ErrorMessage = "Type is required.")]
        public string Type
        {
            get => type;
            set { type = value; Validate(nameof(Type), value); }
        }

        [Required(ErrorMessage = "Category is required.")]
        public string Category
        {
            get => category;
            set { category = value; Validate(nameof(Category), value); }
        }

        [Required(ErrorMessage = "Unit is required.")]
        public string Unit
        {
            get => unit;
            set { unit = value; Validate(nameof(Unit), value); }
        }


        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public float Price
        {
            get => price;
            set { price = value; Validate(nameof(Price), value); }
        }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]

        public int Quantity
        {
            get => quantity;
            set { quantity = value; Validate(nameof(Quantity), value); }
        }

        public Medicine.StatusType Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }

        private readonly DataContext _context;

        public ICommand AddMedicineCommand { get; }

        public AddMedicineVM()
        {
            _context = new DataContext();
            AddMedicineCommand = new RelayCommand(AddMedicine, CanSubmit);
        }

        private void AddMedicine(object parameter)
        {
            var newMedicine = new Medicine
            {
                Name = this.Name,
                ATCCode = this.ATCCode,
                GenericName = this.GenericName,
                Description = this.Description,
                Manufacturer = this.Manufacturer,
                Type = this.Type,
                Category = this.Category,
                Unit = this.Unit,
                Price = this.Price,
                Quantity = this.Quantity,
                Status = Medicine.StatusType.Active
            };

            _context.Medicines.Add(newMedicine);
            _context.SaveChanges();
            MessageBox.Show("Medicine added successfully");
        }

        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

        }
    }


}
