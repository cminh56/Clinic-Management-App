using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ZXing.Aztec.Internal;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Views.Doctor;
using System.Windows.Navigation;

namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class AddPrescriptionVM : BaseViewModel, INotifyDataErrorInfo
    {
        int _PrescriptionID = 0;
        public static Patient_Record patientInstan;
        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];

            }
            else
            {
                return Enumerable.Empty<string>();
            }

        }
        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);


            if (results.Any())
            {

                // Check if propertyName already exists in Errors
                if (Errors.ContainsKey(propertyName))
                {
                    // Update existing errors for propertyName
                    Errors[propertyName] = results.Select(r => r.ErrorMessage).ToList();
                }
                else
                {
                    // Add new entry for propertyName
                    Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }



            AddPrescriptionCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };



        }
        public ICommand AddPrescriptionCommand { get; }
        public ICommand AddMedicineCommand { get; }
        public ICommand DeleteMedicineCommand { get; }

        private Patient_Record _patient;
        public Patient_Record Patient
        {
            get { return (Patient_Record)_patient; }
            set
            {
                _patient = value;
                OnPropertyChanged();

            }
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
        private readonly DataContext _context;

        public string PatientName { get; set; }
        public string Phone { get; set; }
        public string PatientRecordId { get; set; }

        private string dosage;

        [Required(ErrorMessage = "Dosage is required.")]
        public string Dosage
        {
            get => dosage;
            set { dosage = value; Validate(nameof(Dosage), value); }

        }
        private string duration;
        [Required(ErrorMessage = "Duration is required.")]
        public string Duration
        {
            get => duration;
            set { duration = value; Validate(nameof(Duration), value); }
        }
        private string instruction;
        [Required(ErrorMessage = "Instruction is required.")]
        public string Instruction
        {
            get => instruction;
            set { instruction = value; Validate(nameof(Instruction), value); }
        }
        private ObservableCollection<string> _medicineComboBoxItems;
        public ObservableCollection<string> MedicineComboBoxItems
        {
            get { return _medicineComboBoxItems; }
            set
            {
                _medicineComboBoxItems = value;
                OnPropertyChanged();
            }
        }
        private string _selectedMedicine;
        public string SelectedMedicine
        {
            get => _selectedMedicine;
            set { _selectedMedicine = value; OnPropertyChanged(); }
        }

        private string mquantity;
        public string MQuantity
        {
            get => mquantity;
            set { mquantity = value; OnPropertyChanged(); }
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(); }
        }

        private string remark;
        [Required(ErrorMessage = "Remark is required.")]
        public string Remark
        {
            get => remark;
            set { remark = value; Validate(nameof(Remark), value); }
        }

        public MedicineDetail Medicine { get; set; }

        public string Date { get; set; }
        public ICommand BackCommand { get; }

        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }
        public AddPrescriptionVM(Patient_Record patient)
        {

            _context = new DataContext();
            Patient = patient;


            int Prescriptionid = 0;
            if(_PrescriptionID != 0)
            {
                Prescriptionid = _PrescriptionID;
            }


            Medicines = new ObservableCollection<MedicineDetail>(
      _context.Prescription_Medicines
          .Include(pm => pm.Medicine)
          .Where(pm => pm.PrescriptionID == Prescriptionid)
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

            AddPrescriptionCommand = new RelayCommand(AddPrescription, CanExecuteAddPrescription);
            AddMedicineCommand = new RelayCommand(AddPrescriptionMedicine, CanExecuteAddMedicine);
          
            DeleteMedicineCommand = new RelayCommand(DeletePrescriptionMedicine);
            
            var allMedicines = _context.Medicines
                    .Select(m => $"{m.Id} - {m.GenericName}")
                    .ToList();
            MedicineComboBoxItems = new ObservableCollection<string>(allMedicines);
            BackCommand = new RelayCommand(NavigateToBackPage);
        }

        private void AddPrescription(object parameter)
        {

            try
            {

                if (string.IsNullOrWhiteSpace(Dosage))
                {
                    MessageBox.Show("Dosage is required.");
                    return;
                }

  

                var newPrescription = new Prescription
                {
                    PatientRecordId = Patient.Id,
                    Dosage = Dosage,
                    Duration = Duration,
                    Instruction = Instruction,
                    Remark = Remark,
                    Date = DateTime.Now,
                };

                _context.Prescriptions.Add(newPrescription);
                _context.SaveChanges();
                _PrescriptionID = newPrescription.Id;
                MessageBox.Show("Prescription added successfully with ID: " + _PrescriptionID);
            }
            catch (Exception ex)
            {
              
            }
        }
        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) && Errors.Count == 0;

        }

        private void AddPrescriptionMedicine(object parameter)
        {
            try
            {
                if (SelectedMedicine == null || string.IsNullOrWhiteSpace(MQuantity) || string.IsNullOrWhiteSpace(Unit))
                {
                    MessageBox.Show("All fields must be filled out to add medicine.");
                    return;
                }

                var medicineId = int.Parse(SelectedMedicine.Split(' ')[0]);
                var medicine = _context.Medicines.FirstOrDefault(m => m.Id == medicineId);
                if (medicine == null)
                {
                    MessageBox.Show("Selected medicine not found.");
                    return;
                }

                int quantity = int.Parse(MQuantity);
                float price = quantity * medicine.Price;
                var newPrescription_Medicine = new Prescription_Medicine
                {
                    MedicineID = medicineId,
                    PrescriptionID = _PrescriptionID,
                    Quantity = int.Parse(MQuantity),
                    Unit = Unit,
                    Price = (float.Parse(MQuantity) * medicine.Price)
                };

                _context.Prescription_Medicines.Add(newPrescription_Medicine);
                _context.SaveChanges();

                var newReceipt= new Receipt
                {
                    PatientId = Patient.PatientId,
                    TotalAmount = 0,
                    ReceptionistId = 2,
                    Date = DateTime.Now,
                    Status = Receipt.StatusType.Unpaid,
              
                };
                _context.Receipts.Add(newReceipt);
                _context.SaveChanges();


                Medicines = new ObservableCollection<MedicineDetail>(
_context.Prescription_Medicines
    .Include(pm => pm.Medicine)
    .Where(pm => pm.PrescriptionID == _PrescriptionID)
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

                MessageBox.Show("Medicine added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding medicine: {ex.Message}");
            }
        }

        private bool CanExecuteAddPrescription(object obj)
        {
            return _PrescriptionID == 0 && !HasErrors;
        }
        private bool CanExecuteAddMedicine(object obj)
        {
            return _PrescriptionID != 0;
        }
        private void DeletePrescriptionMedicine(object parameter)
        {
            var medicineDetail = parameter as MedicineDetail;
            if (medicineDetail != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this medicine?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var prescriptionMedicine = _context.Prescription_Medicines
                        .FirstOrDefault(pm => pm.MedicineID == medicineDetail.MedicineId && pm.PrescriptionID == _PrescriptionID);

                    if (prescriptionMedicine != null)
                    {
                        _context.Prescription_Medicines.Remove(prescriptionMedicine);
                        _context.SaveChanges();
                        Medicines = new ObservableCollection<MedicineDetail>(
                            _context.Prescription_Medicines
                                .Include(pm => pm.Medicine)
                                .Where(pm => pm.PrescriptionID == _PrescriptionID)
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
                    MessageBox.Show("Medicine deleted successfully.");
                }
            }
        }
        private void NavigateToBackPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Doctor/ViewPrescription.xaml", UriKind.Relative));
        }
    }

}
