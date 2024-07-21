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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.Win32;
using System.IO;

namespace ProjectClinicManagement.Views.Receiptor
{
    /// <summary>
    /// Interaction logic for Receipt.xaml
    /// </summary>
    public partial class Receipt : Window
    {
        public DataContext context;
        public ObservableCollection<ReceiptViewModel> AllReceipts { get; set; }
        private ReceiptViewModel selectedReceipt;
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
                        group new { prescription, patientRecord, patient, prescriptionMedicine, medicine, receipt } by new
                        {
                            receipt.Id,
                            patient.Name,
                            patient.Age,
                            patient.Weight,
                            patient.Height,
                            patient.Address,
                            patient.Phone,
                            patientRecord.Symptoms,
                            receipt.TotalAmount,
                            receipt.Date,
                            receipt.Status
                        } into grouped
                        select new ReceiptViewModel
                        {
                            Id = grouped.Key.Id,
                            PatientName = grouped.Key.Name,
                            Age = grouped.Key.Age,
                            Weight = grouped.Key.Weight,
                            Height = grouped.Key.Height,
                            Address = grouped.Key.Address,
                            Phone = grouped.Key.Phone,
                            Symptoms = grouped.Key.Symptoms,
                            MedicineName = string.Join(", ", grouped.Select(g => g.medicine.Name).Distinct()),
                            Quantity = grouped.Sum(g => g.prescriptionMedicine.Quantity),
                            TotalPrice = grouped.Sum(g => g.prescriptionMedicine.Quantity * g.prescriptionMedicine.Price),
                            Date = grouped.Key.Date,
                            Status = grouped.Key.Status
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
                Invoices invoices = new Invoices(selectedReceipt);
                invoices.Show();
            }
            else if (cbCard.IsChecked == true)
            {
                VietQRPaymentAPI vietQRPaymentAPI = new VietQRPaymentAPI(selectedReceipt);
                vietQRPaymentAPI.Show();
            }
            else
            {
                MessageBox.Show("Please select a payment method.");
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedReceipt = (ReceiptViewModel)lvData.SelectedItem;
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Receipts");

                    // Add header
                    worksheet.Cells[1, 1].Value = "No";
                    worksheet.Cells[1, 2].Value = "Patients Name";
                    worksheet.Cells[1, 3].Value = "Age";
                    worksheet.Cells[1, 4].Value = "Weight";
                    worksheet.Cells[1, 5].Value = "Height";
                    worksheet.Cells[1, 6].Value = "Address";
                    worksheet.Cells[1, 7].Value = "Phone";
                    worksheet.Cells[1, 8].Value = "Symptoms";
                    worksheet.Cells[1, 9].Value = "Medicines";
                    worksheet.Cells[1, 10].Value = "Quantity";
                    worksheet.Cells[1, 11].Value = "Total Price";
                    worksheet.Cells[1, 12].Value = "Date";
                    worksheet.Cells[1, 13].Value = "Status";

                    // Add data
                    for (int i = 0; i < AllReceipts.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = AllReceipts[i].Id;
                        worksheet.Cells[i + 2, 2].Value = AllReceipts[i].PatientName;
                        worksheet.Cells[i + 2, 3].Value = AllReceipts[i].Age;
                        worksheet.Cells[i + 2, 4].Value = AllReceipts[i].Weight;
                        worksheet.Cells[i + 2, 5].Value = AllReceipts[i].Height;
                        worksheet.Cells[i + 2, 6].Value = AllReceipts[i].Address;
                        worksheet.Cells[i + 2, 7].Value = AllReceipts[i].Phone;
                        worksheet.Cells[i + 2, 8].Value = AllReceipts[i].Symptoms;
                        worksheet.Cells[i + 2, 9].Value = AllReceipts[i].MedicineName;
                        worksheet.Cells[i + 2, 10].Value = AllReceipts[i].Quantity;
                        worksheet.Cells[i + 2, 11].Value = AllReceipts[i].TotalPrice;
                        worksheet.Cells[i + 2, 12].Value = AllReceipts[i].Date.ToString("dd/MM/yyyy");
                        worksheet.Cells[i + 2, 13].Value = AllReceipts[i].Status.ToString();
                    }

                    // Auto fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Save file
                    var file = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(file);

                    MessageBox.Show("Export successful ! Let's check it !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

    }
}