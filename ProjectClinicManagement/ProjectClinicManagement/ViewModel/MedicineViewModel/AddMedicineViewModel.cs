using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModels.MedicineViewModel
{
    public class AddMedicineViewModel : INotifyPropertyChanged
    {
        
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

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string ATCCode
        {
            get => _atcCode;
            set { _atcCode = value; OnPropertyChanged(); }
        }

        public string GenericName
        {
            get => _genericName;
            set { _genericName = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set { _manufacturer = value; OnPropertyChanged(); }
        }

        public string Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(); }
        }

        public float Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public ICommand SubmitCommand { get; }

     
        private readonly MedicineViewModel _medicineViewModel;
        public AddMedicineViewModel(MedicineViewModel medicineViewModel)
        {
            _medicineViewModel = medicineViewModel;
            SubmitCommand = new RelayCommand(param => AddMedicine());
        }
        private void AddMedicine()
        {
            using (var context = new DataContext())
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
                    Status = Medicine.StatusType.Active // Assuming default status
                };

                context.Medicines.Add(newMedicine);
                context.SaveChanges();
            }


            
            MessageBox.Show("Medicine added successfully.");
           
            ClearFields();
            _medicineViewModel.LoadMedicines();
        }

        private void ClearFields()
        {
            Name = "";
            ATCCode = "";
            GenericName = "";
            Description = "";
            Manufacturer = "";
            Type = "";
            Category = "";
            Unit = "";
            Price = 0.0f;
            Quantity = 0;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
