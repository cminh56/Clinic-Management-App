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
    public class EditPrescriptionVM : BaseViewModel
    {
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



            EditPrescriptionCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };
           


        }

        public void Validate2(string propertyName, object propertyValue)
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



            AddMedicineCommand.CanExecuteChanged += (sender, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
            };



        }
        public ICommand EditPrescriptionCommand { get; }
        public ICommand AddMedicineCommand { get; }
        public ICommand DeleteMedicineCommand { get; }

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
            set { dosage = value; OnPropertyChanged(); }

        }

      
        private string duration;
        [Required(ErrorMessage = "Duration is required.")]
        public string Duration
        {
            get => duration;
            set { duration = value; OnPropertyChanged(); }
        }

       
        private string instruction;
        [Required(ErrorMessage = "Instruction is required.")]
        public string Instruction
        {
            get => instruction;
            set { instruction = value; OnPropertyChanged(); }
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

        [Required(ErrorMessage = "Remark is required.")]
        private string remark;
        public string Remark
        {
            get => remark;
            set { remark = value; OnPropertyChanged(); }
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

        public EditPrescriptionVM(Prescription prescription)
        {
            Prescription = prescription;
            _context = new DataContext();

            var prescriptionDetails = _context.Prescriptions
      .Include(p => p.Patient_Record)
      .ThenInclude(pr => pr.Patient)
      .Include(p => p.Prescription_Medicines)
                      .ThenInclude(pm => pm.Medicine)
      .FirstOrDefault(p => p.Id == Prescription.Id);
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

                EditPrescriptionCommand = new RelayCommand(EditPrescription, CanSubmit);
                AddMedicineCommand = new RelayCommand(AddPrescriptionMedicine);
                DeleteMedicineCommand = new RelayCommand(DeletePrescriptionMedicine);
              
            }
            var allMedicines = _context.Medicines
                    .Select(m => $"{m.Id} - {m.GenericName}")
                    .ToList();
            MedicineComboBoxItems = new ObservableCollection<string>(allMedicines);
            BackCommand = new RelayCommand(NavigateToBackPage);
        }

        private void EditPrescription(object parameter)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(Dosage))
                {
                    MessageBox.Show("Dosage is required.");
                    return;
                }

                Prescription.Dosage = Dosage;
                Prescription.Duration = Duration;
                Prescription.Instruction = Instruction;
                Prescription.Remark = Remark;


                // Lưu thay đổi vào cơ sở dữ liệu
                _context.Prescriptions.Update(Prescription);
                _context.SaveChanges();

                MessageBox.Show("Prescription updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing Prescription: {ex.Message}");
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
                    PrescriptionID = Prescription.Id,
                    Quantity = int.Parse(MQuantity),
                    Unit = Unit,
                    Price = (float.Parse(MQuantity) * medicine.Price)
                };

                _context.Prescription_Medicines.Add(newPrescription_Medicine);
                _context.SaveChanges();

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

                MessageBox.Show("Medicine added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding medicine: {ex.Message}");
            }
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
                        .FirstOrDefault(pm => pm.MedicineID == medicineDetail.MedicineId && pm.PrescriptionID == Prescription.Id);

                    if (prescriptionMedicine != null)
                    {
                        _context.Prescription_Medicines.Remove(prescriptionMedicine);
                        _context.SaveChanges();
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
