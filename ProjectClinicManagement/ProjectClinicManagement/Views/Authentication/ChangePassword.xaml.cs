using ProjectClinicManagement.ViewModel.AuthenViewModel;
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

namespace ProjectClinicManagement.Views.Authentication
{
    /// <summary>
    /// Interaction logic for ChangePasswordTemplate.xaml
    /// </summary>
    public partial class ChangePasswordTemplate : Page
    {
        private ChangePassVM ChangePassVM;
        public ChangePasswordTemplate()
        {
            InitializeComponent();
            ChangePassVM = new ChangePassVM();
            this.DataContext = ChangePassVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePassVM.CurrPassword = txtOldPass.Password;
            ChangePassVM.Newpassword = txtNewPass.Password;
            ChangePassVM.ConfirmPassWord = txtConfirmPass.Password;
      
        }
    }
}
