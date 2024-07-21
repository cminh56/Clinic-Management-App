using ProjectClinicManagement.Models;
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
    /// Interaction logic for PatientRecord.xaml
    /// </summary>
    public partial class PatientRecord : Page
    {
        private PatientRecordVM vm;
        public PatientRecord()
        {
            InitializeComponent();
            vm = new PatientRecordVM(PatientVM.patientInstan);
            this.DataContext = vm;
        }
        private void UpdatePatientRecord(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
        private void ViewPres(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
        private void ViewPatient(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
    }
}
