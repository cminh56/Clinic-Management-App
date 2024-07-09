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
        private string name;
        private string password;    
        private DateTime dob;
        private Account.GenderType gender;
        private Account.RoleType role;
       
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
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public DateTime Dob
        {
            get => dob;
            set { dob = value; OnPropertyChanged(); }
        }

        public Account.GenderType Gender
        {
            get => gender;
            set { gender = value; OnPropertyChanged(); }
        }
        public Account.RoleType Role
        {
            get => role;
            set { role = value; OnPropertyChanged(); }
        }



        public decimal Salary
        {
            get => salary;
            set { salary = value; OnPropertyChanged(); }
        }
        


        private readonly DataContext _context;

        public ICommand AddUserCommand { get; }
        public AddUserVM()
        {
            _context = new DataContext();
            AddUserCommand = new RelayCommand(AddUser);
        }

        private void AddUser(object parameter)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(this.Password);
            var newAccount = new Account
            {
                Email = this.Email,
                UserName = this.UserName,
                Name = this.Name,
                Gender = this.Gender,
                Dob = this.Dob,
                Password = hashedPassword,
                Role = this.Role,
                Status = Account.StatusType.Active,
                Salary = this.Salary,
               JoinDate = DateTime.Now,
            };

            _context.Account.Add(newAccount);
            _context.SaveChanges();
            MessageBox.Show("Add ok");
        }
    }
}
