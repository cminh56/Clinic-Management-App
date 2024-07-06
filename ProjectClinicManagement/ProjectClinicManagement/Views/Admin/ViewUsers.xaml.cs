using ProjectClinicManagement.ViewModel;
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
    /// Interaction logic for ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : Page
    {
        private UserVM userVM;
        public ViewUsers()
        {
            InitializeComponent();
           userVM = new UserVM();   
            this.DataContext = userVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userVM._navigationService = NavigationService;
            
        }
    }
}
