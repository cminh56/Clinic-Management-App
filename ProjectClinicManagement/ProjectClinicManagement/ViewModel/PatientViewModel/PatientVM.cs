using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using OfficeOpenXml.LoadFunctions.Params;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Helper;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.AuthenViewModel;
using ProjectClinicManagement.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectClinicManagement.ViewModel.PatientViewModel
{
    public class PatientVM : BaseViewModel
    {
        private readonly ExelService exelService;
        public static Patient patientInstan;
        public String accountName { get; set; }
        private List<Patient> _Patients;
        public List<Patient> Patients
        {
            get { return _Patients; }
            set
            {
                _Patients = value;
                OnPropertyChanged();
            }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }
        public int _currentPage = 1; // Trang hiện tại
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();


            }

        }
        public int _itemsPerPage = 3; // Số mục trên mỗi trang
        public int totalpage;
        public int Totalpage
        {
            get { return totalpage; }
            set
            {
                totalpage = value;
                OnPropertyChanged();


            }

        }


        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                CurrentPage = 1;
                PlaceHolderText = string.Empty;
                loadPatientData();
            }
        }
        private string placeHolderText;
        public string PlaceHolderText
        {
            get { return placeHolderText; }
            set
            {
                placeHolderText = value;
                OnPropertyChanged();


            }

        }

        public ICommand AddPatientCommand { get; set; }
        public ICommand EditPatientCommand { get; set; }
        public ICommand PatientRecordCommand { get; set; }
        public ICommand Nextpage { get; set; }
        public ICommand Prepage { get; set; }
        public ICommand ExportFilePatientCommand { get; set; }

       
        

        public static int sort { get; set; }
        public static int by { get; set; }
        public NavigationService _navigationService;
        public NavigationService NavigationService
        {
            get { return _navigationService; }
            set { _navigationService = value; }
        }

        private readonly DataContext _context;

        public PatientVM()
        {
            _context = new DataContext();
            sort = 0;
            by = 1;
            exelService = new ExelService();
            var query = _context.Patients.AsQueryable();
            Patients=query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
            int totalCount = query.ToList().Count;
            Totalpage= (totalCount + _itemsPerPage - 1) / _itemsPerPage;

            

            placeHolderText = "Search by name, email,...";


            AddPatientCommand = new RelayCommand(NavigateToAddPatientPage);
            EditPatientCommand = new RelayCommand(NavigateToEditPatientPage, CanExecuteUserCommand);
            PatientRecordCommand = new RelayCommand(NavigateToPatientRecordPage, CanExecuteUserCommand);
            Nextpage = new RelayCommand(NextPage, CanNextPage);
            Prepage = new RelayCommand(PrePage, CanPrePage);
            ExportFilePatientCommand = new RelayCommand(ExportToExel);
        }
       
            
        public PatientVM(int a,int b)
        {
            _context = new DataContext();
            sort = a;
            by = b;
            exelService = new ExelService();
            var query = _context.Patients.AsQueryable();
            if(by==0)
            {
                if(sort==0)
                {
                    query = query.OrderBy(x => x.Id);
                }
                else if (sort==1)
                {
                    query = query.OrderBy(x => x.Age);

                }
                else if(sort == 2)
                {
                    query = query.OrderBy(x => x.Height);

                }
                else
                {
                    query = query.OrderBy(x => x.Weight);

                }
            }
            else if (by==1)
            {
                if (sort==0)
                {
                    query = query.OrderByDescending(x => x.Id);
                }
                else if (sort==1)
                {
                    query = query.OrderByDescending(x => x.Age);

                }
                else if (sort==2)
                {
                    query = query.OrderByDescending(x => x.Height);

                }
                else
                {
                    query = query.OrderByDescending(x => x.Weight);


                }
            }
            Patients = query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
            int totalCount = query.ToList().Count;
            Totalpage = (totalCount + _itemsPerPage - 1) / _itemsPerPage;


            placeHolderText = "Search by name, email,...";


            AddPatientCommand = new RelayCommand(NavigateToAddPatientPage);
            EditPatientCommand = new RelayCommand(NavigateToEditPatientPage, CanExecuteUserCommand);
            PatientRecordCommand = new RelayCommand(NavigateToPatientRecordPage, CanExecuteUserCommand);
            Nextpage = new RelayCommand(NextPage, CanNextPage);
            Prepage = new RelayCommand(PrePage, CanPrePage);
            ExportFilePatientCommand = new RelayCommand(ExportToExel);
        }

        private void NavigateToAddPatientPage(object parameter)
        {
            NavigationService?.Navigate(new Uri("Views/Patient/AddPatient.xaml", UriKind.Relative));
        }

        private void NavigateToPatientRecordPage(object parameter)
        {
            if (SelectedPatient != null)
            {
                patientInstan = this.SelectedPatient;
                var x=_context.Patient_Records.Where(p=> p.PatientId == patientInstan.Id).FirstOrDefault();
                if (x != null)
                {
                    NavigationService?.Navigate(new Uri($"Views/Patient/PatientRecord.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to create record for this patient?", "Confirm ", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        NavigationService?.Navigate(new Uri($"Views/Patient/AddPatientRecord.xaml", UriKind.Relative));

                    }
                }
            }
            
        }
        private void NavigateToEditPatientPage(object parameter)
        {
            if (SelectedPatient != null)
            {
                patientInstan = this.SelectedPatient;
                NavigationService?.Navigate(new Uri($"Views/Patient/EditPatient.xaml", UriKind.Relative));
            }

        }


        private bool CanExecuteUserCommand(object parameter)
        {
            return SelectedPatient != null;
        }
        private void NextPage(object parameter)
        {
            CurrentPage++;
            loadPatientData();
        }

        // Phân trang - Previous Page
        private void PrePage(object parameter)
        {
            CurrentPage--;
            loadPatientData();
        }
        private void loadPatientData()
        {
            var query = _context.Patients.AsQueryable();
            if (!string.IsNullOrEmpty(SearchText))
            {

                query = query.Where(x => x.Email.Contains(SearchText)
                || x.Name.Contains(SearchText));


            }
            if (by == 0)
            {
                if (sort == 0)
                {
                    query = query.OrderBy(x => x.Id);
                }
                else if (sort == 1)
                {
                    query = query.OrderBy(x => x.Age);

                }
                else if (sort == 2)
                {
                    query = query.OrderBy(x => x.Height);

                }
                else
                {
                    query = query.OrderBy(x => x.Weight);

                }
            }
            else if (by == 1)
            {
                if (sort == 0)
                {
                    query = query.OrderByDescending(x => x.Id);
                }
                else if (sort == 1)
                {
                    query = query.OrderByDescending(x => x.Age);

                }
                else if (sort == 2)
                {
                    query = query.OrderByDescending(x => x.Height);

                }
                else
                {
                    query = query.OrderByDescending(x => x.Weight);


                }
            }
            Patients = query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
            int totalCount = query.ToList().Count;
            Totalpage = (totalCount + _itemsPerPage - 1) / _itemsPerPage;
        }


        // Kiểm tra có thể đi tới trang trước đó không
        private bool CanPrePage(object parameter)
    {
        return _currentPage > 1;
    }
    private bool CanNextPage(object parameter)
        {

            return _currentPage < totalpage;
        }

        private void ExportToExel(object parameter)
    {
        //get list
        var query = _context.Patients.AsQueryable();        
        //Convert
        var data = exelService.ConvertListToExelPatient(query.ToList());

        //Send
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            FileName = "AccountsList.xlsx"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            File.WriteAllBytes(saveFileDialog.FileName, data);
            MessageBox.Show("The Excel file has been downloaded successfully.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

}

