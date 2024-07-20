using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Models
{
  public  class Prescription_Medicine
    {
        [Key] public int Id { get; set; }
        [Required] public int MedicineID { get; set; }
        [Required] public int PrescriptionID { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public string Unit { get; set; }
        [Required] public float Price { get; set; }
        [ForeignKey("MedicineID")] public virtual Medicine? Medicine { get; set; }
        [ForeignKey("PrescriptionID")] public virtual Prescription? Prescription { get; set; }

    }
}
