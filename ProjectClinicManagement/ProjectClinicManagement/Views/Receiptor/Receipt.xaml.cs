using System;
using System.Collections.Generic;
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
        public Receipt()
        {
            InitializeComponent();
            context = new DataContext();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var query = from prescription in context.Prescriptions
                        join patientRecord in context.Patient_Records on prescription.PatientRecordId equals patientRecord.Id
                        join patient in context.Patients on patientRecord.PatientId equals patient.Id
                        join prescriptionMedicine in context.Prescription_Medicines on prescription.Id equals prescriptionMedicine.PrescriptionID
                        join medicine in context.Medicines on prescriptionMedicine.MedicineID equals medicine.Id
                        join receipt in context.Receipts on patient.Id equals receipt.PatientId
                        select new
                        {
                            Id = receipt.Id, // Assuming Id is used as No, adjust as needed
                            PatientName = patient.Name,
                            Age = patient.Age, // Assuming Patient class has an Age property
                            Weight = patient.Weight, // Assuming Patient class has a Weight property
                            Height = patient.Height, // Assuming Patient class has a Height property
                            Address = patient.Address,
                            Symptoms = patientRecord.Symptoms, // Adjust as per your actual model
                            MedicineName = medicine.Name,
                            Quantity = prescriptionMedicine.Quantity, // Assuming Prescription_Medicine class has a Quantity property
                            TotalPrice = receipt.TotalAmount, // Assuming Receipt class has a TotalPrice property
                            Date = receipt.Date,
                            Status = receipt.Status // Assuming Receipt class has a Status property
                        };
            lvData.ItemsSource = query.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItem != null)
            {
                try
                {
                    var selectedItem = (dynamic)lvData.SelectedItem;
                    DataContext = selectedItem;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
    }
    }
