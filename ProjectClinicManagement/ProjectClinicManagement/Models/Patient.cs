using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Emergency { get; set; }
        [Required] public StatusType Status { get; set; }

        public virtual ICollection<Patient_Record>? Patient_Records { get; set; }
        public enum StatusType
        {
            Inactive,
            Active
            
        }

    }
}
