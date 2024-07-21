using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Models
{
    public class Prescription
    {
        [Key] public int Id { get; set; }
        [Required] public int PatientRecordId { get; set; }
        [Required] public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Instruction { get; set; }
        public string Remark { get; set; }
        [Required] public DateTime Date { get; set; }

        [ForeignKey("PatientRecordId")] public virtual Patient_Record? Patient_Record { get; set; }
        public virtual ICollection<Prescription_Medicine>? Prescription_Medicines { get; set; }
        
             
    }
}
