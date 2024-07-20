using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.EntityFrameworkCore;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;

namespace ProjectClinicManagement.Views.Receiptor
{
    /// <summary>
    /// Interaction logic for Receipt.xaml
    /// </summary>
    public partial class Receipt : Window
    {
        public DataContext context;
        public ObservableCollection<ReceiptViewModel> AllReceipts { get; set; }
        public Receipt()
        {
            InitializeComponent();
            context = new DataContext();
            AllReceipts = new ObservableCollection<ReceiptViewModel>();
            DataContext = this; // Set DataContext to the current instance
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           LoadData();
        }
        private void LoadData()
        {
            var query = from prescription in context.Prescriptions
                        join patientRecord in context.Patient_Records on prescription.PatientRecordId equals patientRecord.Id
                        join patient in context.Patients on patientRecord.PatientId equals patient.Id
                        join prescriptionMedicine in context.Prescription_Medicines on prescription.Id equals prescriptionMedicine.PrescriptionID
                        join medicine in context.Medicines on prescriptionMedicine.MedicineID equals medicine.Id
                        join receipt in context.Receipts on patient.Id equals receipt.PatientId
                        select new ReceiptViewModel
                        {
                            Id = receipt.Id,
                            PatientName = patient.Name,
                            Age = patient.Age,
                            Weight = patient.Weight,
                            Height = patient.Height,
                            Address = patient.Address,
                            Phone = patient.Phone,
                            Symptoms = patientRecord.Symptoms,
                            MedicineName = medicine.Name,
                            Quantity = prescriptionMedicine.Quantity,
                            TotalPrice = receipt.TotalAmount,
                            Date = receipt.Date,
                            Status = receipt.Status
                        };

            AllReceipts = new ObservableCollection<ReceiptViewModel>(query.ToList());
            lvData.ItemsSource = AllReceipts;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilterData();
        }
        public class ReceiptViewModel
        {
            public int Id { get; set; }
            public string PatientName { get; set; }
            public int Age { get; set; }
            public double Weight { get; set; }
            public double Height { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Symptoms { get; set; }
            public string MedicineName { get; set; }
            public int Quantity { get; set; }
            public float TotalPrice { get; set; }
            public DateTime Date { get; set; }
            public ProjectClinicManagement.Models.Receipt.StatusType Status { get; set; }
        }
        private void FilterData()
        {
            var filteredData = AllReceipts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(tbSearchPatientName.Text))
            {
                filteredData = filteredData.Where(r => r.PatientName.Contains(tbSearchPatientName.Text, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(tbSearchPhone.Text))
            {
                filteredData = filteredData.Where(r => r.Phone.Contains(tbSearchPhone.Text));
            }
            if (dtpDateFrom.SelectedDate.HasValue)
            {
                filteredData = filteredData.Where(r => r.Date >= dtpDateFrom.SelectedDate.Value);
            }

            if (dtpDateTo.SelectedDate.HasValue)
            {
                filteredData = filteredData.Where(r => r.Date <= dtpDateTo.SelectedDate.Value);
            }
            lvData.ItemsSource = new ObservableCollection<ReceiptViewModel>(filteredData.ToList());
        }
        private void btCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (cbCash.IsChecked == true)
            {
                // Điều hướng đến form Invoices.xaml
                Invoices invoices = new Invoices();
                invoices.Show();
            }
            else if (cbCard.IsChecked == true)
            {
                VietQRPaymentAPI vietQRPaymentAPI = new VietQRPaymentAPI();
                vietQRPaymentAPI.Show();
            }
            else
            {
                MessageBox.Show("Please select a payment method.");
            }
        }
    }
    }
