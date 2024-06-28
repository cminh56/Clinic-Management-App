using ProjectClinicManagement.ViewModel;
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
using System.Windows.Shapes;

namespace ProjectClinicManagement.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private LoginViewModel viewModel;
        public Login()
        {
            InitializeComponent();
            viewModel = new LoginViewModel(this);
            this.DataContext = viewModel;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (viewModel != null)
            {
              
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
}
