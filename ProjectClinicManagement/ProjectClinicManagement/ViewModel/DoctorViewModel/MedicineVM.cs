using Microsoft.Win32;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Helper;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static ProjectClinicManagement.Models.Medicine;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class MedicineVM : BaseViewModel
    {
            private readonly ExelService exelService;
        public int _currentPage = 1; // Trang hiện tại
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();

                LoadMedicines();
            }

        }

        public int _itemsPerPage = 10; // Items per page
        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set
            {
                _itemsPerPage = value;
                OnPropertyChanged();
                LoadMedicines(); // Reload medicines when items per page changes
            }
        }
        public int totalpage;
        public int Totalpage
        {
            get { return totalpage; }
            set
            {
                totalpage = value;
                OnPropertyChanged();


            }

        }

        public ICommand Nextpage { get; set; }
        public ICommand Prepage { get; set; }


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
        public ICommand ExportFileCommand { get; set; }
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
                ApplyFilters();
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
            //Cacular total page

            exelService = new ExelService();




            AddMedicineCommand = new RelayCommand(NavigateToAddMedicinePage);
            UpdateMedicineCommand = new RelayCommand(NavigateToEditMedicinePage);
            DeleteMedicineCommand = new RelayCommand(DeleteMedicine);
            FilterByCategoryCommand = new RelayCommand(Filter);
            Nextpage = new RelayCommand(NextPage, CanNextPage);
            Prepage = new RelayCommand(PreviousPage, CanPreviousPage);
            ExportFileCommand = new RelayCommand(ExportToExel);


            CategoryButtons = new List<Button>
    {
        new Button { Content = "Analgesics", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Antipyretics", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Anti-inflammatory", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Antibiotics", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "All", Style = Application.Current.FindResource("tabButton") as Style }

         
    };
            SelectedCategory = "All";
            LoadMedicines();
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
            var query = _context.Medicines.Where(m => m.Status != Medicine.StatusType.Inactive);
            if (!string.IsNullOrEmpty(SearchText))
            {
                query = query.Where(x => x.Name.Contains(SearchText) || x.GenericName.Contains(SearchText));
            }
            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "All")
            {
                query = query.Where(m => m.Category == SelectedCategory);
            }

            var totalItems = query.Count();
            Totalpage = (totalItems + ItemsPerPage - 1) / ItemsPerPage;

            Medicines = query
                .Skip((CurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToList();
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
            LoadMedicines(); // Reapply the filter and reload medicines
        }

        //Filter by role
        private void Filter(object parameter)
        {
            SelectedCategory = parameter as string;
        }
        private void NextPage(object parameter)
        {
            if (CurrentPage < Totalpage)
            {
                CurrentPage++;
            }
        }

        private bool CanNextPage(object parameter)
        {
            return CurrentPage < Totalpage;
        }

        private void PreviousPage(object parameter)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        private bool CanPreviousPage(object parameter)
        {
            return CurrentPage > 1;
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


        private void ExportToExel(object parameter)
        {
            //get list
            var query = _context.Medicines.ToList();

           
            //Convert
            var data = exelService.ConvertListToExelMedicine(query);

            //Send
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "MedicinesList.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllBytes(saveFileDialog.FileName, data);
                MessageBox.Show("The Excel file has been downloaded successfully.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



    }
}
