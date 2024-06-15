using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Models
{
    public class Patient_Record
    {
        [Key] public int Id { get; set; }
        [Required] public int PatientId { get; set; }
        [Required] public int DoctorId { get; set; }
        [Required] public string Disease { get; set; }
        [Required] public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("PatientId")] public virtual Patient? Patient { get; set; }
        [ForeignKey("DoctorId")] public virtual Account? Doctor { get; set; }

        public virtual ICollection<Prescription>? Prescriptions { get; set; }
      
        
    }
}
