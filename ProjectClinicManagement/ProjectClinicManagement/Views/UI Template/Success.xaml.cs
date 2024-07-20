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

namespace ProjectClinicManagement.Views.UI_Template
{
    /// <summary>
    /// Interaction logic for Success.xaml
    /// </summary>
    public partial class Success : Window
    {
        public bool Result { get; private set; }
        public Success(string message, string title = "Title")
        {
            InitializeComponent();
            MessageTextBlock.Text = message;
            Title = title;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            this.Close();
        }

        public static bool? Show(string message, string title = "Title")
        {
            Success messageBox = new Success(message, title);
            messageBox.ShowDialog();
            return messageBox.Result;
        }
    }
}
