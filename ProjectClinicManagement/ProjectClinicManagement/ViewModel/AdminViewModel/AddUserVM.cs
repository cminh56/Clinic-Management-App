using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Helper;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class AddUserVM : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly SendEmail send = new SendEmail();
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



            AddUserCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }
        //End
        private string email;
        private string userName;
        private string name;
        private string password;
        private DateTime dob;
        private Account.GenderType gender;
        private Account.RoleType role;

        private decimal salary;

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

        public Account.GenderType Gender
        {
            get => gender;
            set
            {
                gender = value;
                Validate(nameof(Gender), value);
            }
        }
        public Account.RoleType Role
        {
            get => role;
            set
            {
                role = value;
                Validate(nameof(Role), value);
            }
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



        private readonly DataContext _context;

        public ICommand AddUserCommand { get; }

        public ICommand BackCommand { get; }

        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }
        public AddUserVM()
        {
            _context = new DataContext();
            AddUserCommand = new RelayCommand(AddUser, CanSubmit);
            BackCommand = new RelayCommand(NavigateToBackPage);
        }

        private void AddUser(object parameter)
        {
            try
            {
                // Check if email or username already exists
                bool emailExists = _context.Account.Any(a => a.Email == this.Email);
                bool usernameExists = _context.Account.Any(a => a.UserName == this.UserName);

                if (emailExists)
                {
                    MessageBox.Show("Email already exists. Please use a different email address.");
                    return;
                }

                if (usernameExists)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                    return;
                }
                //
                string Password = GenerateRandomPassword();
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
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

                //send email
                send.sendEmailCreateUser(newAccount, Password);

                MessageBox.Show("Add and Send Email Successfully ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count ==0 ;

        }
        private string GenerateRandomPassword()
        {
            // Độ dài của mật khẩu ngẫu nhiên
            int length = 8;

            // Các ký tự được phép trong mật khẩu
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=-";

            // Sinh mật khẩu ngẫu nhiên
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void NavigateToBackPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Admin/ViewUsers.xaml", UriKind.Relative));
        }
    }
  
}
