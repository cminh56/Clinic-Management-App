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
    /// Interaction logic for EditPatientRecord.xaml
    /// </summary>
    public partial class EditPatientRecord : Page
    {
        private EditPatientRecordVM vm;
        public EditPatientRecord()
        {
            InitializeComponent();
            vm = new EditPatientRecordVM(PatientRecordVM.patientInstan);
            this.DataContext = vm;
        }
    }
}
