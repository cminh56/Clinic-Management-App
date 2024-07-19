using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
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
    public class ChangePassVM
    {
        private readonly DataContext context;
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



            ChangePassCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };


        }
        //End
        private string userName;
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
        private string newpassword;
        [Required(ErrorMessage = "New Password is required.")]
        public string Newpassword
        {
            get { return newpassword; }
            set
            {
                newpassword = value;
                Validate(nameof(Newpassword), value);
            }
        }
        private string confirmPassWord;
        [Required(ErrorMessage = "Confirm PassWord is required.")]
        public string ConfirmPassWord
        {
            get { return confirmPassWord; }
            set
            {
                confirmPassWord = value;
                Validate(nameof(ConfirmPassWord), value);
            }
        }

        public ICommand ChangePassCommand { get; set; }
        public ChangePassVM()
        {
            context = new DataContext();
            ChangePassCommand = new RelayCommand(ChangePasss, CanSubmit);
        }
        private void ChangePasss(object obj)
        {
            if (Newpassword != ConfirmPassWord)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Implement logic to change password securely
            try
            {
                // Example logic (replace with your actual password change logic):
                var user = context.Account.FirstOrDefault(u => u.UserName == UserName);
                if (user != null)
                {
                    // Update user's password (ensure to hash the new password securely)
                    user.Password = BCrypt.Net.BCrypt.HashPassword(Newpassword);
                    context.SaveChanges();

                    MessageBox.Show("Password changed successfully.");
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error changing password: " + ex.Message);
            }
        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

        }
    }
}
