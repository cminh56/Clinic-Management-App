using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        public ICommand AddUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }
        

        private readonly DataContext _context;
        public NavigationService _navigationService;

        public UserVM()
        {
            _context = new DataContext();


            // Initialize list Accounts
            Accounts = new List<Account>(_context.Account);

            // Initialize list (command)
            AddUserCommand = new RelayCommand(NavigateToAddUserPage);
            UpdateUserCommand = new RelayCommand(NavigateToUpdateUser);
          
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
       
    }
}
