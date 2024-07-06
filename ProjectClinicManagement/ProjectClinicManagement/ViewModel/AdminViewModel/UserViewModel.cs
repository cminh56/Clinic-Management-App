using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel
{
    class UserViewModel: BaseViewModel
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

        public UserViewModel()
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
      
     
        }
        private void UpdateUser(object parameter) { }
        private bool CanExecuteUserCommand(object parameter)
        {
            return Account != null;
        }
    }
}
