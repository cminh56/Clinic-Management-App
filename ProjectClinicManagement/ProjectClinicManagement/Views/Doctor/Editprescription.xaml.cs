using ProjectClinicManagement.ViewModel.DoctorViewModel;
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

namespace ProjectClinicManagement.Views.Doctor
{
    /// <summary>
    /// Interaction logic for Editprescription.xaml
    /// </summary>
    public partial class Editprescription : Page
    {
        private EditPrescriptionVM vm;
        public Editprescription()
        {
            InitializeComponent();
            vm = new EditPrescriptionVM(PrescriptionVM.PrescriptionInstan);
            this.DataContext = vm;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
    }
}
