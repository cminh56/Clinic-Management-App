using Microsoft.EntityFrameworkCore;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.ViewModel.DoctorViewModel;
using ProjectClinicManagement.Views.Receiptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectClinicManagement.ViewModel.Receiptor
{
    public class InVoicesVM : BaseViewModel
    {
        private DataContext context;


        private  List<MedicineDetail> medicines;
        public List<MedicineDetail> Medicines
        {
            get { return medicines; }
            set
            {
                medicines = value;
                OnPropertyChanged();
            }
        }
        private Receipt receipt;
        public Receipt Receipt
        {
            get { return receipt; }
            set
            {
                receipt = value;
                OnPropertyChanged();
            }
        }
 
        public ICommand CheckoutCommand { get; set; }

        public InVoicesVM(Receipt receiptIns) {

            context = new DataContext();
            medicines = new List<MedicineDetail>();
            receipt = receiptIns;
            CheckoutCommand = new RelayCommand(Checkout);



        }

        private void Checkout(Object pra)
        {
            if (receipt.Status == ProjectClinicManagement.Models.Receipt.StatusType.Paid)
            {
                receipt.Status = ProjectClinicManagement.Models.Receipt.StatusType.Unpaid;
            }
            else
            {
                receipt.Status = ProjectClinicManagement.Models.Receipt.StatusType.Paid;
            }
            using (var context = new DataContext())
            {
                var receiptToUpdate = context.Receipts.FirstOrDefault(r => r.Id == receipt.Id);
                if (receiptToUpdate != null)
                {
                    receiptToUpdate.Status = receipt.Status;
                    context.SaveChanges();
                    MessageBox.Show("Status changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Receipt.Status = receipt.Status;

                }
                else
                {
                    MessageBox.Show("Failed to change status.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
     
        public void getData()
        {
            Patient_Record patient_Record = context.Patient_Records.FirstOrDefault(p => p.PatientId == receipt.PatientId);
            Prescription prescription = context.Prescriptions.Include(p => p.Prescription_Medicines).FirstOrDefault(p => p.PatientRecordId == patient_Record.Id);

            foreach (Prescription_Medicine pm in prescription.Prescription_Medicines)
            {
                Medicine medicine = context.Medicines.FirstOrDefault(p => p.Id == pm.MedicineID);

                MedicineDetail md = new MedicineDetail()
                {
                    MedicineId = pm.MedicineID,
                    GenericName = medicine.GenericName,
                    ATCCode = medicine.ATCCode,
                    Price = pm.Price,
                    Quantity = pm.Quantity,
                    TotalPrice = pm.Price * pm.Quantity

                };
                medicines.Add(md);


            }
        }
    }
}
