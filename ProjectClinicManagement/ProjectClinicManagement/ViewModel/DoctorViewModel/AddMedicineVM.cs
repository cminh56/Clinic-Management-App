using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    class AddMedicineVM : BaseViewModel
    {
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

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string ATCCode
        {
            get => atcCode;
            set { atcCode = value; OnPropertyChanged(); }
        }

        public string GenericName
        {
            get => genericName;
            set { genericName = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        public string Manufacturer
        {
            get => manufacturer;
            set { manufacturer = value; OnPropertyChanged(); }
        }

        public string Type
        {
            get => type;
            set { type = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => category;
            set { category = value; OnPropertyChanged(); }
        }

        public string Unit
        {
            get => unit;
            set { unit = value; OnPropertyChanged(); }
        }

        public float Price
        {
            get => price;
            set { price = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(); }
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
            AddMedicineCommand = new RelayCommand(AddMedicine);
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
                Status = this.Status
            };

            _context.Medicines.Add(newMedicine);
            _context.SaveChanges();
            MessageBox.Show("Medicine added successfully");
        }
    }
}
