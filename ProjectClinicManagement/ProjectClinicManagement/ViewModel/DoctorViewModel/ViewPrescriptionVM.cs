using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.ViewModel.Common;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class ViewPrescriptionVM : BaseViewModel
    {
   
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
        private readonly DataContext _context;

        public string PatientName { get; set; }
        public string Phone { get; set; }
        public string PatientRecordId { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Instruction { get; set; }
        public string Remark { get; set; }
        public string Date { get; set; }
        public ViewPrescriptionVM(Prescription prescription)
        {
            Prescription = prescription;
            _context = new DataContext();

            var prescriptionDetails = _context.Prescriptions
.Include(p => p.Patient_Record)
.ThenInclude(pr => pr.Patient)
.Include(p => p.Prescription_Medicines)
                .ThenInclude(pm => pm.Medicine)
.FirstOrDefault(p => p.PatientRecordId == Prescription.PatientRecordId);
            if (prescriptionDetails != null)
            {
                Prescription = prescriptionDetails;
                PatientName = Prescription.Patient_Record.Patient.Name;
                Phone = Prescription.Patient_Record.Patient.Phone;
                PatientRecordId = Prescription.PatientRecordId.ToString(); 
                Dosage = Prescription.Dosage;
                Duration = Prescription.Duration;
                Instruction = Prescription.Instruction;
                Remark = Prescription.Remark;
                Date = Prescription.Date.ToString("d");

                OnPropertyChanged(nameof(PatientName));
                OnPropertyChanged(nameof(Phone));
                OnPropertyChanged(nameof(PatientRecordId));
                OnPropertyChanged(nameof(Dosage));
                OnPropertyChanged(nameof(Duration));
                OnPropertyChanged(nameof(Instruction));
                OnPropertyChanged(nameof(Remark));
                OnPropertyChanged(nameof(Date));



                Medicines = new ObservableCollection<MedicineDetail>(
      _context.Prescription_Medicines
          .Include(pm => pm.Medicine)
          .Where(pm => pm.PrescriptionID == Prescription.Id)
          .Select(pm => new MedicineDetail
          {
              MedicineId = pm.Medicine.Id,
              GenericName = pm.Medicine.GenericName,
              ATCCode = pm.Medicine.ATCCode,
              Price = pm.Medicine.Price,
              Quantity = pm.Quantity,
              TotalPrice = pm.Medicine.Price * pm.Quantity
          })
          .ToList()
  );
          


            }

        }




    }
}
