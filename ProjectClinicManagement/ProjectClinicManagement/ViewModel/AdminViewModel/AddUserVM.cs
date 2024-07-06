using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
     class AddUserVM : BaseViewModel
    {
        private string email;
        private string userName;
        private string password;
        private Account.RoleType role;
        private Account.StatusType status;
        private decimal salary;
        private DateTime joinDate;

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public string UserName
        {
            get => userName;
            set { userName = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public Account.RoleType Role
        {
            get => role;
            set { role = value; OnPropertyChanged(); }
        }

        public Account.StatusType Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }

        public decimal Salary
        {
            get => salary;
            set { salary = value; OnPropertyChanged(); }
        }

        public DateTime JoinDate
        {
            get => joinDate;
            set { joinDate = value; OnPropertyChanged(); }
        }

        private readonly DataContext _context;

        public ICommand AddAccountCommand { get; }
        public AddUserVM()
        {
            _context = new DataContext();
            AddAccountCommand = new RelayCommand(AddUser);
        }

        private void AddUser(object parameter)
        {
            var newAccount = new Account
            {
                Email = this.Email,
                UserName = this.UserName,
                Password = this.Password,
                Role = this.Role,
                Status = this.Status,
                Salary = this.Salary,
                JoinDate = this.JoinDate
            };

            _context.Account.Add(newAccount);
            _context.SaveChanges();
            MessageBox.Show("Add ok");
        }
    }
}
