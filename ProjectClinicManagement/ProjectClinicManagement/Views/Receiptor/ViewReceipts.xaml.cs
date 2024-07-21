using ProjectClinicManagement.ViewModel.AdminViewModel;
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
    /// Interaction logic for ViewReceipts.xaml
    /// </summary>
    public partial class ViewReceipts : Page
    {
        private ReceiptorVM receiptorVM;
        public ViewReceipts()
        {
            InitializeComponent();
            receiptorVM = new ReceiptorVM();
            this.DataContext = receiptorVM;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            receiptorVM._navigationService = NavigationService;

        }

    }
}
