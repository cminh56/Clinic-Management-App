using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ProjectClinicManagement.Models.Account;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class EditUserVM : BaseViewModel
    {
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
        private string email;
        private string userName;
        private string name;
        private DateTime dob;
        private int gender;
        private int role;
        private string status;
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

        public int Gender
        {
            get => gender;
            set { gender = value; OnPropertyChanged(); }
        }
        public int Role
        {
            get => role;
            set { role = value; OnPropertyChanged(); }
        }

        public string Status
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

        public ICommand EditUserCommand { get; }
        public ICommand ChangeStatusCommand { get; }

        public EditUserVM(Account account)
        {
            
            Account = account;

            email = Account.Email;
            userName = Account.UserName;
            name = Account.Name;
            dob = Account.Dob;
            gender = (int)Account.Gender;
            role = (int)Account.Role;
            status = Account.Status.ToString();
            salary = Account.Salary;

            _context = new DataContext();
            EditUserCommand = new RelayCommand(EditUser);
            ChangeStatusCommand = new RelayCommand(ChangeStatusUser); 
        }
        private void ChangeStatusUser(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Do you want change?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (Account.Status == StatusType.Active)
                {
                    Account.Status = StatusType.Inactive;
                   
                }
                else
                {
                    Account.Status = StatusType.Active;
                }

                // Cập nhật Status trong ViewModel
                Status = Account.Status.ToString();
                // Optionally, save changes to the database
                _context.Account.Update(Account);
                _context.SaveChanges(); // Assuming _context is your DbContext instance
                 
                MessageBox.Show("Update successfully.");
            }
        }
        private void EditUser(object parameter)
        {
            try
            {
                // Kiểm tra các trường thông tin cần thiết trước khi lưu vào cơ sở dữ liệu
                if (string.IsNullOrEmpty(Account.Email) || string.IsNullOrEmpty(Account.UserName))
                {
                    MessageBox.Show("Email and Username are required fields.");
                    return;
                }

                // Cập nhật thông tin từ các thuộc tính binding vào đối tượng Account
                Account.Email = Email;
                Account.UserName = UserName;
                Account.Name = Name;
                Account.Dob = Dob;
                Account.Gender = (GenderType)Gender; // Ép kiểu từ int sang Gender enum
                Account.Role = (RoleType)Role; // Ép kiểu từ int sang RoleType enum
                Account.Salary = Salary;
                Account.JoinDate = JoinDate;


                // Lưu thay đổi vào cơ sở dữ liệu
                _context.Account.Update(Account);
                _context.SaveChanges();

                MessageBox.Show("Account updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing account: {ex.Message}");
            }
        }

    }
}
