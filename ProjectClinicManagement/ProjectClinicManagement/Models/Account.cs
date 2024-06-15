using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Models
{
    public class Account
    {
        [Key] public int Id { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Emai { get; set; }
        [Required] public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required] public RoleType Role { get; set; }
        [Required] public StatusType Status { get; set; }

        public virtual ICollection<Patient_Record>? Patient_Records { get; set; }
        public virtual ICollection<Receipt>? Receipts { get; set; }

        public enum RoleType
        {
            Admin,
            Doctor,
            Patient
        }
        public enum StatusType
        {
            Active,
            Inactive
        }
    }
}
