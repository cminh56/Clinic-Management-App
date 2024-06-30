
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel
{
    public class LoginViewModel
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
            // Kiểm tra xem Username hoặc Password có null hoặc empty không
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Username and password are required.");
                return;
            }
            else
            {

                var user = _context.Account.FirstOrDefault(u => u.UserName == Username && u.Password == Password);

                if (user != null)
                {


                    // Open main window (Window1)
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    _loginWindow.Close();

                }
                else
                {
                    // Đăng nhập thất bại
                    System.Windows.MessageBox.Show("Invalid username or password.");
                }

            }

        }
        private bool CanLogin(object? parameter)
        {
            // Điều kiện để lệnh đăng nhập có thể thực thi
            return true;

        }
    }
}