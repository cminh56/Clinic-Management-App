using ProjectClinicManagement.ViewModel.PatientViewModel;
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

namespace ProjectClinicManagement.Views.Patient
{
    /// <summary>
    /// Interaction logic for PatientPrinter.xaml
    /// </summary>
    public partial class PatientPrinter : Page
    {
        PatientRecordVM vm;
        public PatientPrinter()
        {
            InitializeComponent();
            vm = new PatientRecordVM(PatientVM.patientInstan);
            this.DataContext = vm;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
        private void PrintAction(object sender, RoutedEventArgs e)
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
