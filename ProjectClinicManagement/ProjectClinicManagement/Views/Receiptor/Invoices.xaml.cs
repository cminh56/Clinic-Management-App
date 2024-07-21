﻿using System;
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
using System.Windows.Shapes;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.ViewModel.Receiptor;


namespace ProjectClinicManagement.Views.Receiptor
{
    /// <summary>
    /// Interaction logic for Invoices.xaml
    /// </summary>
    public partial class Invoices : Window
    {
        private InVoicesVM vm;
        public Invoices()
        {
            InitializeComponent();
            vm = new InVoicesVM(ReceiptorVM.receiptInsta);   
            this.DataContext = vm;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    tbDate.DataContext = receipt.Date.ToString("dd/MM/yyyy");
        //    tbId.DataContext = receipt.Id;
        //    tbPatientName.DataContext = receipt.PatientName;
        //    tbAddress.DataContext = receipt.Address;
        //    tbPhone.DataContext = receipt.Phone;
        //    tbStatus.DataContext = receipt.Status;
        //    tbTotalPrice.DataContext = receipt.TotalPrice;
        //    tbSymtoms.DataContext = receipt.Symptoms;
        //    tbTotalP.DataContext = receipt.TotalPrice;
        //    tbMedicines.DataContext = receipt.MedicineName;
        //    tbQuantity.DataContext = receipt.Quantity;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        //private void btnChangeStatus_Click(object sender, RoutedEventArgs e)
        //{
        //    if (receipt.Status == ProjectClinicManagement.Models.Receipt.StatusType.Paid)
        //    {
        //        receipt.Status = ProjectClinicManagement.Models.Receipt.StatusType.Unpaid;
        //    }
        //    else
        //    {
        //        receipt.Status = ProjectClinicManagement.Models.Receipt.StatusType.Paid;
        //    }

        //    // Cập nhật giao diện người dùng
        //    tbStatus.Text = receipt.Status.ToString();

        //    // Lưu thay đổi vào cơ sở dữ liệu
        //    using (var context = new DataContext())
        //    {
        //        var receiptToUpdate = context.Receipts.FirstOrDefault(r => r.Id == receipt.Id);
        //        if (receiptToUpdate != null)
        //        {
        //            receiptToUpdate.Status = receipt.Status;
        //            context.SaveChanges();
        //            MessageBox.Show("Status changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Failed to change status.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}
    }
}
