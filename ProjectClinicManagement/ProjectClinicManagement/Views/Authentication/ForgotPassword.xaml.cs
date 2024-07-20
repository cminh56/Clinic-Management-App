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
    /// Interaction logic for ForgotPassWord.xaml
    /// </summary>
    public partial class ForgotPassWord : Page
    {
        private ForgotPassVM ForgotPassVM;
        private AuthenWindow _authenWindow;
        public ForgotPassWord(Window window)
        {
            InitializeComponent();
            _authenWindow = window as AuthenWindow;
            ForgotPassVM = new ForgotPassVM();
            this.DataContext = ForgotPassVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Tôi muốn về page login
            _authenWindow.MainFrame.Navigate(new Login(_authenWindow));
        }
    }
}
