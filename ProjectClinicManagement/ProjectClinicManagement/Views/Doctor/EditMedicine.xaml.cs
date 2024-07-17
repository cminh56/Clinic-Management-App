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
    /// Interaction logic for EditMedicine.xaml
    /// </summary>
    public partial class EditMedicine : Page
    {
        private EditMedicineVM vm;
        public EditMedicine()
        {

            InitializeComponent();
            vm = new EditMedicineVM(MedicineVM.medicineInstan);
            this.DataContext = vm;

        }
    }
}
