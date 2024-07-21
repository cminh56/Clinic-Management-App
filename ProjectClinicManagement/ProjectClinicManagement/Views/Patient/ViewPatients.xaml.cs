using ProjectClinicManagement.ViewModel.AdminViewModel;
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
    /// Interaction logic for ViewPatients.xaml
    /// </summary>
    public partial class ViewPatients : Page
    {
        private PatientVM patientVM;
       
        public ViewPatients()
        {
            InitializeComponent();
            patientVM = new PatientVM();
            this.DataContext = patientVM;
        }
        private void ClickAdd(object sender, RoutedEventArgs e)
        {
            patientVM._navigationService = NavigationService;
        }
        private void ClickUpdate(object sender, RoutedEventArgs e)
        {
            patientVM._navigationService = NavigationService;

        }
        private void ClickPatientRecord(object sender, RoutedEventArgs e)
        {
            patientVM._navigationService = NavigationService;

        }
        private void change(object sender, RoutedEventArgs e)
        {
            test.Text = SortCombobox2.SelectedIndex.ToString() ;
            
            patientVM = new PatientVM(SortCombobox.SelectedIndex, SortCombobox2.SelectedIndex);
            this.DataContext = patientVM;

        }

    }
}
