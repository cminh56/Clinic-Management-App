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

namespace ProjectClinicManagement.Views.Authentication
{
    /// <summary>
    /// Interaction logic for AuthenWindow.xaml
    /// </summary>
    public partial class AuthenWindow : Window
    {
        public AuthenWindow()
        {
            InitializeComponent();
        }
        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến LoginPage khi MainWindow khởi động
            MainFrame.Navigate(new ChangePasswordTemplate());
        }
    }
}
