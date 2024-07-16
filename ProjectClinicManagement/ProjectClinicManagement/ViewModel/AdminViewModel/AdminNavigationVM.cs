using ProjectClinicManagement.Command;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectClinicManagement.ViewModel.AdminViewModel
{
    class AdminNavigationVM : BaseViewModel
    {
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public RelayCommand NavigateToPage1Command { get; }
        public RelayCommand NavigateToPage2Command { get; }

        public AdminNavigationVM()
        {
            NavigateToPage1Command = new RelayCommand(_ => NavigateToPage1());
            NavigateToPage2Command = new RelayCommand(_ => NavigateToPage2());
            CurrentPage = new Views.Admin.ViewUsers();
        }

        private void NavigateToPage1()
        {
            CurrentPage = new Views.Admin.ViewUsers();
        }

        private void NavigateToPage2()
        {
            CurrentPage = new Views.Page2();
        }
    }
}