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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class UserVM : BaseViewModel
    {
        //This variable is saved when switching to other pages
        public static Account accountInstan;
        
        public int _currentPage = 1; // Trang hiện tại
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();


            }

        }
        public int _itemsPerPage = 3; // Số mục trên mỗi trang
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
                CurrentPage = 1;
                Filter(SelectedRole);
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
        public ICommand Nextpage { get; set; }
        public ICommand Prepage { get; set; }


        private readonly DataContext _context;
        public NavigationService _navigationService;

        public UserVM()
        {
            _context = new DataContext();


            // Initialize list Accounts
            var query = _context.Account.AsQueryable();
            Accounts = query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();

            placeHolderText = "Search by name, email,...";
            //Cacular total page
            Totalpage = query.ToList().Count / _itemsPerPage == 0 ? 1 : query.ToList().Count / _itemsPerPage;

            // Initialize list (command)
            AddUserCommand = new RelayCommand(NavigateToAddUserPage);
            UpdateUserCommand = new RelayCommand(NavigateToUpdateUser);
            FilterByRoleCommand = new RelayCommand(Filter2);
            Nextpage = new RelayCommand(NextPage,CanNextPage);
            Prepage = new RelayCommand(PrePage,CanPrePage);
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

        //Filter by role
        private void Filter(object parameter)
        {
            string role = parameter as string;
            SelectedRole = role;

            var query = _context.Account.AsQueryable();
         
            if (Enum.TryParse<Account.RoleType>(SelectedRole, out var roleType))
            {
              
                query = query.Where(a => a.Role == roleType);
            }
          
            if (!string.IsNullOrEmpty(SearchText))
            {

                query = query.Where(x => x.Email.Contains(SearchText) 
                || x.UserName.Contains(SearchText) 
                || x.Name.Contains(SearchText));


            }

            Totalpage = query.ToList().Count / _itemsPerPage == 0 ? 1 : query.ToList().Count / _itemsPerPage;

            Accounts = query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        }
        private void Filter2(object parameter)
        {
            string role = parameter as string;
            SelectedRole = role;
            CurrentPage = 1;
            var query = _context.Account.AsQueryable();

            if (Enum.TryParse<Account.RoleType>(SelectedRole, out var roleType))
            {

                query = query.Where(a => a.Role == roleType);
            }

            if (!string.IsNullOrEmpty(SearchText))
            {

                query = query.Where(x => x.Email.Contains(SearchText)
                || x.UserName.Contains(SearchText)
                || x.Name.Contains(SearchText));


            }

            Totalpage = query.ToList().Count / _itemsPerPage == 0 ? 1 : query.ToList().Count / _itemsPerPage;

            Accounts = query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
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
        // Phân trang - Next Page
        private void NextPage(object parameter)
        {
            CurrentPage++;
            Filter(SelectedRole);
            UpdateButtonBorderBrush();
        }

        // Phân trang - Previous Page
        private void PrePage(object parameter)
        {
            CurrentPage--;
            Filter(SelectedRole);
            UpdateButtonBorderBrush();
        }

        // Kiểm tra có thể đi tới trang tiếp theo không
        private bool CanNextPage(object parameter)
        {
      
            return _currentPage < totalpage;
        }

        // Kiểm tra có thể đi tới trang trước đó không
        private bool CanPrePage(object parameter)
        {
            return _currentPage > 1;
        }


    }
}
