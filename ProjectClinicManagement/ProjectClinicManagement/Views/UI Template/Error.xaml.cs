using System.Windows;

namespace ProjectClinicManagement.Views.UI_Template
{
    public partial class Error : Window
    {
        public Error()
        {
            InitializeComponent();
        }

        public static void Show(string message)
        {
            var errorWindow = new Error();
            errorWindow.MessageTextBlock.Text = message;
            errorWindow.ShowDialog();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
