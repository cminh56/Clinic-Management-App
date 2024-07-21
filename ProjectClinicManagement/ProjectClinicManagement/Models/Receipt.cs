using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Models
{
    public class Receipt
    {
        [Key] public int Id { get; set; }
        [Required] public int PatientId { get; set; }
        [Required] public float TotalAmount { get; set; }
        [Required] public int ReceptionistId { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public StatusType Status { get; set; }

        [ForeignKey("PatientId")] public virtual Patient? Patient { get; set; }
        [ForeignKey("ReceptionistId")] public virtual Account? Receptionist { get; set; }
        public enum StatusType
        {
            Paid,
            Unpaid,
            Cancel
        }
    }
}
