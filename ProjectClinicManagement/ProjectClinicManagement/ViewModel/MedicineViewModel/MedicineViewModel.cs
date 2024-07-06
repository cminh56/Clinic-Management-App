using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.Views.Medicine;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModels.MedicineViewModel
{
    public class MedicineViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Medicine> _medicines;
        public ObservableCollection<Medicine> Medicines
        {
            get => _medicines;
            set
            {
                _medicines = value;
                OnPropertyChanged();
            }
        }

        public Medicine SelectedMedicine { get; set; }

        public ICommand RefreshCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MedicineViewModel()
        {
            RefreshCommand = new RelayCommand(param => LoadMedicines());
            AddCommand = new RelayCommand(param => OpenAddMedicineWindow());
            EditCommand = new RelayCommand(param => Edit(), CanEdit);
            DeleteCommand = new RelayCommand(param => Delete(), CanDelete);

            LoadMedicines();
        }

       public void LoadMedicines()
        {
            using (var context = new DataContext())
            {
                Medicines = new ObservableCollection<Medicine>(context.Medicines.ToList());
            }
        }

        private void OpenAddMedicineWindow()
        {
            var addMedicineViewModel = new AddMedicineViewModel(this); // Pass MedicineViewModel instance
            var addMedicineWindow = new AddMedicineWindow
            {
                DataContext = addMedicineViewModel
            };

            addMedicineWindow.ShowDialog();
        }

        private void Edit()
        {
            if (SelectedMedicine != null)
            {
                // Open the EditMedicineWindow for editing the selected medicine
                var editMedicineViewModel = new EditMedicineViewModel(this)
                {
                    SelectedMedicine = SelectedMedicine // Pass the selected medicine to the EditMedicineViewModel
                };

                var editMedicineWindow = new EditMedicineWindow
                {
                    DataContext = editMedicineViewModel
                };

                editMedicineWindow.ShowDialog(); // Show the EditMedicineWindow as a dialog
            }
        }


        private void Delete()
        {
            if (SelectedMedicine != null)
            {
                using (var context = new DataContext())
                {
                    context.Medicines.Remove(SelectedMedicine);
                    context.SaveChanges();
                }

                LoadMedicines();
            }
        }

        private bool CanEdit(object obj)
        {
            return SelectedMedicine != null;
        }

        private bool CanDelete(object obj)
        {
            return SelectedMedicine != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
