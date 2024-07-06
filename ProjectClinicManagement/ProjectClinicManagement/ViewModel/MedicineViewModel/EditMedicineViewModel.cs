using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModels.MedicineViewModel
{
    public class EditMedicineViewModel : INotifyPropertyChanged
    {
        private Medicine _selectedMedicine;
        private string _name;
        private string _atcCode;
        private string _genericName;
        private string _description;
        private string _manufacturer;
        private string _type;
        private string _category;
        private string _unit;
        private float _price;
        private int _quantity;

        public Medicine SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                _selectedMedicine = value;
                LoadMedicineDetails();
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string ATCCode
        {
            get => _atcCode;
            set
            {
                _atcCode = value;
                OnPropertyChanged();
            }
        }

        public string GenericName
        {
            get => _genericName;
            set
            {
                _genericName = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }

        public float Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; }

        private readonly MedicineViewModel _medicineViewModel;

        public EditMedicineViewModel(MedicineViewModel medicineViewModel)
        {
            _medicineViewModel = medicineViewModel;
            SubmitCommand = new RelayCommand(Submit); // Use a RelayCommand to bind to Submit method
        }

        private void LoadMedicineDetails()
        {
            if (SelectedMedicine != null)
            {
                Name = SelectedMedicine.Name;
                ATCCode = SelectedMedicine.ATCCode;
                GenericName = SelectedMedicine.GenericName;
                Description = SelectedMedicine.Description;
                Manufacturer = SelectedMedicine.Manufacturer;
                Type = SelectedMedicine.Type;
                Category = SelectedMedicine.Category;
                Unit = SelectedMedicine.Unit;
                Price = SelectedMedicine.Price;
                Quantity = SelectedMedicine.Quantity;
            }
        }

        private void Submit(object parameter)
        {
            using (var context = new DataContext())
            {
                // Update the selected medicine
                var medicineToUpdate = context.Medicines.Find(SelectedMedicine.Id);
                if (medicineToUpdate != null)
                {
                    medicineToUpdate.Name = Name;
                    medicineToUpdate.ATCCode = ATCCode;
                    medicineToUpdate.GenericName = GenericName;
                    medicineToUpdate.Description = Description;
                    medicineToUpdate.Manufacturer = Manufacturer;
                    medicineToUpdate.Type = Type;
                    medicineToUpdate.Category = Category;
                    medicineToUpdate.Unit = Unit;
                    medicineToUpdate.Price = Price;
                    medicineToUpdate.Quantity = Quantity;

                    context.SaveChanges();
                }
            }

            _medicineViewModel.LoadMedicines(); // Refresh medicines collection

            // Optionally, close the window or reset form fields
            MessageBox.Show("Medicine updated successfully!");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
