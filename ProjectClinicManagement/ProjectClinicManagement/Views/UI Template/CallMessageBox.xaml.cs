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
    /// Interaction logic for CallMessageBox.xaml
    /// </summary>
    public partial class CallMessageBox : Window
    {
        public CallMessageBox()
        {
            InitializeComponent();
            bool? result1 = Success.Show("Your custom message here", "Custom Title");
            if (result1 == true)
            {
                Error.Show("An error has occurred. Please try again.");
            }
            else
            {
                // Handle No
            }
        }

    }
}
