﻿using ProjectClinicManagement.ViewModel.DoctorViewModel;
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

namespace ProjectClinicManagement.Views.Doctor
{
    /// <summary>
    /// Interaction logic for ViewPrescription.xaml
    /// </summary>
    public partial class ViewPrescription : Page
    {
        private PrescriptionVM PrescriptionVM;
        public ViewPrescription()
        {
            InitializeComponent();
            PrescriptionVM = new PrescriptionVM(PatientRecordVM.patientInstan);
            this.DataContext = PrescriptionVM;
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            PrescriptionVM._navigationService = NavigationService;

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            PrescriptionVM._navigationService = NavigationService;

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            PrescriptionVM._navigationService = NavigationService;

        }
    }
}
