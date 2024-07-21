using ProjectClinicManagement.ViewModel.Receiptor;
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

namespace ProjectClinicManagement.Views.Receiptor
{
    /// <summary>
    /// Interaction logic for ReceiptorWindow.xaml
    /// </summary>
    public partial class ReceiptorWindow : Window
    {
        private ReceiptorNavigationVM receiptorVM;
        public ReceiptorWindow()
        {
            InitializeComponent();
            receiptorVM = new ReceiptorNavigationVM();
            this.DataContext = receiptorVM;
        }

       
    }
}
