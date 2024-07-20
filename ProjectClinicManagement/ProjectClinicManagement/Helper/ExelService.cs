using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using OfficeOpenXml;
using ProjectClinicManagement.Models;
using ProjectClinicManagement.Data;
using static ProjectClinicManagement.Models.Account;

namespace ProjectClinicManagement.Helper
{
    public class ExelService
    {
        private readonly DataContext context = new DataContext();
        public byte[] ConvertListToExel(List<Account> accounts)
        {
            // Đường dẫn gốc của thư mục bin
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Lấy thư mục gốc của dự án (ProjectClinicManagement)
            string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;

            // Đường dẫn đến thư mục Template trong dự án
            string templateDirectory = Path.Combine(projectDirectory, "Templates");

            // Đường dẫn đầy đủ đến tệp tin Excel
            string excelFilePath = Path.Combine(templateDirectory, "UserTemplate.xlsx");
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package =
                 new ExcelPackage
                 (new FileInfo(excelFilePath));

            var worksheet = package.Workbook.Worksheets[0];
            worksheet.Cells["A1"].Value = "Account List";
            worksheet.Cells["A3"].Value = "ID";
            worksheet.Cells["B3"].Value = "Email";
            worksheet.Cells["C3"].Value = "Name";
            worksheet.Cells["D3"].Value = "Dob";
            worksheet.Cells["E3"].Value = "Gender";
            worksheet.Cells["F3"].Value = "UserName";
            worksheet.Cells["G3"].Value = "Role";
            worksheet.Cells["H3"].Value = "Status";



            for (int i = 0; i < accounts.Count(); i++)
            {

                worksheet.Cells[i + 4, 1].Value = accounts[i].Id;
                worksheet.Cells[i + 4, 2].Value = accounts[i].Email;
                worksheet.Cells[i + 4, 3].Value = accounts[i].Name;
                worksheet.Cells[i + 4, 4].Value = accounts[i].Dob.ToString();
                worksheet.Cells[i + 4, 5].Value = accounts[i].Gender.ToString();
                worksheet.Cells[i + 4, 6].Value = accounts[i].UserName;
                worksheet.Cells[i + 4, 7].Value = accounts[i].Role.ToString();
                worksheet.Cells[i + 4, 8].Value = accounts[i].Status.ToString();
              
            }
            //Convert ExcelPackage to Stream
            var memoryStream = new MemoryStream();
            package.SaveAsAsync(memoryStream);

            return memoryStream.ToArray();
        }
        public byte[] ConvertListToExelPatient(List<Patient> patients)
        {
            // Đường dẫn gốc của thư mục bin
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Lấy thư mục gốc của dự án (ProjectClinicManagement)
            string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;

            // Đường dẫn đến thư mục Template trong dự án
            string templateDirectory = Path.Combine(projectDirectory, "Templates");

            // Đường dẫn đầy đủ đến tệp tin Excel
            string excelFilePath = Path.Combine(templateDirectory, "UserTemplate.xlsx");
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package =
                 new ExcelPackage
                 (new FileInfo(excelFilePath));

            var worksheet = package.Workbook.Worksheets[0];
            worksheet.Cells["A1"].Value = "Patient List";
            worksheet.Cells["A3"].Value = "ID";
            worksheet.Cells["B3"].Value = "Name";
            worksheet.Cells["C3"].Value = "Age";
            worksheet.Cells["D3"].Value = "Weight";
            worksheet.Cells["E3"].Value = "Height";
            worksheet.Cells["F3"].Value = "Phone";
            worksheet.Cells["G3"].Value = "Email";
            worksheet.Cells["H3"].Value = "Address";
            worksheet.Cells["I3"].Value = "Emergency";
            worksheet.Cells["J3"].Value = "Status";




            for (int i = 0; i < patients.Count(); i++)
            {

                worksheet.Cells[i + 4, 1].Value = patients[i].Id;
                worksheet.Cells[i + 4, 2].Value = patients[i].Name;
                worksheet.Cells[i + 4, 3].Value = patients[i].Age;
                worksheet.Cells[i + 4, 4].Value = patients[i].Weight.ToString();
                worksheet.Cells[i + 4, 5].Value = patients[i].Height.ToString();
                worksheet.Cells[i + 4, 6].Value = patients[i].Phone.ToString();
                worksheet.Cells[i + 4, 7].Value = patients[i].Email.ToString();
                worksheet.Cells[i + 4, 8].Value = patients[i].Address.ToString();
                worksheet.Cells[i + 4, 9].Value = patients[i].Emergency.ToString();
                worksheet.Cells[i + 4, 10].Value = patients[i].Status.ToString();



            }
            //Convert ExcelPackage to Stream
            var memoryStream = new MemoryStream();
            package.SaveAsAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
