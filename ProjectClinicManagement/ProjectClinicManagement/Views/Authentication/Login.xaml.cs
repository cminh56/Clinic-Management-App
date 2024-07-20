using ProjectClinicManagement.ViewModel.AuthenViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectClinicManagement.Views.Authentication
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private LoginViewModel viewModel;
        public Login(Window window)
        {
            InitializeComponent();
           
            viewModel = new LoginViewModel(window);
            this.DataContext = viewModel;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (viewModel != null)
            {

                viewModel.Password = PasswordBox.Password;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (showpass.IsChecked == true)
            {
                passwordTextBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                passwordTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordBox.Password = passwordTextBox.Text;
                PasswordBox.Visibility = Visibility.Visible;
                passwordTextBox.Visibility = Visibility.Collapsed;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangePass());
        }
    }
}
