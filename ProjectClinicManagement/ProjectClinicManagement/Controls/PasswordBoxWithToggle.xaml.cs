using System.Windows;
using System.Windows.Controls;

namespace ProjectClinicManagement.Controls
{
    public partial class PasswordBoxWithToggle : UserControl
    {
        public PasswordBoxWithToggle()
        {
            InitializeComponent();
            TextBox.TextChanged += (s, e) => PasswordBox.Password = TextBox.Text;
            PasswordBox.PasswordChanged += (s, e) => TextBox.Text = PasswordBox.Password;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Collapsed;
            TextBox.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Visible;
            TextBox.Visibility = Visibility.Collapsed;
        }

        public string Password
        {
            get => PasswordBox.Password;
            set
            {
                PasswordBox.Password = value;
                TextBox.Text = value;
            }
        }
    }
}
