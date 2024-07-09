using ProjectClinicManagement.ViewModel.AdminViewModel;
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
    /// Interaction logic for ViewMedicine.xaml
    /// </summary>
    public partial class ViewMedicine : Page
    {
        private MedicineVM MedicineVM;
        public ViewMedicine()
        {
            InitializeComponent();
            MedicineVM = new MedicineVM();
            this.DataContext = MedicineVM;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MedicineVM._navigationService = NavigationService;
        }
    }
}
