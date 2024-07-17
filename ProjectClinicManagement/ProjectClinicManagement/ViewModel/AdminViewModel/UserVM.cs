using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class UserVM : BaseViewModel
    {
        //This variable is saved when switching to other pages
        public static Account accountInstan;
       
        //Declare list account
        private List<Account> _accounts;
        public List<Account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }
        //Declarec account
        private Account _account;
        public Account Account
        {
            get { return (Account)_account; }
            set
            {
                _account = value;
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
        private string _selectedRole;
        public string SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                OnPropertyChanged();

                // Cập nhật màu BorderBrush của các Button
                UpdateButtonBorderBrush();
            }
        }
        private List<Button> _roleButtons;
        public List<Button> RoleButtons
        {
            get { return _roleButtons; }
            set
            {
                _roleButtons = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }
        public ICommand FilterByRoleCommand { get; set; }


        private readonly DataContext _context;
        public NavigationService _navigationService;

        public UserVM()
        {
            _context = new DataContext();


            // Initialize list Accounts
            Accounts = new List<Account>(_context.Account);
            placeHolderText = "Search by name, email,...";
            // Initialize list (command)
            AddUserCommand = new RelayCommand(NavigateToAddUserPage);
            UpdateUserCommand = new RelayCommand(NavigateToUpdateUser);
            FilterByRoleCommand = new RelayCommand(Filter);
            // Initialize RoleButtons
            RoleButtons = new List<Button>
    {
        new Button { Content = "Admin", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Doctor", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Nurse", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "Receipter", Style = Application.Current.FindResource("tabButton") as Style },
        new Button { Content = "All", Style = Application.Current.FindResource("tabButton") as Style }


    };
            SelectedRole = "All";

        }
        private void NavigateToAddUserPage(object parameter)
        {
            _navigationService.Navigate(new Uri("Views/Admin/AddUser.xaml", UriKind.Relative));

        }
        private void NavigateToUpdateUser(object parameter)
        {

            // Pass the account as a parameter
            if (_account != null)
            {
                accountInstan = Account;
                _navigationService.Navigate(new Uri($"Views/Admin/EditUser.xaml", UriKind.Relative));
            }



        }
        private void ApplyFilters()
        {
            // Filter by search text  if applicable
            if (string.IsNullOrEmpty(SearchText))
            {
                Accounts = new List<Account>(_context.Account);
            }
            else
            {
                Accounts = new List<Account>(_context.Account
                    .Where(x => x.Email.Contains(SearchText) || x.UserName.Contains(SearchText) || x.Name.Contains(SearchText)));
            }
        }

        //Filter by role
        private void Filter(object parameter)
        {
            string role = parameter as string;
            if (role == "All")
            {
                SelectedRole = "All"; // Không có vai trò nào được chọn
                Accounts = new List<Account>(_context.Account);
            }
            else if (Enum.TryParse<Account.RoleType>(role, out var roleType))
            {
                SelectedRole = role; // Cập nhật vai trò được chọn
                Accounts = new List<Account>(_context.Account
                    .Where(a => a.Role == roleType));
            }
            else
            {
                SelectedRole = null; // Trường hợp không hợp lệ
                ApplyFilters(); // Áp dụng lại bộ lọc nếu không có vai trò được chọn
            }
        }
        private void UpdateButtonBorderBrush()
        {
            foreach (var button in RoleButtons)
            {
                if (button.Content.ToString() == SelectedRole)
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
