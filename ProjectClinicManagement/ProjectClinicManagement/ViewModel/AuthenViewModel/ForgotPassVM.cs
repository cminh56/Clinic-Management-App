using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Helper;
using ProjectClinicManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.AuthenViewModel
{
    public class ForgotPassVM
    {
        private readonly DataContext context;
        private readonly SendEmail send ;
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



            SubmitCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }
        //End
        private string email;
       
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

        public ICommand SubmitCommand { get; set; }
        public ForgotPassVM() {
            context = new DataContext();
            send = new SendEmail();
            SubmitCommand = new RelayCommand(SendNewPasss, CanSubmit);
        }

        private void SendNewPasss(object obj)
        {

            string Password = GenerateRandomPassword();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            Account account = context.Account.FirstOrDefault( a => a.Email.Equals(Email));
            if (account != null)
            {
                account.Password = Password;
                context.Update(account);
                context.SaveChanges();

            }
            else
            {
                MessageBox.Show("Email isn't exit");
            }
          
        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

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
    }
}
