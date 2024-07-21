using Microsoft.EntityFrameworkCore;
using ProjectClinicManagement.Command;
using ProjectClinicManagement.Data;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.ViewModel.Common;
using ProjectClinicManagement.Views.Receiptor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectClinicManagement.ViewModel.Receiptor
{
    public class ReceiptorVM : BaseViewModel
    {
        private readonly DataContext context;
        public static Receipt receiptInsta;
        public NavigationService _navigationService;

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
        public int _itemsPerPage = 8; // Số mục trên mỗi trang
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
        private List<Receipt> receipts;
        public List<Receipt> Receipts
        {
            get { return receipts; }
            set
            {
                receipts = value;
                OnPropertyChanged();
            }
        }
        //Declarec account
        private Receipt receipt;
        public Receipt Receipt
        {
            get { return (Receipt)receipt; }
            set
            {
                receipt = value;
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

        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public string Status { get; set; }
        public string SearchText { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ExportToExelCommand { get; set; }
        public ICommand Nextpage { get; set; }
        public ICommand Prepage { get; set; }
        public ICommand ReceiptDetailsCommand { get; set; }
        public ICommand CheckoutCommand { get; set; }

        public ReceiptorVM()
        {
            context = new DataContext();
            SearchCommand = new RelayCommand(Search);
            ExportToExelCommand = new RelayCommand(ExportToExel);
            Nextpage = new RelayCommand(NextPage, CanNextPage);
            Prepage = new RelayCommand(PrePage, CanPrePage);
            ReceiptDetailsCommand = new RelayCommand(ReceiptDetails);
            CheckoutCommand = new RelayCommand(Checkout);
            getListReceipts();

        }


        public void Checkout(Object pra)
        {
            if (receipt == null)
            {
                MessageBox.Show("Please Choose one Receipt to check out");
                return;
            }
            receiptInsta = receipt;
            if (isCashChecked)
            {

                Invoices invoices = new Invoices();
                invoices.Show();
            }
            if(isCashChecked) { 
            
            }
        }
        private void ReceiptDetails(Object pra)
        {
            // Pass the account as a parameter
            if (receipt != null)
            {
                receiptInsta = receipt;
                _navigationService.Navigate(new Uri($"Views/Receiptor/ReceiptDetails.xaml", UriKind.Relative));
            }


        }


        private void getListReceipts()
        {
            var query = context.Receipts.Include(r => r.Patient).AsQueryable();
            Totalpage = query.ToList().Count - 1 / _itemsPerPage == 0 ? 1 : query.ToList().Count / _itemsPerPage + 1;
            Receipts = query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();

        }


        private void ExportToExel(Object pra)
        {
            //Export here
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Receipts");

                // Thêm tiêu đề cột
                worksheet.Cells[1, 1].Value = "Receipt ID";
                worksheet.Cells[1, 2].Value = "Patient Name";
                worksheet.Cells[1, 3].Value = "Patient Phone";
                worksheet.Cells[1, 4].Value = "Date";
                worksheet.Cells[1, 5].Value = "Status";
                worksheet.Cells[1, 6].Value = "Total Amount";

                // Thêm dữ liệu vào worksheet
                for (int i = 0; i < Receipts.Count; i++)
                {
                    var receipt = Receipts[i];
                    worksheet.Cells[i + 2, 1].Value = receipt.Id;
                    worksheet.Cells[i + 2, 2].Value = receipt.Patient.Name;
                    worksheet.Cells[i + 2, 3].Value = receipt.Patient.Phone;
                    worksheet.Cells[i + 2, 4].Value = receipt.Date.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 5].Value = receipt.Status.ToString();
                    worksheet.Cells[i + 2, 6].Value = receipt.TotalAmount;
                }

                // Lưu file Excel
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FilterIndex = 2,
                    FileName = "Receipts.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                    MessageBox.Show("Export to Excel successful!");
                }
            }
        }
        private void Search(Object pra)
        {
            var query = context.Receipts.Include(r => r.Patient).AsQueryable();
            if (!string.IsNullOrEmpty(SearchText))
            {
                query = query.Where(p => p.Patient.Name.Contains(SearchText) || p.Patient.Phone.Contains(SearchText));
            }

            if (Enum.TryParse<Receipt.StatusType>(Status, out var statusType))
            {

                query = query.Where(p => p.Status == statusType);
            }
            if (DateFrom != DateTime.MinValue)
            {
                query = query.Where(r => r.Date >= DateFrom);
            }

            if (DateTo != DateTime.MinValue)
            {
                query = query.Where(r => r.Date <= DateTo);
            }

            Totalpage = query.ToList().Count / _itemsPerPage == 0 ? 1 : query.ToList().Count / _itemsPerPage + 1;
            Receipts = query.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        }

        private void NextPage(object parameter)
        {
            CurrentPage++;
            Search("");



        }

        // Phân trang - Previous Page
        private void PrePage(object parameter)
        {
            CurrentPage--;
            Search("");

        }
        // Kiểm tra có thể đi tới trang tiếp theo không
        private bool CanNextPage(object parameter)
        {

            return _currentPage < totalpage;
        }

        // Kiểm tra có thể đi tới trang trước đó không
        private bool CanPrePage(object parameter)
        {
            return _currentPage > 1;
        }
    }
}
