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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectClinicManagement.Views.Receiptor
{
    /// <summary>
    /// Interaction logic for ReceiptDetails.xaml
    /// </summary>
    public partial class ReceiptDetails : Page
    {
        private ReceiptDetailsVM receiptDetails;
        public ReceiptDetails()
        {
            InitializeComponent();
            receiptDetails = new ReceiptDetailsVM(ReceiptorVM.receiptInsta);
            this.DataContext = receiptDetails;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            receiptDetails._navigationService = NavigationService;

        }
    }
}
