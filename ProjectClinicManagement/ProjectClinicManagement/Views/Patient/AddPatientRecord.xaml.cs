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
    /// Interaction logic for AddPatientRecord.xaml
    /// </summary>
    public partial class AddPatientRecord : Page
    {
        private AddPatientRecordVM vm;
        public AddPatientRecord()
        {
            InitializeComponent();
            vm = new AddPatientRecordVM(PatientVM.patientInstan);
            this.DataContext = vm;
        }
        private void ClickAdd(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
        private void ClickBack(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
    }
}
