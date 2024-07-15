using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    class EditMedicineVM : BaseViewModel
    {
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
        private Medicine.StatusType status;

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

        public ICommand SaveChangesCommand { get; }

        public EditMedicineVM(int medicineId)
        {
            _context = new DataContext();
            var medicine = _context.Medicines.Find(medicineId);

            if (medicine != null)
            {
                Id = medicine.Id;
                Name = medicine.Name;
                ATCCode = medicine.ATCCode;
                GenericName = medicine.GenericName;
                Description = medicine.Description;
                Manufacturer = medicine.Manufacturer;
                Type = medicine.Type;
                Category = medicine.Category;
                Unit = medicine.Unit;
                Price = medicine.Price;
                Quantity = medicine.Quantity;
                Status = medicine.Status;
            }

            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        private void SaveChanges(object parameter)
        {
            var medicine = _context.Medicines.Find(Id);
            if (medicine != null)
            {
                medicine.Name = this.Name;
                medicine.ATCCode = this.ATCCode;
                medicine.GenericName = this.GenericName;
                medicine.Description = this.Description;
                medicine.Manufacturer = this.Manufacturer;
                medicine.Type = this.Type;
                medicine.Category = this.Category;
                medicine.Unit = this.Unit;
                medicine.Price = this.Price;
                medicine.Quantity = this.Quantity;
                medicine.Status = this.Status;

                _context.SaveChanges();
                MessageBox.Show("Changes saved successfully");
            }
            else
            {
                MessageBox.Show("Medicine not found");
            }
        }
    }
}
