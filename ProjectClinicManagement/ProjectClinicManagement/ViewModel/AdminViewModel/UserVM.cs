using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class UserVM: BaseViewModel
    {

        //Khai báo list account
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
        //Khai báo account
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
        public ICommand DeleteUserCommand { get; set; }

        private readonly DataContext _context;
        public  NavigationService _navigationService;

        public UserVM()
        {
            _context = new DataContext();
         

            // Khởi tạo danh sách Accounts
            Accounts = new List<Account>(_context.Account);

            // Khởi tạo các lệnh (command)
            AddUserCommand = new RelayCommand(NavigateToAddUserPage);
            UpdateUserCommand = new RelayCommand(UpdateUser, CanExecuteUserCommand);
        }
        private void NavigateToAddUserPage(object parameter)
        {
            _navigationService.Navigate(new Uri("Views/Admin/AddUser.xaml", UriKind.Relative));

        }
        private void UpdateUser(object parameter) { }
        private bool CanExecuteUserCommand(object parameter)
        {
            return Account != null;
        }
    }
}
