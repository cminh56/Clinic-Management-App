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
using static ProjectClinicManagement.Views.Receiptor.Receipt;

namespace ProjectClinicManagement.Views.Receiptor
{
    /// <summary>
    /// Interaction logic for Invoices.xaml
    /// </summary>
    public partial class Invoices : Window
    {
        private ReceiptViewModel receipt;
        public Invoices(ReceiptViewModel receipt)
        {
            InitializeComponent();
            this.receipt = receipt;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbDate.DataContext = receipt.Date;
            tbId.DataContext = receipt.Id;
            tbPatientName.DataContext = receipt.PatientName;
            tbAddress.DataContext = receipt.Address;
            tbPhone.DataContext = receipt.Phone;
            tbStatus.DataContext = receipt.Status;
            tbTotalPrice.DataContext = receipt.TotalPrice;
            tbSymtoms.DataContext = receipt.Symptoms;
            tbTotalP.DataContext = receipt.TotalPrice;
            tbMedicines.DataContext = receipt.MedicineName;
            tbQuantity.DataContext = receipt.Quantity;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
