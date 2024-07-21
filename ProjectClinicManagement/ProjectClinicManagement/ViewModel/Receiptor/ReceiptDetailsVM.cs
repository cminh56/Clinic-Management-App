using Microsoft.EntityFrameworkCore;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.ViewModel.DoctorViewModel;
using ProjectClinicManagement.Views.Receiptor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjectClinicManagement.ViewModel.Receiptor
{
    public class ReceiptDetailsVM : BaseViewModel
    {
        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }
        private Receipt receipt;
        public Receipt Receipt
        {
            get { return (Receipt)receipt; }

        }


        private Prescription _Prescription;
        public Prescription Prescription
        {
            get { return (Prescription)_Prescription; }
            set
            {
                _Prescription = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MedicineDetail> _medicines;
        public ObservableCollection<MedicineDetail> Medicines
        {
            get { return _medicines; }
            set
            {
                _medicines = value;
                OnPropertyChanged();
            }
        }
        private bool isCashChecked;
        private bool isCardChecked;

        public bool IsCashChecked
        {
            get { return isCashChecked; }
            set
            {
                isCashChecked = value;
                OnPropertyChanged();
            }
        }

        public bool IsCardChecked
        {
            get { return isCardChecked; }
            set
            {
                isCardChecked = value;
                OnPropertyChanged();
            }
        }
        private readonly DataContext context;
        public string PatientName { get; set; }
        public string Phone { get; set; }
        public string PatientRecordId { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Instruction { get; set; }
        public string Remark { get; set; }
        public string Date { get; set; }
        public ICommand BackCommand { get; }
        public ICommand CheckoutCommand { get; }
        public ReceiptDetailsVM(Receipt receiptInst)
        {

            receipt = receiptInst;
            _medicines = new ObservableCollection<MedicineDetail>();
            context = new DataContext();
            Patient_Record patient_Record = context.Patient_Records.FirstOrDefault(p => p.PatientId == receipt.PatientId);
            if (patient_Record != null)
            {
                _Prescription = context.Prescriptions
                              .Include(p => p.Patient_Record)
              .ThenInclude(pr => pr.Patient)
              .Include(p => p.Prescription_Medicines)
                              .ThenInclude(pm => pm.Medicine).FirstOrDefault(p => p.PatientRecordId == patient_Record.Id);

            }
            if (_Prescription != null)
            {
                Prescription = _Prescription;
                PatientName = Prescription.Patient_Record.Patient.Name;
                Phone = Prescription.Patient_Record.Patient.Phone;
                PatientRecordId = Prescription.PatientRecordId.ToString();
                Dosage = Prescription.Dosage;
                Duration = Prescription.Duration;
                Instruction = Prescription.Instruction;
                Remark = Prescription.Remark;
                Date = Prescription.Date.ToString("d");


                foreach (Prescription_Medicine pm in _Prescription.Prescription_Medicines)
                {
                    Medicine m = context.Medicines.FirstOrDefault(m => m.Id == pm.MedicineID);
                    MedicineDetail md = new MedicineDetail()
                    {
                        MedicineId = pm.MedicineID,
                        GenericName = m.GenericName,
                        ATCCode = m.ATCCode,
                        Price = pm.Price,
                        Quantity = pm.Quantity,
                        TotalPrice = pm.Price * pm.Quantity

                    };
                    _medicines.Add(md);
                }


                BackCommand = new RelayCommand(NavigateToBackPage);
                CheckoutCommand = new RelayCommand(NavigateToCheckout);

            }

        }


        private void NavigateToBackPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Receiptor/ViewReceipts.xaml", UriKind.Relative));
        }

        private void NavigateToCheckout(object parameter)
        {

           
            if (isCashChecked)
            {

                Invoices invoices = new Invoices(receipt);
                invoices.Show();
            }
            if (IsCardChecked)
            {
                VietQRPaymentAPI paymentAPI = new VietQRPaymentAPI(receipt);
                paymentAPI.Show();
            }
        }



    }
}
