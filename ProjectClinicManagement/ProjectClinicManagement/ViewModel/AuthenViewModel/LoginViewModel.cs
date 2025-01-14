﻿using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.ViewModel.Receiptor;
using ProjectClinicManagement.Views;
using ProjectClinicManagement.Views.Admin;
using ProjectClinicManagement.Views.Authentication;
using ProjectClinicManagement.Views.Doctor;
using ProjectClinicManagement.Views.Receiptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.AuthenViewModel
{
    public class LoginViewModel : BaseViewModel

    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; }

        private readonly DataContext _context;
        private readonly Window _loginWindow;

        public LoginViewModel(Window loginWindow)
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
            _context = new DataContext();
            _loginWindow = loginWindow; // Lưu tham chiếu của cửa sổ đăng nhập
        }

        private void Login(object? parameter)
        {
            // check Username or Password is null/empty ?
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Username and password are required.");
                return;
            }
            else
            {
                //check username
                var user = _context.Account.FirstOrDefault(u => u.UserName == Username);

                //Check pass
                if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
                {
                    //check status
                    if (user.Status.ToString().Equals("Inactive"))
                    {
                        MessageBox.Show("Your Account is locked");
                        return;
                    }
                    //Save in Application
                    Application.Current.Properties["UserName"] = user.UserName;
                    Application.Current.Properties["UserRole"] = user.Role.ToString();
                    Application.Current.Properties["User"] = _context.Account.FirstOrDefault(u => u.UserName == user.UserName);

                   
                    if(user.Role.ToString() == "Admin")
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                    }else if(user.Role.ToString() == "Doctor")
                    {
                        DoctorWindow doctorWindow = new DoctorWindow();
                        doctorWindow.Show();
                    }
                    else if (user.Role.ToString() == "Receipter")
                    {
                        ReceiptorWindow receiptor = new ReceiptorWindow();
                        receiptor.Show();
                    }

                    _loginWindow.Close();
               

                }
                else
                {
                    // Login Failed
                    MessageBox.Show("Invalid username or password.");
                }

            }

        }
        private bool CanLogin(object? parameter)
        {
            // 
            return true;

        }


    }
}