using ProjectClinicManagement.ViewModel.AdminViewModel;
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

namespace ProjectClinicManagement.Views.Admin
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        private AddUserVM addUserVM;
        public AddUser()
        {
            InitializeComponent();
            addUserVM = new AddUserVM();
            this.DataContext = addUserVM;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            addUserVM._navigationService = NavigationService;

        }
    }
}
