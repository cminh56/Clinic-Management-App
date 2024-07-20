using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.ViewModel.Common;
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
    public class ChangePassVM : BaseViewModel
    {
        private readonly DataContext context;
    
      
        public string UserName { get; set; }

        public string CurrPassword { get; set; }


        public string Newpassword { get; set; }


        public string ConfirmPassWord { get; set; }

        public ICommand ChangePassCommand { get; set; }
        public ChangePassVM()
        {
            context = new DataContext();
            ChangePassCommand = new RelayCommand(ChangePasss);
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
                if (user != null && BCrypt.Net.BCrypt.Verify(CurrPassword, user.Password))
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
       
    }
}
