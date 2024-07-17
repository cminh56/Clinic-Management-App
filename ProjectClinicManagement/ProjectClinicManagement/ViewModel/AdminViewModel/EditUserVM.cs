using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ProjectClinicManagement.Models.Account;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class EditUserVM : BaseViewModel, INotifyDataErrorInfo
    {
        //Validation
        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];

            }
            else
            {
                return Enumerable.Empty<string>();
            }

        }

        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);


            if (results.Any())
            {

                // Check if propertyName already exists in Errors
                if (Errors.ContainsKey(propertyName))
                {
                    // Update existing errors for propertyName
                    Errors[propertyName] = results.Select(r => r.ErrorMessage).ToList();
                }
                else
                {
                    // Add new entry for propertyName
                    Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }



            EditUserCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }
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

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                Validate(nameof(Email), value);
            }
        }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                Validate(nameof(UserName), value);
            }
        }

        [Required(ErrorMessage = "Name is required.")]

        public string Name
        {
            get => name;
            set
            {
                name = value;
                Validate(nameof(Name), value);
            }
        }

        [Required(ErrorMessage = "Date of birth is required.")]
        [Range(typeof(DateTime), "1/1/1800", "12/31/9999", ErrorMessage = "Date of birth must be after 1800.")]
        public DateTime Dob
        {
            get => dob;
            set
            {
                dob = value;
                Validate(nameof(Dob), value);
            }
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


        [Range(0, double.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        public decimal Salary
        {
            get => salary;
            set
            {
                salary = value;
                Validate(nameof(Salary), value);
            }
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
            EditUserCommand = new RelayCommand(EditUser, CanSubmit);
            ChangeStatusCommand = new RelayCommand(ChangeStatusUser); 
        }
        private void ChangeStatusUser(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Do you want change?", "Confirm ", MessageBoxButton.YesNo, MessageBoxImage.Question);
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

        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null ) && Errors.Count == 0;

        }

    }
}
