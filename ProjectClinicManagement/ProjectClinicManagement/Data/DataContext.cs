using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectClinicManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Data
{
    public class DataContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = config.GetConnectionString("Dbcontext");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Account> Account { get; set; }
        //public DbSet<Patient> Patients { get; set; }
        //public DbSet<Medicine> Medicines { get; set; }
        //public DbSet<Patient_Record> Patient_Records { get; set; }
        //public DbSet<Prescription> Prescriptions { get; set; }
        //public DbSet<Receipt> Receipts { get; set; }
        //public DbSet<Attendance> Attendances { get; set; }
        //public DbSet<TaskAssignment> TaskAssignments { get; set; }
        //public DbSet<Prescription_Medicine> Prescription_Medicines { get; set; }

    }
}
