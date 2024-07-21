using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ProjectClinicManagement.Views.Admin
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Page
    {
        private EditUserVM vm;
        public EditUser()
        {

            InitializeComponent();
            vm = new EditUserVM(UserVM.accountInstan);
            this.DataContext = vm;
          

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            vm._navigationService = NavigationService;

        }
    }
}
