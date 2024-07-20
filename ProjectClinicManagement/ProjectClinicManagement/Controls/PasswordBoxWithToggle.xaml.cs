using System.Windows;
using System.Windows.Controls;

namespace ProjectClinicManagement.Controls
{
    public partial class PasswordBoxWithToggle : UserControl
    {
        private bool _isPasswordChanging = false;

        public PasswordBoxWithToggle()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_isPasswordChanging)
                return;

            _isPasswordChanging = true;
            TextBox.Text = PasswordBox.Password;
            _isPasswordChanging = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isPasswordChanging)
                return;

            _isPasswordChanging = true;
            PasswordBox.Password = TextBox.Text;
            _isPasswordChanging = false;
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
