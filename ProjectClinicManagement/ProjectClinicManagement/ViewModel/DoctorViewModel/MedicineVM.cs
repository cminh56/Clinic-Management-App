using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using static ProjectClinicManagement.Models.Medicine;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class MedicineVM : BaseViewModel
    {
        public static Medicine medicineInstan;
        private List<Medicine> _medicines;
        public List<Medicine> Medicines
        {
            get { return _medicines; }
            set
            {
                _medicines = value;
                OnPropertyChanged();
            }
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
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                ApplyFilters(); // Thay đổi tên hàm gọi tới
                PlaceHolderText = string.Empty;
            }
        }

        private string placeHolderText;
        public string PlaceHolderText
        {
            get { return placeHolderText; }
            set
            {
                placeHolderText = value;
                OnPropertyChanged();


            }

        }
        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();

                // Cập nhật màu BorderBrush của các Button
                UpdateButtonBorderBrush();
            }
        }
        private List<Button> _categoryButtons;
        public List<Button> CategoryButtons
        {
            get { return _categoryButtons; }
            set
            {
                _categoryButtons = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddMedicineCommand { get; set; }
        public ICommand UpdateMedicineCommand { get; set; }

        public ICommand DeleteMedicineCommand { get; set; }
        public ICommand FilterByCategoryCommand { get; set; }
        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }

        private readonly DataContext _context;

        public MedicineVM()
        {
            _context = new DataContext();
            LoadMedicines();
            placeHolderText = "Search by Name";
            AddMedicineCommand = new RelayCommand(NavigateToAddMedicinePage);
            UpdateMedicineCommand = new RelayCommand(NavigateToEditMedicinePage);
            DeleteMedicineCommand = new RelayCommand(DeleteMedicine);
            FilterByCategoryCommand = new RelayCommand(Filter);
            CategoryButtons = new List<Button>
    {
        new Button { Content = "Analgesics", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Antipyretics", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Anti-inflammatory", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Antibiotics", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "All", Style = Application.Current.FindResource("tabButton") as Style }

         
    };
            SelectedCategory = "All";
        }

        private void NavigateToAddMedicinePage(object parameter)
        {
            NavigationService.Navigate(new Uri("Views/Doctor/AddMedicine.xaml", UriKind.Relative));
        }

        private void NavigateToEditMedicinePage(object parameter)
        {
            if (_medicine != null)
            {
                medicineInstan = Medicine;
                NavigationService.Navigate(new Uri("Views/Doctor/EditMedicine.xaml", UriKind.Relative));
            }
        }
        private void LoadMedicines()
        {
            Medicines = new List<Medicine>(_context.Medicines.Where(m => m.Status != Medicine.StatusType.Inactive));
        }

        private void DeleteMedicine(object parameter)
        {
            if (_medicine != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this medicine?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var medicineToDelete = _context.Medicines.Find(_medicine.Id);

                    if (medicineToDelete != null)
                    {
                        medicineToDelete.Status = Medicine.StatusType.Inactive;
                        _context.SaveChanges();
                        OnPropertyChanged(nameof(Medicines));
                        LoadMedicines();
                    }
                }
            }


        }
        private void ApplyFilters()
        {
            // Filter by search text  if applicable
            if (string.IsNullOrEmpty(SearchText))
            {
                Medicines = new List<Medicine>(_context.Medicines.ToList());
            }
            else
            {
                Medicines = new List<Medicine>(_context.Medicines
                    .Where(x => x.Name.Contains(SearchText) || x.GenericName.Contains(SearchText)));
                    
            }
        }

        //Filter by role
        private void Filter(object parameter)
        {
            string category = parameter as string;
            
            if (category == "All")
            {
                SelectedCategory = "All"; // Không có vai trò nào được chọn
                Medicines = new List<Medicine>(_context.Medicines.ToList());
            }
            else if (!string.IsNullOrEmpty(category))
            {
                SelectedCategory = category; // Cập nhật vai trò được chọn
                Medicines = new List<Medicine>(_context.Medicines.Where(a => a.Category == category).ToList());
            }
            else
            {
                SelectedCategory = null; // Trường hợp không hợp lệ
                ApplyFilters(); // Áp dụng lại bộ lọc nếu không có vai trò được chọn
            }
        }
        private void UpdateButtonBorderBrush()
        {
            foreach (var button in CategoryButtons)
            {
                if (button.Content.ToString() == SelectedCategory)
                {
                    button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#784ff2"));
                }
                else
                {
                    // Thiết lập màu BorderBrush mặc định cho các Button khác
                    button.ClearValue(Button.BorderBrushProperty);
                }
            }
        }
    }
}
